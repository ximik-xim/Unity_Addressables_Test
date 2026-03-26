using System;
using UnityEngine;
using UnityEngine.UI;

public class GetTextUI_Default : AbsGetTextUI
{
    public override bool IsInit => true;
    public override event Action OnInit;
    
    [SerializeField]
    private Text _text;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override Text GetText()
    {
        return _text;
    }
}
