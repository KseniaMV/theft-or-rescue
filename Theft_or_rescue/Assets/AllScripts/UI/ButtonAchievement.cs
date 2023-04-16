using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAchievement : AbstractButton, IPointerDownHandler
{
    [Header("Button Data")]
    [SerializeField] private string _name;
    [SerializeField] private string _description;

    //[SerializeField] private GameObject modalPanel;
    public void OnPointerDown(PointerEventData eventData)
    {
        PressButton();
    }
    private void PressButton()
    {
        base.OpenNewPanel();
    }
}
