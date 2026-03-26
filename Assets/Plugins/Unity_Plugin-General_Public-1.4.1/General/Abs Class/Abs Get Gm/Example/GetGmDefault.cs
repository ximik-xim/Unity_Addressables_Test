using System;
using UnityEngine;

public class GetGmDefault : AbsGetGm
{
    public override bool IsInit => true;
    public override event Action OnInit;

    [SerializeField]
    private GameObject _gm;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override GameObject GetGm()
    {
        return _gm;
    }
}
