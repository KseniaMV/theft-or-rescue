using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum PanelsGameScene { MainPanel = 0, OprionsPanel, WarningPanel, LosePanel, CharacterPanel}
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
    [SerializeField] private MoveBackground _moveBackground;
    private int _numBg;

    [Header("Game")]
    [SerializeField] private int _timeToWarning;
    [SerializeField] private int _timeToLose;
    [SerializeField] private Text _textTimeToLose;
    [SerializeField] private Text _textCurrentWins;

    private int _numberAttempts;//колво попыток
    private int _currentWins;//колво правильных ответов
    private int _currentRightAction;//тест, правильный ответ
    [SerializeField] private int _remainingTimeBeforeWarning;//оставшееся время ожидания
    private const int MAX_NUMBER_ACTIONS = 10;
    private int _remainingNumberAttempts;//оставшееся колво попыток для сохранения

    [Header("Coroutines")]
    private Coroutine _timerToLose;
    private Coroutine _timerToWarning;

    [Header("test")]
    public int rightAnswer;

    private void Awake()
    {
        _mainManager.panels[(int)PanelsGameScene.WarningPanel].SetActive(false);
        _mainManager.panels[(int)PanelsGameScene.MainPanel].SetActive(true);
        _mainManager.panels[(int)PanelsGameScene.CharacterPanel].SetActive(true);

        if (_mainManager == null)
            _mainManager = transform.parent.GetComponentInChildren<MainManager>();
    }
    private void Start()
    {
        _mainManager.eventManager.ButtonActionPressedEvent += SelectAction;

        if (_currentWins > 0)
            _remainingTimeBeforeWarning = AllDataSave.RemainingTimeBeforeWarning;

        CreateNumberRightActionOrLoad();

        if (AllDataSave.NumberLoadedBackground > 0)
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

        if (AllDataSave.NumberAttempts == 0)
            _numberAttempts = 1;
        else
            _numberAttempts = AllDataSave.NumberAttempts;

        LoadCurrentWins();
        if (AllDataSave.RemainingTimeBeforeWarning != 0)
            _remainingTimeBeforeWarning = AllDataSave.RemainingTimeBeforeWarning;

        _timerToWarning = Coroutines.StartRoutine(TimeToWarning());
    }
    private void LoadCurrentWins()//
    {
        _currentWins = AllDataSave.NumberCurrentWins;
        _remainingNumberAttempts = MAX_NUMBER_ACTIONS - _currentWins;
        _textCurrentWins.text = $"{_currentWins} / {MAX_NUMBER_ACTIONS}";
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
            TimeIsUp();

        _textCurrentWins.text = $"{_currentWins} / {MAX_NUMBER_ACTIONS}";
        _mainManager.allDataSave.SaveCurrentWins(_currentWins);
        _mainManager.allDataSave.SaveNumberVictory(_currentWins);
    }
    private void CreateNewBackgroundOrLoad(int number = 0)
    {
        if (number == 0)
            _numBg = Random.Range(1, Resources.LoadAll<Sprite>("Background Sprites").Length + 1);
        else
            _numBg = number;

        _moveBackground.GetImagesAndStartMoving(Resources.Load<Sprite>($"Background Sprites/bg_{_numBg}"));
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
        if (_remainingTimeBeforeWarning <= 0)
            _remainingTimeBeforeWarning = _timeToWarning;

        while (_remainingTimeBeforeWarning > 0)
        {
            yield return new WaitForSeconds(1);
            _remainingTimeBeforeWarning -= 1;
        }
        if (_remainingTimeBeforeWarning <= 0)
        {
            _mainManager.panels[(int)PanelsGameScene.WarningPanel].SetActive(true);
            _timerToLose = Coroutines.StartRoutine(TimeToLose());
        }
    }
    private IEnumerator TimeToLose()//таймер панели предупреждения о поражении
    {
        int timer = _timeToLose;
        Coroutines.StopRoutine(_timerToWarning);

        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer -= 1;
            _textTimeToLose.text = timer.ToString();
            Debug.Log(timer);

            if (timer <= 0)
                TimeIsUp();
        }
    }
    public void StopTimers()
    {
        Coroutines.StopRoutine(_timerToWarning);
        Coroutines.StopRoutine(_timerToLose);
        _mainManager.panels[(int)PanelsGameScene.WarningPanel].SetActive(false);
    }
    private void SelectAction(int number)//срабатывает ивентом при выборе действия
    {
        StopTimers();
        _timerToWarning = Coroutines.StartRoutine(TimeToWarning());

        _remainingTimeBeforeWarning = _timeToWarning;

        if (_remainingNumberAttempts > 1)
        {
            if (_currentRightAction == number)
                CorrectAnswer();

            _remainingNumberAttempts--;
        }
        else
            TimeIsUp();

        _mainManager.allDataSave.SaveCurrentRightAction(0);
        CreateNumberRightActionOrLoad();

        _mainManager.allDataSave.SaveRemainingTimeBeforeWarning(_remainingTimeBeforeWarning);
    }
    private void TimeIsUp()//открывает меню с предложением продолжить или выйти
    {
        Coroutines.StopRoutine(_timerToLose);

        if (_numberAttempts == 1 && _currentWins < MAX_NUMBER_ACTIONS)
        {
            _mainManager.panels[(int)PanelsGameScene.CharacterPanel].SetActive(false);
            _mainManager.panels[(int)PanelsGameScene.LosePanel].SetActive(true);
        }
        else
            ReturnToMainMenu();

        _numberAttempts = 2;
    }
    public void ContinueGame()//воспользоваться 2м шансом
    {
        _mainManager.panels[(int)PanelsGameScene.CharacterPanel].SetActive(true);
        _mainManager.panels[(int)PanelsGameScene.LosePanel].SetActive(false);
        _remainingNumberAttempts = MAX_NUMBER_ACTIONS - _currentWins;

        CreateNewBackgroundOrLoad();
        CreateNewCharacter();
        CreateNewThing();

        _timerToWarning = Coroutines.StartRoutine(TimeToWarning());
    }
    public void ReturnToMainMenu()
    {
        _mainManager.achievementsManager.CheckAndAddAchievement(_numberAttempts, _currentWins);
        _currentWins = 0;
        _mainManager.allDataSave.SaveCurrentWins(_currentWins);

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    private void CorrectAnswer()//при верном ответе
    {
        AddCurrentWin();
        _mainManager.allDataSave.SaveTotalWins();

        CreateNewBackgroundOrLoad();
        CreateNewCharacter();
        CreateNewThing();
    }
    private void OnDestroy()
    {
        _mainManager.eventManager.ButtonActionPressedEvent -= SelectAction;
    }
    private void OnApplicationQuit()//сохранение фона, перса, вещи при закрытии игры
    {
        if (_numberAttempts == 2)
            _mainManager.allDataSave.SaveSecondChance(1);

        _mainManager.allDataSave.SaveRemainingTimeBeforeWarning(_remainingTimeBeforeWarning);
        _mainManager.allDataSave.SaveCurrentRightAction(_currentRightAction);
        _mainManager.allDataSave.SaveLoadedNumberBackground(_numBg);
        _mainManager.allDataSave.SaveLoadedNumberCharacter(_numCharacter);
        _mainManager.allDataSave.SaveLoadedNumberThing(_numThing);
    }
}
