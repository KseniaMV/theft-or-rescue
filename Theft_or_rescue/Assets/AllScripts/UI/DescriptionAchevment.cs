using UnityEngine;
using UnityEngine.UI;

public class DescriptionAchevment : MonoBehaviour
{
    [SerializeField] private LocalizedText _localizedTextSilverPanel;
    [SerializeField] private LocalizedText _localizedTextGoldenPanel;

    [SerializeField] private Text _texSilverPanel;
    [SerializeField] private Text _textGoldenPanel;

    [SerializeField] private ButtonAchievement[] _goldButtons;
    [SerializeField] private ButtonAchievement[] _silverButtons;

    [SerializeField] private MainManager _mainManager;

    private bool _dataIsExists;

    private void Awake()
    {
        if (_localizedTextSilverPanel == null)
            _localizedTextSilverPanel = transform.Find("Silver Modal Panel").GetComponentInChildren<LocalizedText>();

        if (_localizedTextGoldenPanel == null)
            _localizedTextGoldenPanel = transform.Find("Golden Modal Panel").GetComponentInChildren<LocalizedText>();
        
        if (_texSilverPanel == null)
            _texSilverPanel = transform.Find("Silver Modal Panel").GetComponentInChildren<Text>();

        if (_textGoldenPanel == null)
            _textGoldenPanel = transform.Find("Golden Modal Panel").GetComponentInChildren<Text>();
    }
    private void Start()
    {
        Invoke("CheckOpenedAchievements", .05f);
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
        if (typePanel == AchievementPanels.SilverPanel)
        {
            _localizedTextSilverPanel.key = key;
            _localizedTextSilverPanel.UpdateText();
            _texSilverPanel = _localizedTextSilverPanel.localizedText;
        }
        else if (typePanel == AchievementPanels.GoldenPanel)
        {
            _localizedTextGoldenPanel.key = key;
            _localizedTextGoldenPanel.UpdateText();
            _textGoldenPanel = _localizedTextGoldenPanel.localizedText;
        }
    }
}
