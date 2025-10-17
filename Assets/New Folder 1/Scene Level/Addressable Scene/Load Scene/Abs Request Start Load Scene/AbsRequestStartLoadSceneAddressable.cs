using System;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
/// <summary>
/// Абстракция нужная что бы отделить логику установки параметров для загр. сцен
/// и логику запуска загрузки сцен
/// </summary>
public abstract class AbsRequestStartLoadSceneAddressable : MonoBehaviour
{
    public abstract event Action OnInit;
    public abstract bool IsInit { get; }
    
    public abstract GetServerRequestData<AsyncOperationHandle<SceneInstance>> StartLoadScene();
}
