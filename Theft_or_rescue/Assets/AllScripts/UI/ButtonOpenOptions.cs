using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonOpenOptions : AbstractButton, IPointerDownHandler
{
    public static bool IsOpened;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            ChangeOpeningCharacterHolder();

            base.OpenNewPanel(false);
        }
        else
            base.OpenNewPanel();
    }
    public void ChangeOpeningCharacterHolder()
    {
        if (!IsOpened)
        {
            IsOpened = true;
            mainManager.eventManager.ChangeOpeningAcharacterHolder(true);
        }
        else
        {
            IsOpened = false;
            mainManager.eventManager.ChangeOpeningAcharacterHolder(false);
        }
    }
}
