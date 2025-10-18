using System;
using UnityEngine;

/// <summary>
/// Абстракция нужная что бы отделить логику установки параметров для загр. сцен
/// и логику запуска загрузки сцен
/// </summary>
public abstract class AbsRequestStartLoadScene : MonoBehaviour
{
    public abstract event Action OnInit;
    public abstract bool IsInit { get; }
    
    public abstract GetServerRequestData<RequestStartLoadSceneData> StartLoadScene();
}

