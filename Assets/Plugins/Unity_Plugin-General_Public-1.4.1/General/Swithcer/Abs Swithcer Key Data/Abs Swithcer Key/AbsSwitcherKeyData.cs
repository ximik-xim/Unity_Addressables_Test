using System.Collections.Generic;
using UnityEngine;

public abstract class AbsSwitcherKeyData<Key> : MonoBehaviour
{
    [SerializeField] 
    protected bool _checkAwake;
    [SerializeField]
    protected bool _checkOnEnable;
    [SerializeField]
    protected bool _checkEvent;
    
    protected Dictionary<Key, List<GameObject>> _keyDataGM = new Dictionary<Key, List<GameObject>>();
    
    protected void CheckAwake()
    {
        if (_checkAwake == true)
        {
            CheckKey();
        }
    }

    /// <summary>
    /// подписать на событие
    /// </summary>
    protected void CheckEvent()
    {
        if (_checkEvent == true)
        {
            CheckKey();
        }
        
    }
    
    protected void CheckEnable()
    {
        if (_checkOnEnable == true)
        {
            CheckKey();
        }
    }

    protected abstract Key GetCurrentKey();
    
    protected void CheckKey()
    {
        foreach (var VARIABLE in _keyDataGM.Keys)
        {
            if (GetCurrentKey().Equals(VARIABLE) == false)  
            {
                foreach (var VARIABLE2 in _keyDataGM[VARIABLE])
                {
                    VARIABLE2.gameObject.SetActive(false);
                }
                 
            }
        }

        foreach (var VARIABLE2 in _keyDataGM[GetCurrentKey()])
        {
            VARIABLE2.gameObject.SetActive(true);
        }
        
    }

   
}
