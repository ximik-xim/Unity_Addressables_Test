using System;
using System.Collections.Generic;
using UnityEngine;

public class StorageKeyAndGM : MonoBehaviour
{
    private bool _isInit = false;
    public bool IsInit => _isInit;
    public event Action OnInit;
    public event Action<string> OnInitElement;
    
    [SerializeField]
    private List<AbsKeyData<GetDataSO_StorageKeyGM, GameObject>> _list = new List<AbsKeyData<GetDataSO_StorageKeyGM, GameObject>>();

    private Dictionary<string, GameObject> _dictionary = new Dictionary<string, GameObject>();
    
#if UNITY_EDITOR
    [SerializeField] 
    private bool _visibleData;
    [SerializeField] 
    private List<AbsKeyData<string, GameObject>> _listVisible = new List<AbsKeyData<string, GameObject>>();
#endif
    
    private void Awake()
    {
        LocalAwake();
    }

    private void LocalAwake()
    {
        foreach (var VARIABLE in _list)
        {
            _dictionary.Add(VARIABLE.Key.GetData().GetKey(), VARIABLE.Data);
            
#if UNITY_EDITOR
            if (_visibleData == true)
            {
                _listVisible.Add(new AbsKeyData<string, GameObject>(VARIABLE.Key.GetData().GetKey(), VARIABLE.Data));
            }
#endif
        }
        

        
        _isInit = true;
        OnInit?.Invoke();
    }
    
    public GameObject GetGM(StorageKeyGM key)
    {
        return _dictionary[key.GetKey()];
    }

    public bool KeyIsInsert(StorageKeyGM key)
    {
        return _dictionary.ContainsKey(key.GetKey());
    }

    public void AddGM(StorageKeyGM key, GameObject gameObject)
    {
        if (_dictionary.ContainsKey(key.GetKey()) == false)
        {
#if UNITY_EDITOR
            if (_visibleData == true)
            {
                _listVisible.Add(new AbsKeyData<string, GameObject>(key.GetKey(), gameObject));
            }
#endif
            
            _dictionary.Add(key.GetKey(), gameObject);
            OnInitElement?.Invoke(key.GetKey());
        }
    }

    public void RemoveGM(StorageKeyGM key)
    {
        if (_dictionary.ContainsKey(key.GetKey()) == true)
        {
#if UNITY_EDITOR
            if (_visibleData == true)
            {
                for (int i = 0; i < _listVisible.Count; i++)
                {
                    if (_listVisible[i].Key == key.GetKey()) 
                    {
                        _listVisible.RemoveAt(i);
                        break;
                    }
                        
                }
                
            }
#endif
            
            _dictionary.Remove(key.GetKey());
        }
    }

}
