
using System.Collections.Generic;
using UnityEngine;


public abstract class AbsSwitcherKey<Key> : AbsSwitcherKeyData<Key>
{
    [SerializeField]
    protected List<AbsKeyData<Key, List<GameObject>>> _listKeyDataGM = new List<AbsKeyData<Key, List<GameObject>>>();
    
    protected virtual void StartInit()
    {
        foreach (var VARIABLE in _listKeyDataGM)
        {
            _keyDataGM.Add(VARIABLE.Key, VARIABLE.Data);
        }
    }
    
    protected void DeletionDuplication()
    {
        int targetCount = _listKeyDataGM.Count;
        for (int i = 0; i < targetCount; i++)
        {
            var type = _listKeyDataGM[i].Key;

            for (int j = i + 1; j < targetCount; j++) 
            {
                if (type.Equals(_listKeyDataGM[j].Key) == true)
                {
                    _listKeyDataGM.RemoveAt(j);

                    j--;
                    i--;
                    targetCount--;
                }
            }
            
        }
    }

    protected void AddType(Key typePlatform)
    {
        for (int i = 0; i < _listKeyDataGM.Count; i++)
        {
            if (_listKeyDataGM[i].Key.Equals(typePlatform) == true)  
            {
                return;
            }
        }
        
        _listKeyDataGM.Add(new AbsKeyData<Key, List<GameObject>>(typePlatform,null));
    }
}
