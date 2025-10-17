using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Список исключений
/// Можно по ключу сцены указать другой префаб обложки сцены(AbsSceneUI)
/// </summary>
public class ExcepListKeyNameSceneStoragePrefabSkinSceneLevelUI : AbsExceptionsListInKeyStoragePrefabSkinSceneLevelUI
{
    [SerializeField]
    //Список исключений. Нужен в случ. если опр. сцене, нужно задать опр. номер на сцене
    private List<AbsKeyData<GetDataSO_NameSceneAndKeyString, AbsSkinSceneLevelUI>> _listExceptions;

    public override event Action OnInit;
    public override bool IsInit => true;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override List<AbsKeyData<KeyNameScene, AbsSkinSceneLevelUI>> GetListExceptions()
    {
        List<AbsKeyData<KeyNameScene, AbsSkinSceneLevelUI>> list = new List<AbsKeyData<KeyNameScene, AbsSkinSceneLevelUI>>();
            
        foreach (var VARIABLE in _listExceptions)
        {
            list.Add(new AbsKeyData<KeyNameScene, AbsSkinSceneLevelUI>(new KeyNameScene(VARIABLE.Key.GetData().GetKey()), VARIABLE.Data));
        }

        return list;
    }
}
