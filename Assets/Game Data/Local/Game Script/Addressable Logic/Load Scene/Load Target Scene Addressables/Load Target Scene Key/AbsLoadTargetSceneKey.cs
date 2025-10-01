using System;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;

public abstract class AbsLoadTargetSceneKey : MonoBehaviour
{
    public abstract event Action OnInit;
    public abstract bool IsInit { get; }

    public abstract GetServerRequestData<SceneInstance> StartLoadScene();
}
