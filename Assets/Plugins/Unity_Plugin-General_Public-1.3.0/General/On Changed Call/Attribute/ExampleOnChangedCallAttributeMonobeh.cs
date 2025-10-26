using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Пример использования атрибута для отслеживания изменения данных в классе через инспектор. 
/// </summary>
public class ExampleOnChangedCallAttributeMonobeh : MonoBehaviour
{
    [SerializeField]
    [OnChangedCall(nameof(OnValueChangeLog1))]
    private ExampleOnChangedCallAttribute1 _testClass;


    //Метод может быть публичным и приватным
    public void OnValueChangeLog1()
    {
        Debug.Log("Class 1");
    }
}

[System.Serializable]
public class ExampleOnChangedCallAttribute1
{
    [SerializeField]
   private List<int> _list;

    [SerializeField] 
    private int _intVal;

    [OnChangedCall(nameof(OnValueChangeLog2))]
    [SerializeField] private ExampleOnChangedCallAttribute2 _ClassVal;

    public void OnValueChangeLog2()
    {
        Debug.Log("Class 2");
    }
}

[System.Serializable]
public class ExampleOnChangedCallAttribute2
{
    [SerializeField] private int _intVal;
}



