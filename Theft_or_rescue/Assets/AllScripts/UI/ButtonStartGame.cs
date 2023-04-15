using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonStartGame : AbstractButton, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        mainManager.options.ClickAudio();

        mainManager.allDataSave.SaveAll();

        if (AllDataSave.NumberAvatar != 0)
            SceneManager.LoadScene("Game");
    }
}
