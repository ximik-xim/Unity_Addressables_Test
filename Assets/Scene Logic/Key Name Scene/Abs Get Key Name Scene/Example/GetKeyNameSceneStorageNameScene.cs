using System;
using System.Collections.Generic;
using UnityEngine;

public class GetKeyNameSceneStorageNameScene : AbsGetStorageKeyNameScene
{
    public override bool IsInit => true;
    public override event Action OnInit;
    
    [SerializeField] 
    private SO_Data_NameScene _storage;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override List<KeyNameScene> GetData()
    {
        return _storage.GetAllData();
    }
}
