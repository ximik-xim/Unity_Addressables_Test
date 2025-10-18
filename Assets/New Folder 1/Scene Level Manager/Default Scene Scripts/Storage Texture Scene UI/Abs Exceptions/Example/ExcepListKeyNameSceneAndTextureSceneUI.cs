using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Список исключений
/// Можно по ключу сцены указать другую текстуру и цвет обложки
/// </summary>
public class ExcepListKeyNameSceneAndTextureSceneUI : AbsExceptionsListStorageTextureAndColorSkinSceneLevelUI
{
    [SerializeField]
    //Список исключений. Нужен в случ. если опр. сцене, нужно задать текстуру и цвет обложки
    private List<AbsKeyData<GetDataSO_NameSceneAndKeyString, DataStorageTextureAndColorSkinSceneLevelUI>> _listExceptions;

    public override event Action OnInit;
    public override bool IsInit => true;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override List<AbsKeyData<KeyNameScene, DataStorageTextureAndColorSkinSceneLevelUI>> GetListExceptions()
    {
        List<AbsKeyData<KeyNameScene, DataStorageTextureAndColorSkinSceneLevelUI>> list = new List<AbsKeyData<KeyNameScene, DataStorageTextureAndColorSkinSceneLevelUI>>();
            
        foreach (var VARIABLE in _listExceptions)
        {
            list.Add(new AbsKeyData<KeyNameScene, DataStorageTextureAndColorSkinSceneLevelUI>(new KeyNameScene(VARIABLE.Key.GetData().GetKey()) , VARIABLE.Data));
        }

        return list;
    }
}
