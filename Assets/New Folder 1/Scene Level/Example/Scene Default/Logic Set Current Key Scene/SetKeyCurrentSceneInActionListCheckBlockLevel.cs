using UnityEngine;

/// <summary>
/// Автоматически находит AutoDetectedKeyNameScene и берет с него ключ тек. сцены,
/// проверяет есть ли ключ для след сцены в StorageSceneNumber и если да,
/// то передает его в логику проверки заблокирован ли Scene Level
/// </summary>
public class SetKeyCurrentSceneInActionListCheckBlockLevel : MonoBehaviour
{
    [SerializeField]
    private ActionListCheckIsBlockSceneLevel _checkIsBlockSceneLeve;

    [SerializeField]
    private GetDKOPatch _dkoPatchAutoDetectedKeyNameScene;
 
    private AutoDetectedKeyNameScene _autoDetectedKeyNameScene;

    [SerializeField]
    private GetDKOPatch _dkoPatchStorageSceneNumber;
 
    private StorageSceneNumber _storageSceneNumber;

    /// <summary>
    /// Логика зарг след сцены(не нужно ждать её иниц, как минимум до момента пока не уст её ключ)
    /// </summary>
    [SerializeField]
    private LoadTargetSceneSetKeyNameScene _loadTargetSceneSetKey;
    
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
        
        if (keyNextScene != null)
        {
            _loadTargetSceneSetKey.SetKeyScene(keyNextScene.GetKey());
            _checkIsBlockSceneLeve.SetNameKey(keyNextScene, _isAutoCheckBlock);
        }
        
    }
}
