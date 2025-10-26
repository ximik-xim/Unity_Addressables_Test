using System;
using UnityEngine;

/// <summary>
/// Этот атрибут нужен, когда нужно отследить изменения данных в классе через инспектор. И при обноружении изменений в классе вызвать метод, который уазыветься в аргументах атрибута
/// пр [OnChangedCall(nameof(OnValueChangeLog1))], где OnValueChangeLog1 это метод 
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class OnChangedCallAttribute : PropertyAttribute //Attribute 
{
    public string methodName;
    public OnChangedCallAttribute(string methodNameNoArguments)
    {
        methodName = methodNameNoArguments;
    }
}
