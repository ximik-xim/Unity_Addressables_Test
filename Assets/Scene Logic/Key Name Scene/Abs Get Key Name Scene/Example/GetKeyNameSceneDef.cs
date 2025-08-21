using System;
using UnityEngine;

public class GetKeyNameSceneDef : AbsGetStorageKeyNameScene
{
    public override bool IsInit => true;
    public override event Action OnInit;
    
    [SerializeField] 
    private SO_Data_NameScene _storage;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override SO_Data_NameScene GetData()
    {
        return _storage;
    }
}
