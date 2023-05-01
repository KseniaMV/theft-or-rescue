using UnityEngine;

public enum TypeAchievement { Null, Gold, Silver }
public enum AchievementPanels { NoAchievementsPanel, LittlePanel, BigPanel}
public class AchievementsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _panels;
    [SerializeField] private MainManager _mainManager;
    [SerializeField] private bool _isGameScene;
    public DescriptionAchevment descriptionAchevment;
    private int _dataAchievement;
    private bool _dataIsExists;
    private const string NULL_ACHEVEMENT = "00000000000";
    public string GoldenAchievements;// { get; private set; }
    public string SilverAchievements;// { get; private set; }
   
    public void OpenPanel(bool isOpen, AchievementPanels panel)
    {
        if(_panels != null)
            _panels[(int)panel].SetActive(isOpen);
    }
    private void Start()
    {
        //if (_descriptionAchevment == null)
        //    _mainManager.panels[(int)PanelsMainMenuScene.AchievementsPanel].GetComponent<DescriptionAchevment>();

        Invoke("LoadOrCreateDataAchievements", .2f);
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

        if (AllDataSave.GoldenAchievements == null)
        {
            GoldenAchievements = NULL_ACHEVEMENT;
            _mainManager.allDataSave.SaveGoldenAchievement(GoldenAchievements);
        }
        else
        {
            GoldenAchievements = AllDataSave.GoldenAchievements;
            isGoldAchiev = true;
        }

        if (AllDataSave.SilverAchievements == null)
        {
            SilverAchievements = NULL_ACHEVEMENT;
            _mainManager.allDataSave.SaveSilverAchievement(SilverAchievements);
        }
        else
        {
            SilverAchievements = AllDataSave.SilverAchievements;
            isSilverAchev = true;
        }

        if (isGoldAchiev && isSilverAchev && !_isGameScene)
            OpenPanel(true, (int)AchievementPanels.NoAchievementsPanel);

        _dataIsExists = true;
    }
    private void RewriteDataAchievementAndSave(TypeAchievement type, int number)
    {
        char[] data = null;
        char openAchievement = '1';

        if (type == TypeAchievement.Gold)
        {
            data = GoldenAchievements.ToCharArray();
            data[number] = openAchievement;
            GoldenAchievements = new string(data);
            _mainManager.allDataSave.SaveGoldenAchievement(GoldenAchievements);
        }
        else if (type == TypeAchievement.Silver)
        {
            data = SilverAchievements.ToCharArray();
            data[number] = openAchievement;
            SilverAchievements = new string(data);
            _mainManager.allDataSave.SaveSilverAchievement(SilverAchievements);
        }
    }
    private void CheckAchiev(TypeAchievement type, int checkNumAchiev)
    {
        char[] dataAchievements = null;

        if (type == TypeAchievement.Gold)
            dataAchievements = GoldenAchievements.ToCharArray();
        else if (type == TypeAchievement.Silver)
            dataAchievements = SilverAchievements.ToCharArray();

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
}
