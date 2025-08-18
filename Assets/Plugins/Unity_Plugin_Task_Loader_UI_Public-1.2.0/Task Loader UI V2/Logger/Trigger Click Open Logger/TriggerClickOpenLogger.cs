using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Нужен, что бы сообщить о том, что пора включать панель логгера
/// </summary>
public class TriggerClickOpenLogger : MonoBehaviour
{
    public event Action OnClick;

    public void ButtonClick()
    {
        OnClick?.Invoke();
    }
}
