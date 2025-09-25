using System;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public abstract class sasdasd : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;
    public abstract GetServerRequestData<T> GetScene<T>(object key, LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true, int priority = 100, SceneReleaseMode releaseMode = SceneReleaseMode.ReleaseSceneWhenSceneUnloaded);
}
