using System;
using UnityEngine;

public abstract class AbsRequestStartLoadScene : MonoBehaviour
{
    public abstract event Action OnInit;
    public abstract bool IsInit { get; }
    
    public abstract GetServerRequestData<RequestStartLoadSceneData> StartLoadScene();
}

