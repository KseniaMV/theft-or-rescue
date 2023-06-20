using UnityEngine;
using UnityEngine.SceneManagement;
public enum PanelsMainMenuScene {LangaugePanel = 0, StartPanel, LoginPanel, InfoPanel, OptionsPanel, AchievementsPanel }
public class MainManager : MonoBehaviour
{
    public static MainManager instance { get; private set; }

    public Options options;
    public EventManager eventManager;
    public AllDataSave allDataSave;
    public LocalizationManager localizationManager;
    public SceneGameManager sceneGameManager;
    public AchievementsManager achievementsManager;

    [Header("StartPanel")]
    public GameObject[] panels;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        if (!transform.parent.gameObject.activeSelf)
            transform.parent.gameObject.SetActive(true);

        if (options == null && transform.parent.GetComponentInChildren<Options>())
            options = transform.parent.GetComponentInChildren<Options>();

        if (eventManager == null && transform.parent.GetComponentInChildren<EventManager>())
            eventManager = transform.parent.GetComponentInChildren<EventManager>();

        if (allDataSave == null && transform.parent.GetComponentInChildren<AllDataSave>())
            allDataSave = transform.parent.GetComponentInChildren<AllDataSave>();

        if (localizationManager == null && transform.parent.GetComponentInChildren<LocalizationManager>())
            localizationManager = transform.parent.GetComponentInChildren<LocalizationManager>();

        if (sceneGameManager == null && transform.parent.GetComponentInChildren<SceneGameManager>())
            sceneGameManager = transform.parent.GetComponentInChildren<SceneGameManager>();

        if (achievementsManager == null && transform.parent.GetComponentInChildren<AchievementsManager>())
            achievementsManager = transform.parent.GetComponentInChildren<AchievementsManager>();
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            CheckSelectedLanguage();
            CheckObtainedAchievement();
        }
    }
    private void CheckObtainedAchievement()
    {
        //if (allDataSave.Answers != null)
        //{
        //    panels[(int)PanelsMainMenuScene.AchievementsPanel].SetActive(true);
        //    //panels[(int)PanelsMainMenuScene.InfoObtainedAchievement].SetActive(true);
        //}
        //else
            panels[((int)PanelsMainMenuScene.StartPanel)].SetActive(true);
    }
    private void CheckSelectedLanguage()
    {
        if (!PlayerPrefs.HasKey("Language"))
            panels[((int)PanelsMainMenuScene.LangaugePanel)].SetActive(true);
    }
    public void CheckSelectedAvatar(bool isSelected)
    {
        if(isSelected)
            panels[(int)PanelsMainMenuScene.InfoPanel].SetActive(true);
        else
            panels[(int)PanelsMainMenuScene.LoginPanel].SetActive(true);
    }
    public void CreateDataLevel()
    {
        int[] characters = new int[10];
        int[] things = new int[10];
        bool[] answers = new bool[10];

        CreateDataLevelCharacters(characters);
        CreateDataLevelAnswers(answers);
        CreateDataLevelThings(things);

        allDataSave.SaveNumberAnswer(0);
        allDataSave.SaveDataLevel(characters, things, answers, GetNumberBackground());
    }
    private int GetNumberBackground()
    {
        int numBg = Random.Range(1, Resources.LoadAll<Sprite>("Background Sprites").Length + 1);

        return numBg;
    }
    private int[] CreateDataLevelCharacters(int[] characters)
    {
        GameObject[] sprites = Resources.LoadAll<GameObject>("Characters");

        for (int i = 0; i < characters.Length; i++)
        {
            int rand = Random.Range(1, sprites.Length +1);
            characters[i] = rand;
        }

        return characters;
    }
    private int[] CreateDataLevelThings(int[] things)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Things");

        for (int i = 0; i < things.Length; i++)
        {
            int rand = Random.Range(1, sprites.Length + 1);
            things[i] = rand;
        }

        return things;
    }
    private bool[] CreateDataLevelAnswers(bool[] answers)
    {
        for (int i = 0; i < answers.Length; i++)
        {
            int rand = Random.Range(0, 2);

            if (rand == 0)
                answers[i] = true;
            else
                answers[i] = false;
        }

        return answers;
    }
}
