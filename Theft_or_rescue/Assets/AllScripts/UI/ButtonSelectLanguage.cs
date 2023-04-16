using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonSelectLanguage : AbstractButton, IPointerDownHandler
{
    [SerializeField] private string _language;
    public void OnPointerDown(PointerEventData eventData)
    {
        SwitchLanguage();
        base.OpenNewPanel();
    }
    private void SwitchLanguage()
    {
        mainManager.localizationManager.CurrentLanguage = _language;
        mainManager.localizationManager.LoadLocalizedText(_language);
    }
}
