using System;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class AbsCallbackGetDataAsyncOperationHandle<GetType, ArgData> : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;
    public abstract GetServerRequestData<AsyncOperationHandle<GetType>> GetData(ArgData data);
}
