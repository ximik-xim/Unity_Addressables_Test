using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Список исключений
/// Можно по ключу сцены указать другой порядковый номер для этого ключа сцены
/// </summary>
public class ExcepListKeyNameSceneSortingScene : AbsExceptionsListInLogicSortingSceneLevel
{
    [SerializeField]
    //Список исключений. Нужен в случ. если опр. сцене, нужно задать опр. номер на сцене
    private List<AbsKeyData<GetDataSO_NameSceneAndKeyString, int>> _listExceptions;

    public override event Action OnInit;
    public override bool IsInit => true;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override List<AbsKeyData<KeyNameScene, int>> GetListExceptions()
    {
        List<AbsKeyData<KeyNameScene, int>> list = new List<AbsKeyData<KeyNameScene, int>>();
            
        foreach (var VARIABLE in _listExceptions)
        {
            list.Add(new AbsKeyData<KeyNameScene, int>(new KeyNameScene(VARIABLE.Key.GetData().GetKey()) , VARIABLE.Data));
        }

        return list;
    }
}
