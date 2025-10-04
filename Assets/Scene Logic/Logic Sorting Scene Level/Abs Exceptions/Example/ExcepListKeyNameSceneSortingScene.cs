using System;
using System.Collections.Generic;
using UnityEngine;

public class ExcepListKeyNameSceneSortingScene : AbsExceptionsListInLogicSortingSceneLevel
{
    [SerializeField]
    //Список исключений. Нужен в случ. если опр. сцене, нужно задать опр. номер на сцене
    private List<AbsKeyData<GetDataSO_NameScene, int>> _listExceptions;

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
            list.Add(new AbsKeyData<KeyNameScene, int>(VARIABLE.Key.GetData(), VARIABLE.Data));
        }

        return list;
    }
}
