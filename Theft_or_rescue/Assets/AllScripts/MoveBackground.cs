using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class MoveBackground : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private RectTransform[] _bgs;
    [SerializeField] private EventManager _eventManager;
    [SerializeField] private Image[] _images;

    private float[] _startPosBgsX;
    private float _widthImage;
    private Coroutine _runBackground;

    private void Awake()
    {
        if (_bgs.Length == 0)
            _bgs = GetComponentsInChildren<RectTransform>();

        if (_images.Length == 0)
            _images = GetComponentsInChildren<Image>();

        if (_eventManager == null)
            _eventManager = GameObject.FindGameObjectWithTag("MainManager").transform.parent.GetComponentInChildren<EventManager>();

        if (_eventManager)
            _eventManager.MoveBackgroundImageEvent += ChangeMoveBackgorund; 

        _startPosBgsX = new float[_bgs.Length];
        _widthImage = _bgs[0].rect.width;

        for (int i = 0; i < _bgs.Length; i++)
            _startPosBgsX[i] = _bgs[i].localPosition.x;
    }
    private void ChangeMoveBackgorund(bool canMove)
    {
        if (canMove)
            _runBackground = Coroutines.StartRoutine(Moving());
        else
            Coroutines.StopRoutine(_runBackground);
    }
    public void GetImagesAndStartMoving(Sprite sprite)
    {
        for (int i = 0; i < _images.Length; i++)
            _images[i].sprite = sprite;

        StartBeginning();

        _runBackground = Coroutines.StartRoutine(Moving());
    }
    private void StartBeginning()
    {
        Coroutines.StopRoutine(_runBackground);

        for (int i = 0; i < _bgs.Length; i++)
            _bgs[i].localPosition = new Vector2(_startPosBgsX[i], _bgs[i].localPosition.y);
    }

    private IEnumerator Moving()
    {
        while (true)
        {
            for (int i = 0; i < _bgs.Length; i++)
            {
                if (_bgs[i].localPosition.x > _startPosBgsX[i] - _widthImage)
                    _bgs[i].localPosition = new Vector2(_bgs[i].localPosition.x - _speed, _bgs[i].localPosition.y);
                else
                    _bgs[i].localPosition = new Vector2(_startPosBgsX[i], _bgs[i].localPosition.y);
            }

            yield return null;
        }
    }
    private void OnDisable()
    {
        if (_eventManager)
            _eventManager.MoveBackgroundImageEvent -= ChangeMoveBackgorund;
    }
}

