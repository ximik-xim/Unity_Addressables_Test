using System.Collections.Generic;
using UnityEngine;

public class ExampleOnChangedCallClassMonobeh : MonoBehaviour
{

    [SerializeField] 
    private ExampleOnChangedCallClass1 _exampleOnChangedCallClass1 = new ExampleOnChangedCallClass1();

    [SerializeField][HideInInspector]
    private bool _init = false;

   private void OnValidate()
    {
        Init();
    }
    
    private void Init()
    {
        if (_init == false)
        {
            _exampleOnChangedCallClass1.SetMethodName(nameof(_exampleOnChangedCallClass1.OnValueChangeLog1) );
            _init = true;
        }
        
    }
    
    private void Reset()
    {
        _init = false;
        Init();
    }
}



[System.Serializable]
public class ExampleOnChangedCallClass2
{
    [SerializeField] 
    private OnChangedCallClass<List<int>> _listInt ;

    public void SetMethodName( string name)
    {
        _listInt.MethodName = name;
    }
    
    
    [SerializeField] private int _blaIntt;
    public void OnValueChangeLog2()
    {
        Debug.Log("Class 2");
    }
}

[System.Serializable]
public class ExampleOnChangedCallClass1
{


    [SerializeField] 
    private OnChangedCallClass<List<ExampleOnChangedCallClass2>> _listCallClass2 = new OnChangedCallClass<List<ExampleOnChangedCallClass2>>(); 

    public void SetMethodName( string name)
    {
        _listCallClass2.MethodName = name;

         foreach (var VARIABLE in _listCallClass2.Data)
         {
             VARIABLE.SetMethodName(nameof(VARIABLE.OnValueChangeLog2));
         }
    }
    
    public void OnValueChangeLog1()
    {
        
        foreach (var VARIABLE in _listCallClass2.Data)
        {
            VARIABLE.SetMethodName(nameof(VARIABLE.OnValueChangeLog2));
        }
        
        Debug.Log("Class 1");
    }
}
