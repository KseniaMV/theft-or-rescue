using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public Action ButtonSelectAvatarPressedEvent;
    public void ButtonSelectAvatarPressed()
    {
        ButtonSelectAvatarPressedEvent?.Invoke();
    }
}
