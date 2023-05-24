using UnityEngine.EventSystems;
using UnityEngine;
public class ButtonStartGame : AbstractButton, IPointerDownHandler
{
    [Header("ButtonData")]
    [SerializeField] private bool _createNewLevelData;
    [SerializeField] private FadeScreen _fadeScreen;
    [SerializeField] private AchievementsManager _achievementsManager;
    private void Awake()
    {
        if (_fadeScreen == null)
            _fadeScreen = GameObject.FindGameObjectWithTag("FadeScreen").GetComponent<FadeScreen>();

        if (_createNewLevelData == false)
            _achievementsManager = GameObject.FindGameObjectWithTag("MainManager").transform.parent.GetComponentInChildren<AchievementsManager>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        mainManager.options.ClickAudio();

        if (_createNewLevelData)
        {
            mainManager.allDataSave.NullAndSaveArraysData();
            mainManager.allDataSave.NullAdnSaveLevelData();
            mainManager.CreateDataLevel();
        }
        else
        {
            _achievementsManager.UndoLactAchievement();
            mainManager.allDataSave.NullAdnSaveLevelData();
        }

        mainManager.allDataSave.SaveAll();

        if (mainManager.allDataSave.NumberAvatar != 0)
            _fadeScreen.StatFadeScreen();
    }
}
