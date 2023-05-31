using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonCancelContinuePlaying : AbstractButton, IPointerDownHandler
{
    [Header("ButtonData")]
    [SerializeField] private AllDataSave _allDataSave;
    [SerializeField] private AchievementsManager _achievementsManager;
    [SerializeField] private SceneGameManager _sceneGameManager;
    [SerializeField] private string _nameScene;
    private void Start()
    {
        if (_allDataSave == null)
            _allDataSave = mainManager.allDataSave;

        if (_achievementsManager == null)
            _achievementsManager = mainManager.achievementsManager;

        if (_sceneGameManager == null)
            _sceneGameManager = mainManager.sceneGameManager;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _achievementsManager.CheckAndAddAchievement(_sceneGameManager.NumberChance, _sceneGameManager.NumCurrentWins);
        _allDataSave.AddAndSaveNumberCompletedGames();
        _allDataSave.SaveTotalWins();
        _allDataSave.NullAndSaveLevelData();
        _allDataSave.NullAndSaveArraysData();

        OpenNewPanel();

        if (_nameScene != null && SceneManager.GetActiveScene().name == "Game")
            SceneManager.LoadScene(_nameScene);
    }
    public override void OpenNewPanel()
    {
        base.OpenNewPanel();
    }
}
