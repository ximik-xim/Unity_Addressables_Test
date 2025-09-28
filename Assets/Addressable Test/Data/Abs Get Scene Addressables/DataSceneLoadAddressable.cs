using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

[System.Serializable]
/// <summary>
/// Настройки для загрузки сцены через Addressable
/// </summary>
public class DataSceneLoadAddressable
{
    /// <summary>
    /// Нужен только для инспектора
    /// </summary>
    public DataSceneLoadAddressable()
    {
        _loadMode = LoadSceneMode.Single;
        _activateOnLoad = true;
        _priority = 100;
        _releaseMode = SceneReleaseMode.ReleaseSceneWhenSceneUnloaded;
    }

    public DataSceneLoadAddressable(object key, LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true, int priority = 100, SceneReleaseMode releaseMode = SceneReleaseMode.ReleaseSceneWhenSceneUnloaded)
    {
        _key = key;
        _loadMode = loadMode;
        _activateOnLoad = activateOnLoad;
        _priority = priority;
        _releaseMode = releaseMode;
    }
    
    [SerializeReference]
    private object _key;
    [SerializeField]
    private LoadSceneMode _loadMode = LoadSceneMode.Single;
    [SerializeField]
    private bool _activateOnLoad = true;
    [SerializeField]
    private int _priority = 100;
    [SerializeField]
    private SceneReleaseMode _releaseMode = SceneReleaseMode.ReleaseSceneWhenSceneUnloaded;

    public object Key => _key;
    public LoadSceneMode LoadMode => _loadMode;
    public bool ActivateOnLoad => _activateOnLoad;
    public int Priority => _priority;
    public SceneReleaseMode ReleaseMode => _releaseMode;
}
