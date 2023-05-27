using System.Collections;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public enum PanelsGameScene { MainPanel = 0, OprionsPanel, WarningPanel, LosePanel, CharacterPanel}
public class SceneGameManager : MonoBehaviour
{
    [SerializeField] private MainManager _mainManager;

    [Header("Character")]
    [SerializeField] private Animator _characterHolder;

    [Header("Thing")]
    [SerializeField] private SpriteRenderer _thingHolder;

    [Header("Background")]
    [SerializeField] private MoveBackground _moveBackground;

    [Header("Game")]
    [SerializeField] private Text _textTimeToLose;
    [SerializeField] private Text _textCurrentWins;
    [SerializeField] private int _numAnswer;//количетво ответов
    [SerializeField] private AnimatorController[] _characters;
    [SerializeField] private Sprite[] _things;
    [SerializeField] private bool[] _answers;
    private const int _timeToWarning = 60;//время до предупреждения
    private const int _timeToLose = 10;//время до проигрыша
    [SerializeField] private int _numberCurrentTotalWins;

    [SerializeField] private int _numberChance;//колво попыток
    [SerializeField] private int _numCurrentWins;//колво правильных ответов
    [SerializeField] private int _remainingTimeBeforeWarning;//оставшееся время ожидания
    [SerializeField] private int _remainingNumberAttempts;//оставшееся колво попыток для сохранения

    [Header("Coroutines")]
    private Coroutine _timerToLose;
    private Coroutine _timerToWarning;

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

        _numCurrentWins = _mainManager.allDataSave.NumberCurrentWins;

        GetRemainingTimeBeforeWarning();
        GetNumberCurrentTotalWins();
        GetRemainingNumberAttempts();
        UpdateTextVins();

        if (_mainManager.allDataSave.NumberChance == 0)
            _numberChance = 1;
        else
            _numberChance = _mainManager.allDataSave.NumberChance;

        GetNumberAnswer();

        _timerToWarning = Coroutines.StartRoutine(TimeToWarning());

        GetDataLevel();
    }
    private int GetNumberCurrentTotalWins()
    {
        _numberCurrentTotalWins = _mainManager.allDataSave.NumberCurrentTotalWins;

        return _numberCurrentTotalWins;
    }
    private int GetRemainingTimeBeforeWarning()
    {
        _remainingTimeBeforeWarning = _mainManager.allDataSave.RemainingTimeBeforeWarning;

        if (_remainingTimeBeforeWarning == 0)
            _remainingTimeBeforeWarning = 60;

        return _remainingTimeBeforeWarning;
    }
    private void UpdateTextVins()
    {
        _textCurrentWins.text = $"{_numCurrentWins} / 10";
    }
    private int GetRemainingNumberAttempts()
    {
        if (_mainManager.allDataSave.RemainingNumberAttempts == 0)
            _remainingNumberAttempts = 10;
        else
            _remainingNumberAttempts = _mainManager.allDataSave.RemainingNumberAttempts;

        return _remainingNumberAttempts;
    }
    private int GetNumberAnswer()
    {
        _numAnswer = _mainManager.allDataSave.NumberAnswer;
        return _numAnswer;
    }
    private void GetDataLevel()
    {
        _characters = new AnimatorController[_mainManager.allDataSave.Characters.Length];
        _answers = new bool[_mainManager.allDataSave.Answers.Length];
        _things = new Sprite[_mainManager.allDataSave.Things.Length];

        for (int i = 0; i < _characters.Length; i++)
            _characters[i] = Resources.Load<AnimatorController>($"Characters/Character_{_mainManager.allDataSave.Characters[i]}");

        for (int i = 0; i < _things.Length; i++)
            _things[i] = Resources.Load<Sprite>($"Things/Thing_{_mainManager.allDataSave.Things[i]}");

        for (int i = 0; i < _answers.Length; i++)
            _answers[i] = _mainManager.allDataSave.Answers[i];

        CreateBackground(_mainManager.allDataSave.NumberBackground);
        CreateCharacter();
        CreateThing();
    }
    public void AddCurrentWin()//добавление очка к текущим победам
    {
        if (_remainingNumberAttempts > 0)
            _numCurrentWins++;
        else
            PlayArain();

        _textCurrentWins.text = $"{_numCurrentWins} / 10";
        _mainManager.allDataSave.SaveCurrentWins(_numCurrentWins);
    }
    private void CreateBackground(int numBg)
    {
        _moveBackground.GetImagesAndStartMoving(Resources.Load<Sprite>($"Background Sprites/bg_{numBg}"));
    }
    private void CreateCharacter()
    {
        //_characterHolder.sprite = _characters[_numAnswer];

        _characterHolder.runtimeAnimatorController = _characters[_numAnswer];

        //Debug.Log(_characters[_numAnswer]);
        //_characterHolder = _characters[_numAnswer];
        //GameObject character = _characters[_numAnswer];
        //character.transform.parent = _characterHolder;

    }
    private void CreateThing()
    {
        _thingHolder.sprite = _things[_numAnswer];
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

            if (timer <= 0)
                PlayArain();
        }
    }
    public void StopTimers()
    {
        Coroutines.StopRoutine(_timerToWarning);
        Coroutines.StopRoutine(_timerToLose);
        _mainManager.panels[(int)PanelsGameScene.WarningPanel].SetActive(false);
    }
    private void SelectAction(bool value)//срабатывает ивентом при выборе действия
    {
        StopTimers();
        _timerToWarning = Coroutines.StartRoutine(TimeToWarning());
        _remainingTimeBeforeWarning = _timeToWarning;
       
        if (_remainingNumberAttempts > 0 && _numCurrentWins < 10)
        {
            if (_answers[_numAnswer] == value)
                CorrectAnswer();

            _remainingNumberAttempts--;
            _numAnswer++;
        }
        if (_remainingNumberAttempts == 0)
            PlayArain();
    }
    private void PlayArain()
    {
        StopTimers();

        _mainManager.panels[(int)PanelsGameScene.CharacterPanel].SetActive(false);
        _mainManager.panels[(int)PanelsGameScene.LosePanel].SetActive(true);
    }
    public void ContinueGame()//воспользоваться 2м шансом
    {
        _mainManager.panels[(int)PanelsGameScene.CharacterPanel].SetActive(true);
        _mainManager.panels[(int)PanelsGameScene.LosePanel].SetActive(false);
        _remainingNumberAttempts = 10;
        _remainingTimeBeforeWarning = 60;
        _numCurrentWins = 0;
        _mainManager.allDataSave.SaveCurrentWins(_numCurrentWins);
        _numAnswer = 0;
        _numberChance = 2;
        _numberCurrentTotalWins = 0;

        UpdateTextVins();
        CreateCharacter();
        CreateThing();

        _timerToWarning = Coroutines.StartRoutine(TimeToWarning());
    }
    public void ReturnToMainMenu()//вызывается кнопкой
    {
        _mainManager.achievementsManager.CheckAndAddAchievement(_numberChance, _numCurrentWins);
        _numAnswer = 0;
        _numberChance = 1;
        SaveData();

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    private void CorrectAnswer()//при верном ответе
    {
        AddCurrentWin();
        _numberCurrentTotalWins++;
        CreateCharacter();
        CreateThing();
    }
    private void OnDestroy()
    {
        _mainManager.eventManager.ButtonActionPressedEvent -= SelectAction;
    }
    private void OnApplicationQuit()//сохранение данных при отключении
    {
        SaveData();
    }
    private void SaveData()
    {
        _mainManager.allDataSave.SaveSecondChance(_numberChance);
        _mainManager.allDataSave.SaveRemainingTimeBeforeWarning(_remainingTimeBeforeWarning);
        _mainManager.allDataSave.SaveNumberAnswer(_numAnswer);
        _mainManager.allDataSave.SaveCurrentTotalWins(_numberCurrentTotalWins);
        _mainManager.allDataSave.SaveRemainingNumberAttempts(_remainingNumberAttempts);
    }
}
