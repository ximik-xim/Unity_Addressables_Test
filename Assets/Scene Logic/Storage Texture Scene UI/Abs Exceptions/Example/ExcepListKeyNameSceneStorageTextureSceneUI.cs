using System;
using System.Collections.Generic;
using UnityEngine;

public class ExcepListKeyNameSceneStorageTextureSceneUI : AbsExceptionsListStorageTextureSceneUI
{
    [SerializeField]
    //Список исключений. Нужен в случ. если опр. сцене, нужно задать опр. номер на сцене
    private List<AbsKeyData<GetDataSO_NameScene, DataStorageTextureSceneUI>> _listExceptions;

    public override event Action OnInit;
    public override bool IsInit => true;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override List<AbsKeyData<KeyNameScene, DataStorageTextureSceneUI>> GetListExceptions()
    {
        List<AbsKeyData<KeyNameScene, DataStorageTextureSceneUI>> list = new List<AbsKeyData<KeyNameScene, DataStorageTextureSceneUI>>();
            
        foreach (var VARIABLE in _listExceptions)
        {
            list.Add(new AbsKeyData<KeyNameScene, DataStorageTextureSceneUI>(VARIABLE.Key.GetData(), VARIABLE.Data));
        }

        return list;
    }
}
