using System;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class AbsCallbackGetDataAsyncOperationHandleT<ArgData> : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;
    public abstract GetServerRequestData<AsyncOperationHandle<T>> GetData<T>(ArgData data);
}
