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
    }
    private void Start()
    {
        Invoke("CheckOpenedAchievements", .1f);
    }
    private void OnEnable()
    {
        if (_dataIsExists)
            CheckOpenedAchievements();
    }
    /// <summary>
    /// Проверка всех достижений и выбор действия по данныйм allDataSave.GoldenUnconfirmedAchievements[i]
    /// </summary>
    public void CheckOpenedAchievements()
    {
        for (int i = 0; i < _goldButtons.Length; i++)
        {
            if (_mainManager.allDataSave.GoldenAchievements[i] == '0')
            {
                _goldButtons[i].UpdateUIAchievement('0');
                _goldButtons[i].NumberOpenedAchievement(-1);
            }
            else if (_mainManager.allDataSave.GoldenAchievements[i] == '1')
            {
                _goldButtons[i].UpdateUIAchievement('1');
                _goldButtons[i].NumberOpenedAchievement(i);
            }
            else if (_mainManager.allDataSave.GoldenAchievements[i] == '2')
            {
                _goldButtons[i].UpdateUIAchievement('2');
            }
        }
        for (int i = 0; i < _silverButtons.Length; i++)
        {
            if (_mainManager.allDataSave.SilverAchievements[i] == '0')
            {
                _silverButtons[i].UpdateUIAchievement('0');
            }
            else if (_mainManager.allDataSave.SilverAchievements[i] == '1')
            {
                _silverButtons[i].UpdateUIAchievement('1');
                _silverButtons[i].NumberOpenedAchievement(i);
            }
            else if (_mainManager.allDataSave.SilverAchievements[i] == '2')
            {
                _silverButtons[i].UpdateUIAchievement('2');
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
