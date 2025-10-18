using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Нужна на случай, если нужно определить ключи из вне
/// </summary>
public class GetKeyNameSceneOutSetKey : AbsGetStorageKeyNameScene
{
    public override event Action OnInit;
    
    public override bool IsInit => _isInit;
    private bool _isInit = false;


    private List<KeyNameScene> _listKey;

    public void SetKeyScene(List<KeyNameScene> list)
    {
        _listKey = list;

        _isInit = true;
        OnInit?.Invoke();
    }
    
    public override List<KeyNameScene> GetData()
    {
        return _listKey;
    }
}
