using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum PanelsGameScene { MainPanel = 0, OprionsPanel, WarningPanel}
public enum Actions { Theft = 1, Rescue }
public class SceneGameManager : MonoBehaviour
{
    [SerializeField] private MainManager _mainManager;

    [Header("Character")]
    [SerializeField] private SpriteRenderer _characterHolder;
    private int _numCharacter;

    [Header("Thing")]
    [SerializeField] private SpriteRenderer _thingHolder;
    private int _numThing;

    [Header("Background")]
    [SerializeField] private float _speed;
    [SerializeField] private MeshRenderer _meshBg;
    private int _numBg;

    [Header("Game")]
    [SerializeField] private int _timeToWarning;
    [SerializeField] private int _timeToLose;
    [SerializeField] private Text _textTimeToLose;
    [SerializeField] private Text _textCurrentWins;

    private Vector2 _meshOffsetBg;
    public bool isPause;

    [SerializeField] private int _currentWins;
    private int _currentRightAction;
    private int _remainingTimeBeforeWarning;

    [Header("test")]
    public int rightAnswer;

    private void Awake()
    {
        _mainManager.panels[(int)PanelsGameScene.WarningPanel].SetActive(false);
        _mainManager.panels[(int)PanelsGameScene.MainPanel].SetActive(true);

        if (_mainManager == null)
            _mainManager = transform.parent.GetComponentInChildren<MainManager>();
    }
    private void Start()
    {
        _mainManager.eventManager.ButtonActionPressedEvent += SelectAction;

        LoadCurrentWins();

        if (_currentWins > 0)
            _remainingTimeBeforeWarning = AllDataSave.RemainingTimeBeforeWarning;

        CreateNumberRightActionOrLoad();

        if(AllDataSave.NumberLoadedBackground > 0)
            CreateNewBackgroundOrLoad(AllDataSave.NumberLoadedBackground);
        else
            CreateNewBackgroundOrLoad();

        if (AllDataSave.NumberLoadedCharacter > 0)
            CreateNewCharacter(AllDataSave.NumberLoadedCharacter);
        else
            CreateNewCharacter();

        if (AllDataSave.NumberLoadedThing > 0)
            CreateNewThing(AllDataSave.NumberLoadedThing);
        else
            CreateNewThing();

        _meshOffsetBg = _meshBg.sharedMaterial.mainTextureOffset;
        StartCoroutine(RunCharacter());
        StartCoroutine(TimeToWarning());
    }
    private void LoadCurrentWins()//
    {
        _currentWins = AllDataSave.NumberCurrentWins;
        _textCurrentWins.text = _currentWins + " / 10";
    }
    private void CreateNumberRightActionOrLoad()//придумывание правильного ответа или загрузка
    {
        if (AllDataSave.CurrentRightAction == 0)
        {
            int randRightAction = Random.Range(1, 3);
            _currentRightAction = randRightAction;
        }
        else
            _currentRightAction = AllDataSave.CurrentRightAction;
       
        rightAnswer = _currentRightAction;//
    }
    public void AddCurrentWin()//добавление очка к текущим победам
    {
        if (_currentWins < 10)
            _currentWins++;
        else
            Win();

        _textCurrentWins.text = _currentWins + " / 10";
        _mainManager.allDataSave.SaveCurrentWins(_currentWins);
        _mainManager.allDataSave.SaveNumberVictory(_currentWins);
    }
    public void Pause(bool pause)//срабатывает евентом Button при открытии панели опций
    {
        isPause = pause;
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
    }//движение заднего фона
    private void CreateNewBackgroundOrLoad(int number = 0)
    {
        if (number == 0)
        {
            _numBg = Random.Range(1, Resources.LoadAll<Material>("Background Materials").Length + 1);
            _meshBg.material = Resources.Load<Material>($"Background Materials/Background_{_numBg}");
        }
        else
        {
            _numBg = number;
            _meshBg.material = Resources.Load<Material>($"Background Materials/Background_{_numBg}");
        }
    }
    private void CreateNewCharacter(int number = 0)
    {
        if (number == 0)
        {
            _numCharacter = Random.Range(1, Resources.LoadAll<Sprite>("Characters").Length + 1);
            _characterHolder.sprite = Resources.Load<Sprite>($"Characters/Character_{_numCharacter}");
        }
        else
        {
            _numCharacter = number;
            _characterHolder.sprite = Resources.Load<Sprite>($"Characters/Character_{_numCharacter}");
        }
    }
    private void CreateNewThing(int number = 0)
    {
        if (number == 0)
        {
            _numThing = Random.Range(1, Resources.LoadAll<Sprite>("Things").Length + 1);
            _thingHolder.sprite = Resources.Load<Sprite>($"Things/Thing_{_numThing}");
        }
        else
        {
            _numThing = number;
            _thingHolder.sprite = Resources.Load<Sprite>($"Things/Thing_{_numThing}");
        }
    }
    private IEnumerator TimeToWarning()//таймер до показа панели предупреждения о поражении
    {
        int timer = 0;

        if (_remainingTimeBeforeWarning == 0)
            timer = _timeToWarning;
        else
            timer = _remainingTimeBeforeWarning;

        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer -= 1;
        }
        if (timer <= 0)
        {
            if (isPause)
            {
                _mainManager.panels[(int)PanelsGameScene.OprionsPanel].SetActive(false);
                _mainManager.panels[(int)PanelsGameScene.MainPanel].SetActive(true);
            }

            _mainManager.panels[(int)PanelsGameScene.WarningPanel].SetActive(true);
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
    }//таймер панели предупреждения о пиражении
    public void StopTimers()
    {
        StopCoroutine(RunCharacter());
        StopCoroutine(TimeToWarning());
        StopCoroutine(TimeToLose());
    }
    private void SelectAction(int number)//срабатывает ивентом при выборе действия
    {
        StopTimers();

        if (_currentRightAction == number)
            CorrectAnswer();
        else
            Lose();
        
        CreateNumberRightActionOrLoad();

        _remainingTimeBeforeWarning = 0;
        _mainManager.allDataSave.SaveRemainingTimeBeforeWarning(_remainingTimeBeforeWarning);
    }
    private void Lose()//при неверном ответе
    {
        Debug.Log($"you lose!");
        _currentWins = 0;
        _mainManager.allDataSave.SaveCurrentWins(_currentWins);
        StopCoroutine(TimeToLose());
    }
    private void CorrectAnswer()//при верном ответе
    {
        AddCurrentWin();
        _mainManager.allDataSave.SaveTotalWins();

        CreateNewBackgroundOrLoad();
        CreateNewCharacter();
        CreateNewThing();

        StartCoroutine(RunCharacter());
        StartCoroutine(TimeToWarning());
    }
    private void Win()//при всех правильных ответах
    {
        _currentWins = 0;
        _mainManager.allDataSave.SaveCurrentWins(_currentWins);
    }
    private void OnDestroy()
    {
        _mainManager.eventManager.ButtonActionPressedEvent -= SelectAction;
    }
    private void OnApplicationQuit()//сохранение фона, перса, вещи при закрытии игры
    {
        _mainManager.allDataSave.SaveCurrentRightAction(_currentRightAction);
        _mainManager.allDataSave.SaveLoadedNumberBackground(_numBg);
        _mainManager.allDataSave.SaveLoadedNumberCharacter(_numCharacter);
        _mainManager.allDataSave.SaveLoadedNumberThing(_numThing);
    }
}
