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

        string typeAch = null;

        if (_sceneGameManager.NumberChance == 1)
            typeAch = "Gold_";
        else
            typeAch = "Silver_";

        _allDataSave.SaveNameTotalLastAchievement(typeAch+_sceneGameManager.NumCurrentWins);
        _allDataSave.AddAndSaveNumberCompletedGames();
        _allDataSave.SaveTotalWins();
        _allDataSave.NullAndSaveLevelData();
        _allDataSave.NullAndSaveArraysData();
        _allDataSave.SaveIsOutOfGame(true);

        base.OpenNewPanel();

        if (_nameScene != null && SceneManager.GetActiveScene().name == "Game")
            SceneManager.LoadScene(_nameScene);
    }
}
