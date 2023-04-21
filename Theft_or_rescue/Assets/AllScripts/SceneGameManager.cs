using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum Actions { Theft, Rescue }
public class SceneGameManager : MonoBehaviour
{
    [SerializeField] private MainManager _mainManager;

    [Header("Character")]
    [SerializeField] private SpriteRenderer _characterHolder;

    [Header("Thing")]
    [SerializeField] private SpriteRenderer _thingHolder;

    [Header("Background")]
    [SerializeField] private float _speed;
    [SerializeField] private MeshRenderer _meshBg;

    [Header("Game")]
    [SerializeField] private int _timeToWarning;
    [SerializeField] private int _timeToLose;
    [SerializeField] private GameObject _warnningPanel;
    [SerializeField] private Text _textTimeToLose;
    [SerializeField] private Text _textCurrentWins;

    private Vector2 _meshOffsetBg;
    public bool isPause;

    public int currentWins;
    public int currentRightAction;

    private void Awake()
    {
        _warnningPanel.SetActive(false);

        if (_mainManager == null)
            _mainManager = transform.parent.GetComponentInChildren<MainManager>();

    }
    private void Start()
    {
        _mainManager.eventManager.ButtonActionPressedEvent += SelectAction;

        CreateNumberRightAction();

        CreateNewBackground();
        CreateNewCharacter();
        CreateNewThing();

        _meshOffsetBg = _meshBg.sharedMaterial.mainTextureOffset;
        StartCoroutine(RunCharacter());
        StartCoroutine(TimeToWarning());
    }
    private void CreateNumberRightAction()
    {
        int randRightAction = Random.Range(0, 2);

        currentRightAction = randRightAction;
    }
    public void AddCurrentWin()
    {
        if (currentWins < 10)
            currentWins++;

        _textCurrentWins.text = currentWins + " / 10";
        _mainManager.allDataSave.SaveNumberVictory(currentWins);
        //добавить победу
    }
    public bool IsPause()
    {
        return isPause;
    }
    private IEnumerator RunCharacter()
    {
        if (isPause)
            yield return null;
        else
        { 
            var x = Mathf.Repeat(Time.time * _speed, 1);
            var offset = new Vector2(x, _meshOffsetBg.y);

            _meshBg.sharedMaterial.mainTextureOffset = offset;
            yield return null;
        }

        StartCoroutine(RunCharacter());
    }
    private void CreateNewBackground()
    {
        int randBg = Random.Range(1, Resources.LoadAll<Material>("Background Materials").Length + 1);
        _meshBg.material = Resources.Load<Material>($"Background Materials/Background_{randBg}");
        _mainManager.allDataSave.SaveLoadedNumberCharacter(randBg);
    }
    private void CreateNewCharacter()
    {
        int randChar = Random.Range(1, Resources.LoadAll<Sprite>("Characters").Length + 1);
        _characterHolder.sprite = Resources.Load<Sprite>($"Characters/Character_{randChar}");

        _mainManager.allDataSave.SaveLoadedNumberCharacter(randChar);
    }
    private void CreateNewThing()
    {
        int randThing = Random.Range(1, Resources.LoadAll<Sprite>("Things").Length + 1);
        _thingHolder.sprite = Resources.Load<Sprite>($"Things/Thing_{randThing}");

        _mainManager.allDataSave.SaveLoadedNumberThing(randThing);
    }
    private IEnumerator TimeToWarning()
    {
        int timer = _timeToWarning;

        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer -= 1;
        }
        if (timer <= 0)
        {
            _warnningPanel.SetActive(true);
            StartCoroutine(TimeToLose());
        }
    }
    private IEnumerator TimeToLose()
    {
        int timer = _timeToLose;
        StopCoroutine(TimeToWarning());

        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer -= 1;
            _textTimeToLose.text = timer.ToString();
        }
        if (timer <= 0)
            Lose();
    }
    public void StopTimers()
    {
        StopCoroutine(RunCharacter());
        StopCoroutine(TimeToWarning());
        StopCoroutine(TimeToLose());
    }
    private void SelectAction(int number)
    {
        Debug.Log($"select answer = {number}");
        StopTimers();
        CreateNumberRightAction();

        if (currentRightAction == number)
            CorrectAnswer();
        else
            Lose();
    }
    private void Lose()
    {
        Debug.Log($"you lose!");
        currentWins = 0;
        StopCoroutine(TimeToLose());
    }
    private void CorrectAnswer()
    {
        AddCurrentWin();
        CreateNewBackground();
        CreateNewCharacter();
        CreateNewThing();
        StartCoroutine(RunCharacter());
        StartCoroutine(TimeToWarning());
    }
    private void Win()
    {
        currentWins = 0;
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            CreateNewBackground();
        if (Input.GetKeyDown(KeyCode.S))
            CreateNewCharacter();
        if (Input.GetKeyDown(KeyCode.D))
            CreateNewThing();
    }
#endif
    private void OnDestroy()
    {
        _mainManager.eventManager.ButtonActionPressedEvent -= SelectAction;
    }
}
