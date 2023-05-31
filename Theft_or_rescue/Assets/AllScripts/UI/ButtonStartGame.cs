using UnityEngine.EventSystems;
using UnityEngine;
public class ButtonStartGame : AbstractButton, IPointerDownHandler
{
    [Header("ButtonData")]
    [SerializeField] private bool _isGameScene;
    [SerializeField] private FadeScreen _fadeScreen;
    [SerializeField] private AchievementsManager _achievementsManager;
    private void Awake()
    {
        if (_fadeScreen == null)
            _fadeScreen = GameObject.FindGameObjectWithTag("FadeScreen").GetComponent<FadeScreen>();

        if (_achievementsManager == false)
            _achievementsManager = GameObject.FindGameObjectWithTag("MainManager").transform.parent.GetComponentInChildren<AchievementsManager>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        mainManager.options.ClickAudio();
        mainManager.allDataSave.NullAndSaveArraysData();
        mainManager.allDataSave.NullAndSaveLevelData();
        _achievementsManager.UndoLactAchievement();
        mainManager.CreateDataLevel();
        mainManager.allDataSave.SaveAll();

        if (_isGameScene)
            mainManager.sceneGameManager.AddChance();

        if (mainManager.allDataSave.NumberAvatar != 0)
            _fadeScreen.StatFadeScreen();
    }
}
