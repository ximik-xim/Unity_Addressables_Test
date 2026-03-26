using System;
using UnityEngine;

public abstract class AbsTriggerOpenKeyboardMobile : MonoBehaviour
{
    public abstract bool KeyboardIsVisible { get; }
    
    public abstract event Action<bool> OnUpdateStatusKeyboardIsVisible;
}
