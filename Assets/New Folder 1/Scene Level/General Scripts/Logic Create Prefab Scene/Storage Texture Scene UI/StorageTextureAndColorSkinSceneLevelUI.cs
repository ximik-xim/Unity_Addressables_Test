using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Хранит в себе текстуру котор будет наложена на экземпляр SceneUI(обложка для загр. сцены)
/// </summary>
public class StorageTextureAndColorSkinSceneLevelUI : MonoBehaviour
{

    public event Action OnInit;
    public bool IsInit => _isInit;
    private bool _isInit = false;
    
    [SerializeField]
    private DataStorageTextureAndColorSkinSceneLevelUI _defaultTexture;
    
    //Список исключений. Нужен в случ. если опр. сцене, нужно задать опр. номер на сцене
    [SerializeField]
    private AbsExceptionsListStorageTextureAndColorSkinSceneLevelUI _listExceptions;

    private Dictionary<string, DataStorageTextureAndColorSkinSceneLevelUI> _exceptionsTextureData = new Dictionary<string, DataStorageTextureAndColorSkinSceneLevelUI>();

    
    public event Action OnUpdateData;

    private void Awake()
    {
        if (_listExceptions.IsInit == false)
        {
            _listExceptions.OnInit += OnInitListExceptions;
        }

        CheckInit();
    }

    private void OnInitListExceptions()
    {
        if (_listExceptions.IsInit == true)
        {
            _listExceptions.OnInit -= OnInitListExceptions;
            CheckInit();
        }
    }

    private void CheckInit()
    {
        if (_listExceptions.IsInit == true)
        {
            foreach (var VARIABLE in _listExceptions.GetListExceptions())
            {
                _exceptionsTextureData.Add(VARIABLE.Key.GetKey(), VARIABLE.Data);
            }

            _isInit = true;
            OnInit?.Invoke();
        }
    }

    public DataStorageTextureAndColorSkinSceneLevelUI GetTextureUI(KeyNameScene keyNameScene)
    {
        if (_exceptionsTextureData.ContainsKey(keyNameScene.GetKey()) == true)
        {
            return _exceptionsTextureData[keyNameScene.GetKey()];
        }

        return _defaultTexture;
    }
    
}

