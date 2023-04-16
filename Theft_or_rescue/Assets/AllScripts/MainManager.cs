using UnityEngine;

public enum Panels {LangaugePanel = 0, StartPanel, LoginPanel, InfoPanel, LoadingPanel, OptionsPanel, AchievementsPanel }
public class MainManager : MonoBehaviour
{
    public static MainManager instance { get; private set; }

    public Options options;
    public EventManager eventManager;
    public AllDataSave allDataSave;
    public LocalizationManager localizationManager;

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

        if (options == null)
            options = transform.parent.GetComponentInChildren<Options>();

        if (eventManager == null)
            eventManager = transform.parent.GetComponentInChildren<EventManager>();

        if (allDataSave == null)
            allDataSave = transform.parent.GetComponentInChildren<AllDataSave>();

        if (localizationManager == null)
            localizationManager = transform.parent.GetComponentInChildren<LocalizationManager>();

        for (int i = 0; i < _panels.Length; i++)
            _panels[i].SetActive(false);
    }
    private void Start()
    {
        CheckSelectedLanguage();
    }
    private void CheckSelectedLanguage()
    {
        if (AllDataSave.NumberLanguage == 0)
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
