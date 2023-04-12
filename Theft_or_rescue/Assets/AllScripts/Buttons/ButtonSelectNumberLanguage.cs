using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectNumberLanguage : AbstractButton, IPointerDownHandler
{
    [Header("Button Data")]
    [Range(1,10)][SerializeField] private int _numberLanguage;
    public void OnPointerDown(PointerEventData eventData)
    {
        mainManager.SelectNumberLanguage(_numberLanguage);
        base.OpenNewPanel();
    }
}
