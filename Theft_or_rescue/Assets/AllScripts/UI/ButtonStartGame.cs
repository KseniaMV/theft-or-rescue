using UnityEngine.EventSystems;
using UnityEngine;
public class ButtonStartGame : AbstractButton, IPointerDownHandler
{
    [Header("ButtonData")]
    [SerializeField] private bool _createNewLevelData;
    [SerializeField] private FadeScreen _fadeScreen;
    private void OnEnable()
    {
        if (_fadeScreen == null)
            _fadeScreen = GameObject.FindGameObjectWithTag("FadeScreen").GetComponent<FadeScreen>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        mainManager.options.ClickAudio();

        if(_createNewLevelData)
            mainManager.CreateDataLevel();
        else
            mainManager.allDataSave.NullAdnSaveLevelData();

        mainManager.allDataSave.SaveAll();

        if (mainManager.allDataSave.NumberAvatar != 0)
            _fadeScreen.StatFadeScreen();
    }
}
