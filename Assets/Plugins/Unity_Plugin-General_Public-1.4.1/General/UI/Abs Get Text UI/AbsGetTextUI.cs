using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbsGetTextUI : MonoBehaviour
{
    public abstract bool IsInit {get;}
    public abstract event Action OnInit;

    public abstract Text GetText();
}
