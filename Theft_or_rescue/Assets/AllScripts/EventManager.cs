using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance { get; private set; }
    public Action ButtonSelectAvatarPressedEvent;
    public Action<bool> ButtonActionPressedEvent;
    public Action<bool> ButtonsActionInteractableEvent;
    public Action<bool> MoveBackgroundImageEvent;
    public Action<bool> ChangeOpeningAcharacterHolderEvent;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public void ButtonSelectAvatarPressed()
    {
        ButtonSelectAvatarPressedEvent?.Invoke();
    }
    public void ButtonActionPressed(bool value)
    {
        ButtonActionPressedEvent?.Invoke(value);
    }
    public void ButtonsActionInteractable(bool interactable)
    {
        ButtonsActionInteractableEvent?.Invoke(interactable);
    }
    public void MovebackgroundImage(bool canMove)
    {
        MoveBackgroundImageEvent?.Invoke(canMove);
    }
    public void ChangeOpeningAcharacterHolder(bool isHide)
    {
        ChangeOpeningAcharacterHolderEvent?.Invoke(isHide);
    }
}

