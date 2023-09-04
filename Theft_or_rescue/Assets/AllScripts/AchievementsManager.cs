using UnityEngine;
public enum TypeAchievement { Null, Gold, Silver }
public enum AchievementPanels { SilverPanel, GoldenPanel}
public class AchievementsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _panels;
    [SerializeField] private MainManager _mainManager;
    private int _dataAchievement;
    private bool _dataIsExists;
    private const string NULL_ACHEVEMENT = "00000000000";//равен колву достижений 1го типа
    private string _goldenAchievements;
    private string _silverAchievements;

    [Header("ModalPanel")]
    private GameObject _selectedPanel;
    private LocalizedText _localText;

    public void OpenPanel(AchievementPanels panel, bool isOverOthers = false)
    {
        if (_panels != null)
        {
            _selectedPanel = _panels[(int)panel];

            _selectedPanel.SetActive(true);

            if (isOverOthers)
            {
                int maxIndex = _selectedPanel.transform.parent.childCount;
                _selectedPanel.transform.SetSiblingIndex(maxIndex);
            }
        }
    }
    public void TranslateTextPanel(string key)
    {
        _localText = _selectedPanel.GetComponentInChildren<LocalizedText>();

        if (key != null)
        {
            _localText.key = key;
            _localText.UpdateText();
        } 
    }
    private void Start()
    {
        LoadOrCreateDataAchievements();
    }
    private void OnEnable()
    {
        if (_dataIsExists)
            LoadOrCreateDataAchievements();
    }
    private void LoadOrCreateDataAchievements()
    {
        if (_mainManager.allDataSave.GoldenAchievements == null)
        {
            _goldenAchievements = NULL_ACHEVEMENT;
            _mainManager.allDataSave.SaveGoldenAchievement(_goldenAchievements);
        }
        else
            _goldenAchievements = _mainManager.allDataSave.GoldenAchievements;

        if (_mainManager.allDataSave.SilverAchievements == null)
        {
            _silverAchievements = NULL_ACHEVEMENT;
            _mainManager.allDataSave.SaveSilverAchievement(_silverAchievements);
        }
        else
            _silverAchievements = _mainManager.allDataSave.SilverAchievements;

        _dataIsExists = true;
    }
    private void RewriteDataAchievementAndSave(TypeAchievement type, int number)
    {
        char[] data = null;
        char openAchievement = '1';
        int numTypeLastAchiev = 0;

        if (type == TypeAchievement.Gold)
        {
            data = _goldenAchievements.ToCharArray();
            data[number] = openAchievement;
            _goldenAchievements = new string(data);
            numTypeLastAchiev = 1;
            _mainManager.allDataSave.SaveGoldenAchievement(_goldenAchievements);
        }
        else if (type == TypeAchievement.Silver)
        {
            data = _silverAchievements.ToCharArray();
            data[number] = openAchievement;
            _silverAchievements = new string(data);
            numTypeLastAchiev = 2;
            _mainManager.allDataSave.SaveSilverAchievement(_silverAchievements);
        }

        SaveLastAchievement(numTypeLastAchiev, number);
    }
    private void CheckAchiev(TypeAchievement type, int checkNumAchiev)
    {
        char[] dataAchievements = null;

        if (type == TypeAchievement.Gold)
            dataAchievements = _goldenAchievements.ToCharArray();
        else if (type == TypeAchievement.Silver)
            dataAchievements = _silverAchievements.ToCharArray();

        _dataAchievement = int.Parse(dataAchievements[checkNumAchiev].ToString());
    }
    public void CheckAndAddAchievement(int numberTry, int numberAchievement)
    {
        TypeAchievement type = TypeAchievement.Null;

        if (numberTry == 1)
            type = TypeAchievement.Gold;
        else if (numberTry == 2)
            type = TypeAchievement.Silver;

        CheckAchiev(type, numberAchievement);

        if (_dataAchievement == 0)
            RewriteDataAchievementAndSave(type, numberAchievement);
    }
    private void SaveLastAchievement(int type, int number)
    {
        _mainManager.allDataSave.SaveLastAcvievement($"{type}_{number}");
    }
    public void UndoLactAchievement()
    {
        string info = _mainManager.allDataSave.LastCurrentAchievement;

        if (info != null)
        {
            string typeInfo = info.Split("_")[0];
            int type = int.Parse(typeInfo);
            string numInfo = info.Split("_")[1];
            int num = int.Parse(numInfo);

            if (type == 1)
            {
                string line = _mainManager.allDataSave.GoldenAchievements;
                char[] achievements = line.ToCharArray();
                achievements[num] = '0';
                line = new string(achievements);
                _mainManager.allDataSave.SaveGoldenAchievement(line);
            }
            else if (type == 2)
            {
                string line = _mainManager.allDataSave.SilverAchievements;
                char[] achievements = line.ToCharArray();
                achievements[num] = '0';
                line = new string(achievements);
                _mainManager.allDataSave.SaveSilverAchievement(line);
            }

            _mainManager.allDataSave.SaveLastAcvievement(null);
        }
    }
}
