using UnityEngine;
using UnityEngine.SceneManagement;
public enum Panels {LangaugePanel = 0, StartPanel, LoginPanel, InfoPanel, LoadingPanel, OptionsPanel, AchievementsPanel }
public class MainManager : MonoBehaviour
{
    public static MainManager instance { get; private set; }

    public Options options;
    public EventManager eventManager;
    public AllDataSave allDataSave;
    public LocalizationManager localizationManager;
    public SceneGameManager sceneGameManager;

    [Header("StartPanel")]
    [SerializeField] private GameObject[] _panels;

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

        for (int i = 0; i < _panels.Length; i++)
            if(_panels[i] != null)
                _panels[i].SetActive(false);
    }
    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
            CheckSelectedLanguage();
    }
    private void CheckSelectedLanguage()
    {
        if (!PlayerPrefs.HasKey("Language"))
            _panels[((int)Panels.LangaugePanel)].SetActive(true);
        else
            _panels[((int)Panels.StartPanel)].SetActive(true);
    }
    public void CheckSelectedAvatar(bool isSelected)
    {
        if(isSelected)
            _panels[(int)Panels.InfoPanel].SetActive(true);
        else
            _panels[(int)Panels.LoginPanel].SetActive(true);
    }
}
