using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAction : AbstractButton, IPointerDownHandler
{
    [Header("Button Data")]
    [SerializeField] private bool _answer;      //thief = false; resc = true;
    public void OnPointerDown(PointerEventData eventData)
    {
        mainManager.options.ClickAudio(true);
        mainManager.eventManager.ButtonActionPressed(_answer);
    }
}
