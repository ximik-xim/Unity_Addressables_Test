using System;
using UnityEngine;

public class GetStringTextInString : AbsGetStringText
{
    public override bool IsInit => true;
    public override event Action OnInit;

    [SerializeField]
    private string _text;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override string GetStringText()
    {
        return _text;
    }
}
