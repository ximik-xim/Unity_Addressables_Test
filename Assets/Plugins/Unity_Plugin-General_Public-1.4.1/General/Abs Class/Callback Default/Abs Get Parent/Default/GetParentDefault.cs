using System;
using UnityEngine;

public class GetParentDefault : AbsGetParent
{
    public override bool IsInit => true;
    public override event Action OnInit;

    [SerializeField] 
    private GameObject _parent;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override GameObject GetParent()
    {
        return _parent;
    }
}
