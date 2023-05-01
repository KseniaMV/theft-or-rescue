using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionAchevment : MonoBehaviour
{
    [SerializeField] private LocalizedText _localizedTextLittlePanel;
    [SerializeField] private LocalizedText _localizedTextBigPanel;

    [SerializeField] private Text _textLittlePanel;
    [SerializeField] private Text _textBigPanel;

    //[SerializeField] private ButtonAchievement[] _buttonAchievements;
    [SerializeField] private ButtonAchievement[] _goldButtons;
    [SerializeField] private ButtonAchievement[] _silverButtons;

    [SerializeField] private MainManager _mainManager;

    public bool _dataIsExists;

    private void Awake()
    {
        if (_localizedTextLittlePanel == null)
            _localizedTextLittlePanel = transform.Find("Litle Modal Panel").GetComponentInChildren<LocalizedText>();

        if (_localizedTextBigPanel == null)
            _localizedTextBigPanel = transform.Find("Big Modal Panel").GetComponentInChildren<LocalizedText>();
        
        if (_textLittlePanel == null)
            _textLittlePanel = transform.Find("Litle Modal Panel").GetComponentInChildren<Text>();

        if (_textBigPanel == null)
            _textBigPanel = transform.Find("Big Modal Panel").GetComponentInChildren<Text>();

        //if(_buttonAchievements.Length == 0)
        //_buttonAchievements = GetComponentsInChildren<ButtonAchievement>();

        StartCoroutine(Getda());
        //CheckOpenedAchievements();
    }
    //private IEnumerator GetData()
    //{
    //    yield return new WaitForSeconds(.1f);

    //                        //переделать!!


    //    //_goldButtons = new Button[Resources.LoadAll<Button>("Achievements/Gold").Length];
    //    //_silverButtons = new Button[Resources.LoadAll<Button>("Achievements/Silver").Length];

    //    //for (int i = 0; i < _buttonAchievements.Length; i++)
    //    //{
    //    //    int j = 0;
    //    //    if (_buttonAchievements[i].type == TypeAchievement.Gold)
    //    //    {
    //    //        _goldButtons[j] = _buttonAchievements[i].gameObject.GetComponent<Button>();
    //    //        j++;
    //    //    }
    //    //}

    //    //for (int i = 0; i < _buttonAchievements.Length; i++)
    //    //{
    //    //    int j = 0;
    //    //    if (_buttonAchievements[i].type == TypeAchievement.Silver)
    //    //    {
    //    //        _silverButtons[j] = _buttonAchievements[j].gameObject.GetComponent<Button>();
    //    //        j++;
    //    //    }
    //    //}

    //    CheckOpenedAchievements();
    //    StopCoroutine(GetData());
    //}
    //private void Start()
    //{
    //    StartCoroutine(GetData());
    //}
    private void OnEnable()
    {
        if (_dataIsExists)
            CheckOpenedAchievements();
    }
    private IEnumerator Getda()
    {
        yield return null;
        CheckOpenedAchievements();
    }
    private void CheckOpenedAchievements()
    {
        for (int i = 0; i < AllDataSave.GoldenAchievements.Length; i++)
        {
            if (AllDataSave.GoldenAchievements[i] == '0')
            {
                _goldButtons[i].button.interactable = false;
                _goldButtons[i].enabled = false;
            }
            else if (AllDataSave.GoldenAchievements[i] == '1')
            {
                _goldButtons[i].button.interactable = true;
                _goldButtons[i].enabled = true;
            }
        }

        for (int i = 0; i < AllDataSave.SilverAchievements.Length; i++)
        {
            if (AllDataSave.SilverAchievements[i] == '0')
            {
                _silverButtons[i].button.interactable = false;
                _silverButtons[i].enabled = false;
            }
            else if (AllDataSave.SilverAchievements[i] == '1')
            {
                _silverButtons[i].button.interactable = true;
                _silverButtons[i].enabled = true;
            }
        }

        _dataIsExists = true;
    }
    public void UpdateText(AchievementPanels typePanel, string key)
    {
        if (typePanel == AchievementPanels.LittlePanel)
        {
            _localizedTextLittlePanel.key = key;
            _localizedTextLittlePanel.UpdateText();
            _textLittlePanel = _localizedTextLittlePanel.localizedText;
        }
        else if (typePanel == AchievementPanels.BigPanel)
        {
            _localizedTextBigPanel.key = key;
            _localizedTextBigPanel.UpdateText();
            _textBigPanel = _localizedTextBigPanel.localizedText;
        }
    }
}
