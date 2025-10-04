
using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbsSceneUI : MonoBehaviour
{
    public abstract void SetNameScene(KeyNameScene nameScene);
    
    public abstract DKOKeyAndTargetAction GetSceneUIDKO();
}
