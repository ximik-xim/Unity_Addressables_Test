using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Список исключений
/// Можно по ключу сцены указать другую текстуру и цвет обложки
/// </summary>
public class ExcepListKeyNameSceneStorageTextureSceneUI : AbsExceptionsListStorageTextureAndColorSceneSkinSceneLevelUI
{
    [SerializeField]
    //Список исключений. Нужен в случ. если опр. сцене, нужно задать текстуру и цвет обложки
    private List<AbsKeyData<GetDataSO_NameSceneAndKeyString, DataStorageTextureAndColorSceneSkinSceneLevelUI>> _listExceptions;

    public override event Action OnInit;
    public override bool IsInit => true;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override List<AbsKeyData<KeyNameScene, DataStorageTextureAndColorSceneSkinSceneLevelUI>> GetListExceptions()
    {
        List<AbsKeyData<KeyNameScene, DataStorageTextureAndColorSceneSkinSceneLevelUI>> list = new List<AbsKeyData<KeyNameScene, DataStorageTextureAndColorSceneSkinSceneLevelUI>>();
            
        foreach (var VARIABLE in _listExceptions)
        {
            list.Add(new AbsKeyData<KeyNameScene, DataStorageTextureAndColorSceneSkinSceneLevelUI>(new KeyNameScene(VARIABLE.Key.GetData().GetKey()) , VARIABLE.Data));
        }

        return list;
    }
}
