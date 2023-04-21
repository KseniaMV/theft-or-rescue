using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance { get; private set; }
    public Action ButtonSelectAvatarPressedEvent;
    public Action<int> ButtonActionPressedEvent;
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
    public void ButtonActionPressed(int number)
    {
        ButtonActionPressedEvent?.Invoke(number);
    }
}
