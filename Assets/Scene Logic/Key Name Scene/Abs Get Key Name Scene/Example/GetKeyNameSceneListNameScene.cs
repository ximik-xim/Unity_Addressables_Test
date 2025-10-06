using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Нужен если нужно получить не все элементы из хранилеща, а только определённые
/// </summary>
public class GetKeyNameSceneListNameScene : AbsGetStorageKeyNameScene
{
    [SerializeField]
    private List<GetDataSO_NameScene> _listKeyNameScene;

    public override bool IsInit => true;
    public override event Action OnInit;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override List<KeyNameScene> GetData()
    {
        List<KeyNameScene> list = new List<KeyNameScene>();
        
        foreach (var VARIABLE in _listKeyNameScene)
        {
            list.Add(VARIABLE.GetData());
        }

        return list;
    }
}
