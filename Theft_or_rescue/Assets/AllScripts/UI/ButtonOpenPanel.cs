using UnityEngine.EventSystems;

public class ButtonOpenPanel : AbstractButton, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        base.OpenNewPanel();
    }
}
