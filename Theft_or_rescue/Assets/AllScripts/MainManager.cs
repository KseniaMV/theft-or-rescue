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

        for (int i = 0; i < panels.Length; i++)
            if(panels[i] != null)
                panels[i].SetActive(false);
    }
    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
            CheckSelectedLanguage();
    }
    private void CheckSelectedLanguage()
    {
        if (!PlayerPrefs.HasKey("Language"))
            panels[((int)PanelsMainMenuScene.LangaugePanel)].SetActive(true);
        else
            panels[((int)PanelsMainMenuScene.StartPanel)].SetActive(true);
    }
    public void CheckSelectedAvatar(bool isSelected)
    {
        if(isSelected)
            panels[(int)PanelsMainMenuScene.InfoPanel].SetActive(true);
        else
            panels[(int)PanelsMainMenuScene.LoginPanel].SetActive(true);
    }
}
