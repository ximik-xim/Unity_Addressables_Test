using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

/// <summary>
/// Простая обертака для загруки указ сцены
/// </summary>
public class LoadTargetSceneAddressables : MonoBehaviour
{
    public event Action OnInit
    {
        add
        {
            _loadSceneAddressables.OnInit += value;
        }

        remove
        {
            _loadSceneAddressables.OnInit -= value;
        }
    }

    public bool IsInit => _loadSceneAddressables.IsInit;
    
    [SerializeField]
    private AbsCallbackGetSceneAddressables _loadSceneAddressables;

    /// Т.К key являеться object и не сериализ. По этому ключ сериализ. отдельно, а ост. данные сразу в нем сериализую
    /// </summary>
    [SerializeField]
    private DataSceneLoadAddressable _sceneLoadSettings;
    
    
    public GetServerRequestData<SceneInstance> StartLoadScene(object keyScene)
    {
        _sceneLoadSettings = new DataSceneLoadAddressable(keyScene, _sceneLoadSettings.LoadMode, _sceneLoadSettings.ActivateOnLoad, _sceneLoadSettings.Priority, _sceneLoadSettings.ReleaseMode);
        var callback = _loadSceneAddressables.GetData(_sceneLoadSettings);

        return callback;
    }
}
