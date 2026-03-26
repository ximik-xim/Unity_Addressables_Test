using System;
using UnityEngine;

/// <summary>
/// (недоконца заверншен. Нужно еще продумать, и дописать логику для поиска данных у родителя и для возможности вызывать приватные методы, как у атрибута)
/// Этот класс нужен, когда нужно отследить изменения данных в классе через инспектор. И при обноружении изменений в классе вызвать метод, который уазыветься через метод MethodName
/// </summary>
[System.Serializable]
public class OnChangedCallClass<Datad> : AbstOnValuCall where Datad : new()
{

    //Имя поля не менять!
    [SerializeField] [HideInInspector] 
    private string _methodName = String.Empty;

    public string MethodName
    {
        get
        {
            return _methodName;
        }
        set
        {
            if (_methodName != value)
            {
                _methodName = value;
            }
        }
    }
    
    public OnChangedCallClass()
    {
        
    }

    // public OnChangedCallClass(string methodName = null, Action action = null)
    // {
    //     if (methodName!=null)
    //     {
    //         _methodName = methodName;    
    //     }
    //
    //     if (action != null)
    //     {
    //         OnValueChangeUpdate += action;    
    //     }
    //
    //     Debug.Log("USE CONSTRUCTOR = " + this.GetHashCode());
    //     Debug.Log("USE CONSTRUCTOR " + "| MethodName = " + _methodName);
    // }
    
    
    public event Action OnValueChangeUpdate;
    
    public Datad Data => _data;
    
    [SerializeField] 
    private Datad _data = new Datad();
    
    public virtual void OnValueChange()
    {
        OnValueChangeUpdate?.Invoke();
    }
}
