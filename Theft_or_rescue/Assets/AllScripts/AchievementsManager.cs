using UnityEngine;

public enum TypeAchievement { Null, Gold, Silver }
public enum AchievementPanels { NoAchievementsPanel, LittlePanel, BigPanel}
public class AchievementsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _panels;
    [SerializeField] private MainManager _mainManager;
    public DescriptionAchevment descriptionAchevment;
    private int _dataAchievement;
    private bool _dataIsExists;
    private const string NULL_ACHEVEMENT = "00000000000";//равен колву достижений 1го типа
    private string _goldenAchievements;
    private string _silverAchievements;
   
    public void OpenPanel(bool isOpen, AchievementPanels panel)
    {
        if(_panels != null)
            _panels[(int)panel].SetActive(isOpen);
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
        bool isGoldAchiev = false;
        bool isSilverAchev = false;

        if (_mainManager.allDataSave.GoldenAchievements == null)
        {
            _goldenAchievements = NULL_ACHEVEMENT;
            _mainManager.allDataSave.SaveGoldenAchievement(_goldenAchievements);
        }
        else
        {
            _goldenAchievements = _mainManager.allDataSave.GoldenAchievements;
            isGoldAchiev = true;
        }

        if (_mainManager.allDataSave.SilverAchievements == null)
        {
            _silverAchievements = NULL_ACHEVEMENT;
            _mainManager.allDataSave.SaveSilverAchievement(_silverAchievements);
        }
        else
        { 
            _silverAchievements = _mainManager.allDataSave.SilverAchievements;
            isSilverAchev = true;
        }

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu" & !isGoldAchiev && !isSilverAchev)
            OpenPanel(true, (int)AchievementPanels.NoAchievementsPanel);
        else if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu")
            OpenPanel(false, (int)AchievementPanels.NoAchievementsPanel);

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
}
