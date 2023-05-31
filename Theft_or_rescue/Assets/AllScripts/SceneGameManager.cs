using System.Collections;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public enum PanelsGameScene { MainPanel = 0, OprionsPanel, WarningPanel, LosePanel, CharacterPanel}
public class SceneGameManager : MonoBehaviour
{
    [SerializeField] private MainManager _mainManager;
    [SerializeField] private InfoObtainedAchievement _obtainedAchievement;

    [Header("Character")]
    [SerializeField] private Animator _characterHolder;

    [Header("Thing")]
    [SerializeField] private SpriteRenderer _thingHolder;

    [Header("Background")]
    [SerializeField] private MoveBackground _moveBackground;

    [Header("Game")]
    private const int _timeToWarning = 60;//время до предупреждения
    private const int _timeToLose = 10;//время до проигрыша

    [SerializeField] private Text _textTimeToLose;
    [SerializeField] private Text _textCurrentWins;
    [SerializeField] public int _numAnswer;//количетво ответов
    [SerializeField] private AnimatorController[] _characters;
    [SerializeField] private Sprite[] _things;
    [SerializeField] private bool[] _answers;
    [SerializeField] private int _numberCurrentTotalWins;
    [SerializeField] private int _remainingTimeBeforeWarning;//оставшееся время ожидания
    [SerializeField] private int _remainingNumberAttempts;//оставшееся колво попыток для сохранения
    public int NumberChance { get; private set; }//колво попыток
    public int NumCurrentWins { get; private set; }//колво правильных ответов

    [Header("Coroutines")]
    private Coroutine _timerToLose;
    private Coroutine _timerToWarning;
    private Coroutine _timerNextAnswer;
    public bool _canNextAnswer = true;

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
        _canNextAnswer = true;
        _mainManager.eventManager.ButtonActionPressedEvent += SelectAction;

        NumCurrentWins = _mainManager.allDataSave.NumberCurrentWins;

        GetRemainingTimeBeforeWarning();
        GetNumberCurrentTotalWins();
        GetRemainingNumberAttempts();
        UpdateTextVins();

        if (_mainManager.allDataSave.NumberChance == 0)
            NumberChance = 1;
        else
            NumberChance = _mainManager.allDataSave.NumberChance;

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
        _textCurrentWins.text = $"{NumCurrentWins} / 10";
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

        if (_numAnswer >= 9)
            _numAnswer = 0;

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
    private IEnumerator DelayNextAnswer(bool isLose)
    {
        _canNextAnswer = false;

        if (isLose)
            _characterHolder.SetTrigger("Win");
        else
            _characterHolder.SetTrigger("Lose");

        float animationDuration = _characterHolder.GetCurrentAnimatorClipInfo(0).Length;

        yield return new WaitForSeconds(animationDuration);
        _canNextAnswer = true;

        Coroutines.StopRoutine(_timerNextAnswer);
    }
    public void AddCurrentWin()//добавление очка к текущим победам
    {
        if (_remainingNumberAttempts > 0)
            NumCurrentWins++;
        else
            PlayAgain();

        _textCurrentWins.text = $"{NumCurrentWins} / 10";
        _mainManager.allDataSave.SaveCurrentWins(NumCurrentWins);
    }
    private void CreateBackground(int numBg)
    {
        _moveBackground.GetImagesAndStartMoving(Resources.Load<Sprite>($"Background Sprites/bg_{numBg}"));
    }
    private void CreateCharacter()
    {
        _characterHolder.runtimeAnimatorController = _characters[_numAnswer];
        Debug.Log($"имя персонажа  {_characterHolder.runtimeAnimatorController.name}");
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
                PlayAgain();
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
        if (_canNextAnswer)
        {
            StopTimers();
            _timerToWarning = Coroutines.StartRoutine(TimeToWarning());
            _remainingTimeBeforeWarning = _timeToWarning;

            if (_remainingNumberAttempts > 0 && NumCurrentWins < 10)
            {
                if (_answers[_numAnswer] == value)
                    CorrectAnswer();
                else
                    _timerNextAnswer = Coroutines.StartRoutine((DelayNextAnswer(false)));

                _remainingNumberAttempts--;
                _numAnswer++;
            }
            if (_remainingNumberAttempts == 0)
                PlayAgain();
        }
    }
    private void PlayAgain()
    {
        StopTimers();

        _obtainedAchievement.CheckLastAchievement(NumberChance, NumCurrentWins);
        _mainManager.panels[(int)PanelsGameScene.CharacterPanel].SetActive(false);
        _mainManager.panels[(int)PanelsGameScene.LosePanel].SetActive(true);
    }
    public void AddChance()
    {
        NumberChance = 2;
        _mainManager.allDataSave.SaveSecondChance(NumberChance);
    }
    private void CorrectAnswer()//при верном ответе
    {
        _timerNextAnswer = Coroutines.StartRoutine((DelayNextAnswer(true)));
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
        _mainManager.allDataSave.SaveSecondChance(NumberChance);
        _mainManager.allDataSave.SaveRemainingTimeBeforeWarning(_remainingTimeBeforeWarning);
        _mainManager.allDataSave.SaveNumberAnswer(_numAnswer);
        _mainManager.allDataSave.SaveCurrentTotalWins(_numberCurrentTotalWins);
        _mainManager.allDataSave.SaveRemainingNumberAttempts(_remainingNumberAttempts);
    }
}
