using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Автоматически находит AutoDetectedKeyNameScene и берет с него ключ тек. сцены,
/// проверяет есть ли ключ для след сцены в StorageSceneNumber и если да,
/// то передает его в логику для получения списка ключей
/// </summary>
public class SetKeyCurrentSceneInGetKeyNameSceneOutSetKey : MonoBehaviour
{
    [SerializeField]
    private GetDKOPatch _dkoPatchAutoDetectedKeyNameScene;
 
    private AutoDetectedKeyNameScene _autoDetectedKeyNameScene;

    [SerializeField]
    private GetDKOPatch _dkoPatchStorageSceneNumber;
 
    private StorageSceneNumber _storageSceneNumber;

    /// <summary>
    /// Абстракция для получ. ключей (не нужно ждать её иниц, как минимум до момента пока не уст её ключ)
    /// </summary>
    [SerializeField]
    private GetKeyNameSceneOutSetKey _getKeyNameSceneOutSetKey;
    
    /// <summary>
    /// Подпис ли на обновл данных об списке заблокир. сцен
    /// </summary>
    [SerializeField]
    private bool _isAutoCheckBlock = true;
    
    private void Awake()
    {
        if (_dkoPatchAutoDetectedKeyNameScene.Init == false)
        {
            _dkoPatchAutoDetectedKeyNameScene.OnInit += OnInitDkoPatchAutoDetectedKeyNameScene;
        }
        if (_dkoPatchStorageSceneNumber.Init == false)
        {
            _dkoPatchStorageSceneNumber.OnInit += OnInitDkoPatchStorageSceneNumber;
        }
        
        CheckInit();
    }
    
    private void OnInitDkoPatchAutoDetectedKeyNameScene()
    {
        if (_dkoPatchAutoDetectedKeyNameScene.Init == true)
        {
            _dkoPatchAutoDetectedKeyNameScene.OnInit -= OnInitDkoPatchAutoDetectedKeyNameScene;
            CheckInit();
        }
    }
    
    private void OnInitDkoPatchStorageSceneNumber()
    {
        if (_dkoPatchStorageSceneNumber.Init == true)
        {
            _dkoPatchStorageSceneNumber.OnInit -= OnInitDkoPatchStorageSceneNumber;
            CheckInit();
        }
    }
   
    private void CheckInit()
    {
        if (_dkoPatchAutoDetectedKeyNameScene.Init == true && _dkoPatchStorageSceneNumber.Init == true) 
        {
            _autoDetectedKeyNameScene = _dkoPatchAutoDetectedKeyNameScene.GetDKO<DKODataInfoT<AutoDetectedKeyNameScene>>().Data;
            _storageSceneNumber = _dkoPatchStorageSceneNumber.GetDKO<DKODataInfoT<StorageSceneNumber>>().Data;

            GetKeyScene();
        }
    }
    private void GetKeyScene()
    {
        var keyNameScene = _autoDetectedKeyNameScene.GetKeyScene();
        var keyNextScene = _storageSceneNumber.GetNextNumberScene(keyNameScene);
        
        List<KeyNameScene> list = new List<KeyNameScene>();
        if (keyNextScene != null)
        {
            list.Add(keyNextScene);
        }
        else
        {
            Debug.Log("ВНИМАНИЕ, ПОТЕНЦИАЛЬНАЯ ОШИБКА. КЛЮЧ ДЛЯ СЛЕД. СЦЕНЫ НЕ БЫЛ НАЙДЕН");
        }
        
        //Тут нужно в лубом случ перед. список, что бы прошла иниц у абстракции
        _getKeyNameSceneOutSetKey.SetKeyScene(list);
    }
    
    
  
}
