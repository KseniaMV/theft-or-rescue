using UnityEngine.EventSystems;

public class ButtonPlay : AbstractButton, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        CheckSelectedAvatar();
        base.OpenNewPanel();
    }
    public void CheckSelectedAvatar()
    {
        if (mainManager.allDataSave.NumberAvatar == 0)
            mainManager.CheckSelectedAvatar(false);
        else
            mainManager.CheckSelectedAvatar(true);
    }
}
