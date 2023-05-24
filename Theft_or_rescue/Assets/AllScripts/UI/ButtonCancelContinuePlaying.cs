using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonCancelContinuePlaying : AbstractButton, IPointerDownHandler
{
    [Header("ButtonData")]
    [SerializeField] private AllDataSave _allDataSave;

    private void Start()
    {
        if (_allDataSave == null)
            _allDataSave = mainManager.allDataSave;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _allDataSave.AddAndSaveNumberCompletedGames();
        _allDataSave.SaveTotalWins();
        _allDataSave.NullAdnSaveLevelData();
        _allDataSave.NullAndSaveArraysData();
        
        OpenNewPanel();
    }
    public override void OpenNewPanel()
    {
        base.OpenNewPanel();
    }
}
