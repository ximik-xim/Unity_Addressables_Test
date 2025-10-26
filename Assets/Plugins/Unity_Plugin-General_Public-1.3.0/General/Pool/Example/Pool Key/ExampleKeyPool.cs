using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleKeyPool : MonoBehaviour
{
    [SerializeField]
    private CustomPoolKey<TestPoolKey, ExampleElementKeyMono> _examplePoolKey;

    [SerializeField] 
    private List<ExampleElementKeyMono> _prefabs;

    [SerializeField] 
    private TestPoolKey _keyCreateTestElement;
    private void OnEnable()
    {
#if UNITY_EDITOR
        bool lastStatusInspector = false;
        if (_examplePoolKey != null)
        {
            lastStatusInspector = _examplePoolKey.UseListInspector;
        }
#endif
        
        _examplePoolKey = new CustomPoolKey<TestPoolKey, ExampleElementKeyMono>(CreateData, null, null);

#if UNITY_EDITOR
        _examplePoolKey.UseListInspector = lastStatusInspector;
#endif  
        
        var obj = _examplePoolKey.GetObject(_keyCreateTestElement);
        //_examplePoolKey.Release( obj);
        
        
    }

    private ExampleElementKeyMono CreateData(TestPoolKey arg)
    {
        foreach (var VARIABLE in _prefabs)
        {
            if (VARIABLE.GetKey() == arg) 
            {
                return Instantiate(VARIABLE);
            }
        }

        return null;
    }
}

public enum TestPoolKey
{
    None,
    One,
    Two
}