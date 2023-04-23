using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonContinueGame : AbstractButton, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        mainManager.sceneGameManager.ContinueGame();
    }
    private void LaunchingAds()//запуск рекламы
    { 
    
    }
}
