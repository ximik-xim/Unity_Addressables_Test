
using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbsSceneUI : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;
    public abstract void SetNameScene(KeyNameScene nameScene);

    public abstract KeyNameScene GetName();
    
    public abstract DKOKeyAndTargetAction GetSceneUIDKO();
}
