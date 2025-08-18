using System;
using UnityEngine;

/// <summary>
/// Будет определять, какие сцены добавлять
/// </summary>
public abstract class AbsControllerAddScene : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;

    public abstract event Action OnComplitedAddScene;
    public abstract void StartAddScene();
}
