using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(BoxCollider2D))]
public class ButtonAchievement : AbstractButton
{
    [Header("Button Data")]
    [SerializeField] private Button button;
    [SerializeField] private TypeAchievement _type;
    [SerializeField] private AchievementPanels _openPanel;
    [SerializeField] private Sprite _spriteClosedMedal;
    [SerializeField] private Sprite _spriteOpenedMedal;
    [SerializeField] private Image _imageMedal;
    [SerializeField] private Text _textName;
    [SerializeField] private RectTransform _rectImage;
    [SerializeField] private float _time;
    [SerializeField] private InfoAchievement _infoAchievement;

    private Vector2 _startPos;
    private float _endPos;
    private bool _isShining;
    private int _numberOpenedAchievement;
    private bool _buttonIsPressed;
    private Tweener _tweener;

    private void Awake()
    {
        if (button == null)
            button = GetComponent<Button>();

        if (_rectImage == null)
            _rectImage = transform.Find("Image Glow").GetComponent<RectTransform>();

        if (_imageMedal == null)
            _imageMedal = transform.Find("Image medal").GetComponent<Image>();

        if (_textName == null)
            _textName = transform.Find("Text name").GetComponent<Text>();

        _startPos = _rectImage.transform.localPosition;
        _endPos = -_startPos.x;
        _rectImage.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        if (_isShining)
            Shining(true);
    }
    private void OnDisable()
    {
        if (!_isShining)
            Shining(false);
    }
    private void OnMouseDown()
    {
        _buttonIsPressed = true;

        StartCoroutine(TapTimer());
    }
    private void OnMouseUp()
    {
        _buttonIsPressed = false;
    }
    private IEnumerator TapTimer()
    {
        float timer = 0;

        while (_buttonIsPressed)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (!_buttonIsPressed)
        {
            if (timer < .1f)
                OnPressButton();

            StopCoroutine(TapTimer());
        }
    }
    private void OnPressButton()
    {
        if (_numberOpenedAchievement >= 0)
        {
            mainManager.achievementsManager.OpenPanel(_openPanel, true);
            _isShining = false;
            Shining(false);
            mainManager.achievementsManager.TranslateTextPanel(_infoAchievement.KeyForTranslateDescription);

            AcceptAchievement();
        }
        else
            UpdateUIAchievement('0');
    }

    public void Shining(bool play)
    {
        if (play)
        {
            _isShining = true;
            _rectImage.gameObject.SetActive(true);
            _rectImage.transform.localPosition = _startPos;
            _tweener = _rectImage.DOAnchorPosX(_endPos, _time, true);

            DOVirtual.DelayedCall(_time, () =>
            {
                if (_tweener != null)
                    _tweener = _rectImage.DOAnchorPosX(_startPos.x, _time, true);
                else
                    DOTween.Kill(_tweener);
            });
            DOVirtual.DelayedCall(_time * 2, () =>
            {
                if (_tweener != null)
                    Shining(true);
                else
                    DOTween.Kill(_tweener);
            });
            
        }
        else if (!_isShining && !play)
        {
            DOTween.Kill(gameObject);
            _rectImage.gameObject.SetActive(false);
            _rectImage.transform.localPosition = _startPos;
        }
    }
    public void UpdateUIAchievement(char type)
    {
        switch (type)
        {
            case '0':
                _textName.text = "**********";
                _imageMedal.sprite = _spriteClosedMedal;
                break;

            case '1':
                Shining(true);
                _textName.text = "**********";
                _imageMedal.sprite = _spriteOpenedMedal;
                break;

            case '2':
                _textName.text = transform.name;
                _imageMedal.sprite = _spriteOpenedMedal;
                break;

            default:
                break;
        }
    }
    private void AcceptAchievement()
    {
        if (_type == TypeAchievement.Gold)
        {
            string golden = mainManager.allDataSave.GoldenAchievements;
            golden = golden.Remove(_numberOpenedAchievement, 1).Insert(_numberOpenedAchievement, "2");

            mainManager.allDataSave.SaveGoldenAchievement(golden);
        }
        else if (_type == TypeAchievement.Silver)
        {
            string silver = mainManager.allDataSave.SilverAchievements;
            silver = silver.Remove(_numberOpenedAchievement, 1).Insert(_numberOpenedAchievement, "2");

            mainManager.allDataSave.SaveSilverAchievement(silver);
        }

        UpdateUIAchievement('2');
    }

    public void NumberOpenedAchievement(int number)
    {
        _numberOpenedAchievement = number;
    }
}
