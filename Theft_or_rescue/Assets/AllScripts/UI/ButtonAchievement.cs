using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAchievement : AbstractButton, IPointerDownHandler
{
    [Header("Button Data")]
    [SerializeField] private string _name;
    [SerializeField] private string _description;

    public void OnPointerDown(PointerEventData eventData)
    {
        PressButton();
    }
    private void PressButton()
    {
        base.OpenNewPanel();
    }
}
