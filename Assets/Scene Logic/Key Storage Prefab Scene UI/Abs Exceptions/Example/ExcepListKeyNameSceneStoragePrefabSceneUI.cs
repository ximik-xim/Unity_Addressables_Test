using System;
using System.Collections.Generic;
using UnityEngine;

public class ExcepListKeyNameSceneStoragePrefabSceneUI : AbsExceptionsListInKeyStoragePrefabSceneUI
{
    [SerializeField]
    //Список исключений. Нужен в случ. если опр. сцене, нужно задать опр. номер на сцене
    private List<AbsKeyData<GetDataSO_NameSceneAndKeyString, AbsSceneUI>> _listExceptions;

    public override event Action OnInit;
    public override bool IsInit => true;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override List<AbsKeyData<KeyNameScene, AbsSceneUI>> GetListExceptions()
    {
        List<AbsKeyData<KeyNameScene, AbsSceneUI>> list = new List<AbsKeyData<KeyNameScene, AbsSceneUI>>();
            
        foreach (var VARIABLE in _listExceptions)
        {
            list.Add(new AbsKeyData<KeyNameScene, AbsSceneUI>(new KeyNameScene(VARIABLE.Key.GetData().GetKey()), VARIABLE.Data));
        }

        return list;
    }
}
