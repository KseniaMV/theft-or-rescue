using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAction : AbstractButton, IPointerDownHandler
{
    [Header("Button Data")]
    [SerializeField] private Actions _actions;
    public void OnPointerDown(PointerEventData eventData)
    {
        mainManager.options.ClickAudio();
        mainManager.eventManager.ButtonActionPressed((int)_actions);
    }
}
