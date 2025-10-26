using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class AbsStorageTaskVisible<Key> : AbsStorageTask<Key> where Key : IGetKey<string>
{

#if UNITY_EDITOR
    [SerializeField] 
    public bool _visibleData;

    [SerializeField]
    private List<AbsKeyData<string, string>> _listVisible = new List<AbsKeyData<string, string>>();
#endif

    public override void AddTask(Key key, string text)
    {
        base.AddTask(key, text);
        
#if UNITY_EDITOR
        if (_visibleData == true)
        {
            AddKeyVisible(key, text);
        }
#endif
    }

    public override void RemoveTask(Key key)
    {
        base.RemoveTask(key);
        
#if UNITY_EDITOR
        if (_visibleData == true)
        {
            RemoveKeyVisible(key);
        }
#endif
    }

#if UNITY_EDITOR
    private AbsKeyData<string, string> IsKeyVisible(Key key)
    {
        foreach (var VARIABLE in _listVisible)
        {
            if (VARIABLE.Key == key.GetKey())
            {
                return VARIABLE;
            }     
        }

        return null;
    }

    private void AddKeyVisible(Key key, string text)
    {
        var data = IsKeyVisible(key);
        if (data == null)
        {
            var newData = new AbsKeyData<string, string>(key.GetKey(),text);
            
            _listVisible.Add(newData);
        }
        
        
    }

    private void RemoveKeyVisible(Key key)
    {
        var data = IsKeyVisible(key);
        if (data != null)
        {
            _listVisible.Remove(data);
        }
    }
#endif
   
   

}
