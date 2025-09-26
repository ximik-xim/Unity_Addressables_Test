using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class DataSceneLoadAddressable
{

    public  DataSceneLoadAddressable(object key, LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true, int priority = 100, SceneReleaseMode releaseMode = SceneReleaseMode.ReleaseSceneWhenSceneUnloaded)
    {
        _key = key;
        _loadMode = loadMode;
        _activateOnLoad = activateOnLoad;
        _priority = priority;
        _releaseMode = releaseMode;
    }
    
    private object _key;
    private LoadSceneMode _loadMode;
    private bool _activateOnLoad;
    private int _priority;
    private SceneReleaseMode _releaseMode;

    public object Key => _key;
    public LoadSceneMode LoadMode => _loadMode;
    public bool ActivateOnLoad => _activateOnLoad;
    public int Priority => _priority;
    public SceneReleaseMode ReleaseMode => _releaseMode;
}
