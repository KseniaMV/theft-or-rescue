using UnityEngine;
using UnityEngine.UI;

public class DescriptionAchevment : MonoBehaviour
{
    [SerializeField] private LocalizedText _localizedTextLittlePanel;
    [SerializeField] private LocalizedText _localizedTextBigPanel;

    [SerializeField] private Text _textLittlePanel;
    [SerializeField] private Text _textBigPanel;

    [SerializeField] private ButtonAchievement[] _goldButtons;
    [SerializeField] private ButtonAchievement[] _silverButtons;

    [SerializeField] private MainManager _mainManager;

    private bool _dataIsExists;

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

        ButtonAchievement[] buttonAchievements = null; 

        if (_goldButtons.Length == 0)
        {
            _goldButtons = new ButtonAchievement[Resources.LoadAll<ButtonAchievement>("Achievements/Gold").Length];
            buttonAchievements = transform.Find("Scroll View/Viewport/Content/Achievements Holder").GetComponentsInChildren<ButtonAchievement>();

            int j = 0;
            for (int i = 0; i < buttonAchievements.Length; i++)
            {
                if (buttonAchievements[i].type == TypeAchievement.Gold)
                {
                    _goldButtons[j] = buttonAchievements[i];
                    j++;
                }
            }
        }

        if (_silverButtons.Length == 0)
        {
            _silverButtons = new ButtonAchievement[Resources.LoadAll<ButtonAchievement>("Achievements/Silver").Length];
            buttonAchievements = transform.Find("Scroll View/Viewport/Content/Achievements Holder").GetComponentsInChildren<ButtonAchievement>();

            int j = 0;
            for (int i = 0; i < buttonAchievements.Length; i++)
            {
                if (buttonAchievements[i].type == TypeAchievement.Silver)
                {
                    _silverButtons[j] = buttonAchievements[i];
                    j++;
                }
            }
        }
    }
    private void Start()
    {
        CheckOpenedAchievements();
    }
    private void OnEnable()
    {
        if (_dataIsExists)
            CheckOpenedAchievements();
    }
    private void CheckOpenedAchievements()
    {
        for (int i = 0; i < _mainManager.allDataSave.GoldenAchievements.Length; i++)
        {
            if (_mainManager.allDataSave.GoldenAchievements[i] == '0')
            {
                _goldButtons[i].button.interactable = false;
                _goldButtons[i].enabled = false;
            }
            else if (_mainManager.allDataSave.GoldenAchievements[i] == '1')
            {
                _goldButtons[i].button.interactable = true;
                _goldButtons[i].enabled = true;
            }
        }

        for (int i = 0; i < _mainManager.allDataSave.SilverAchievements.Length; i++)
        {
            if (_mainManager.allDataSave.SilverAchievements[i] == '0')
            {
                _silverButtons[i].button.interactable = false;
                _silverButtons[i].enabled = false;
            }
            else if (_mainManager.allDataSave.SilverAchievements[i] == '1')
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
