using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleDefaultPool : MonoBehaviour
{
    [SerializeField]
    private CustomPool<ExampleElementDefaultPoolData> _examplePoolKey;

    [SerializeField] 
    private ExampleElementDefaultPoolData  _prefabs;
    private void OnEnable()
    {
#if UNITY_EDITOR
        bool lastStatusInspector = false;
        if (_examplePoolKey != null)
        {
            lastStatusInspector = _examplePoolKey.UseListInspector;
        }
#endif
        
        _examplePoolKey = new CustomPool<ExampleElementDefaultPoolData>(CreateData, null, null);

#if UNITY_EDITOR
        _examplePoolKey.UseListInspector = lastStatusInspector;
#endif   

        var obj = _examplePoolKey.GetObject();
        //_examplePoolKey.ReleaseObject(obj);
        
    }

    private ExampleElementDefaultPoolData CreateData()
    {
        return Instantiate(_prefabs);
    }
}
