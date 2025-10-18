using System;
using UnityEngine;

/// <summary>
/// Логика нумерации сцен
/// </summary>
public class LogicStartNumberScene : MonoBehaviour
{
    public bool IsInit => _isInit;
    private bool _isInit = false;
    public event Action OnInit;
    
    [SerializeField] 
    private AbsGetStorageKeyNameScene _storageAbsGetKeyScene;
    
    [SerializeField]
    private LogicSortingSceneLevel _logicSortingSceneLevel;

    [SerializeField]
    private StorageSceneNumber _storageSceneNumber;
    
    private void Awake()
    {
        if (_storageAbsGetKeyScene.IsInit == false)
        {
            _storageAbsGetKeyScene.OnInit += OnInitStorageAbsGetKeyScene;
        }
 
        if (_logicSortingSceneLevel.IsInit == false)
        {
            _logicSortingSceneLevel.OnInit += OnInitLogicSortingSceneLevel;
        }
        
        CheckInit();
    }

    private void OnInitStorageAbsGetKeyScene()
    {
        if (_storageAbsGetKeyScene.IsInit == true)
        {
            _storageAbsGetKeyScene.OnInit -= OnInitStorageAbsGetKeyScene;
            CheckInit();
        }
    }

    private void OnInitLogicSortingSceneLevel()
    {
        if (_logicSortingSceneLevel.IsInit == true)
        {
            _logicSortingSceneLevel.OnInit -= OnInitLogicSortingSceneLevel;
            CheckInit();
        }
    }

    private void CheckInit()
    {
        if (_storageAbsGetKeyScene.IsInit == true && _logicSortingSceneLevel.IsInit == true) 
        {
            _isInit = true;
            OnInit?.Invoke();
        }
    }

    public void StartNumberScene()
    {
        var listKeyScene = _storageAbsGetKeyScene.GetData();

        //получ. отсортир. по порядку список сцен(с нумерацией)
       var listKeySceneAndNumber = _logicSortingSceneLevel.SortingNumberScene(listKeyScene, 1);
         
        //Да, его нужно вручную отдельно уст. т.к надо знать под каким поряд. номером должна наход сцена
        _storageSceneNumber.SetNumberScene(listKeySceneAndNumber);
    }
}
