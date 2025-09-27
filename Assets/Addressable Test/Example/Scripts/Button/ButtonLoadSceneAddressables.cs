using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonLoadSceneAddressables : MonoBehaviour
{
    public bool IsInit => _isInit;
    private bool _isInit = false;
    public event Action OnInit;

    [SerializeField]
    private Button _button;
    
    [SerializeField]
    private AssetReference _keyNameScene;
    
    /// <summary>
    /// Т.К key являеться object и не сериализ. По этому ключ сериализ. отдельно, а ост. данные сразу в нем сериализую
    /// </summary>
    [SerializeField]
    private DataSceneLoadAddressable _sceneLoadSettings;

    [SerializeField] 
    private AbsCallbackGetSceneAddressables _getSceneAddressables;

    private void Awake()
    {
        if (_getSceneAddressables.IsInit == false)
        {
            _getSceneAddressables.OnInit += OnInitStorageIsGetScene;
        }
        

        CheckInit();
    }

    private void OnInitStorageIsGetScene()
    {
        if (_getSceneAddressables.IsInit == true)
        {
            _getSceneAddressables.OnInit -= OnInitStorageIsGetScene;
            CheckInit();
        }
    }
    
    private void CheckInit()
    {
        if (_getSceneAddressables.IsInit == true)
        {
            Init();
        }
    }
    
    private void Init()
    {
        _button.onClick.AddListener(ButtonClick);
    }

    private void ButtonClick()
    {
        StartLogic();
    }
    

    private void StartLogic()
    {
        //настройки загрузки сцены
        _sceneLoadSettings = new DataSceneLoadAddressable(_keyNameScene, _sceneLoadSettings.LoadMode, _sceneLoadSettings.ActivateOnLoad, _sceneLoadSettings.Priority, _sceneLoadSettings.ReleaseMode);
        
        Debug.Log("Послан запрос на загрузку сцены");
        var dataCallback = _getSceneAddressables.GetData(_sceneLoadSettings);

        if (dataCallback.IsGetDataCompleted == true)
        {
            CompletedGetData();
        }
        else
        {
            dataCallback.OnGetDataCompleted += OnCompletedGetData;
        }

        void OnCompletedGetData()
        {
            if (dataCallback.IsGetDataCompleted == true)
            {
                dataCallback.OnGetDataCompleted -= OnCompletedGetData;
                CompletedGetData();
            }
        }

        void CompletedGetData()
        {
            Debug.Log("----- Данные получены от загр. сцены ----");

            Scene sceneData = dataCallback.GetData.Scene;
            
            Debug.Log("Scene Name = " + sceneData.name);
            Debug.Log("Scene Path = " + sceneData.path);

            _isInit = true;
            OnInit?.Invoke();
        }
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ButtonClick);
    }
}
