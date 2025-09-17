using System;
using UnityEngine;

public abstract class AbsSceneLoader : MonoBehaviour
{
    public abstract event Action OnInit;
    public abstract bool IsInit { get; }
    
    public abstract void LoadScene(int numberScene);

    public abstract void LoadScene(string nameScene);
}
