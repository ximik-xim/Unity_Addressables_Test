using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Логика автоматического получ номерации сцены
/// </summary>
public class LogicSceneUI_GetSceneNumberText : MonoBehaviour
{
    [SerializeField]
    private GetDKOPatch _getDkoPatch;

    [SerializeField]
    private AbsSceneUI _sceneUI;

    private StorageSceneNumber _storageSceneNumber;
    
    [SerializeField]
    private Text _text;
    
    [SerializeField]
    private bool _textNewLine = true;
    
    [SerializeField]
    private string _textLevel = "Уровень ";
    
    private void Awake()
    {
        if (_getDkoPatch.Init == false)
        {
            _getDkoPatch.OnInit += OnInitGetDkoPatchStorageSceneNumber;
        }
        
        if (_sceneUI.IsInit == false)
        {
            _sceneUI.OnInit += OnInitSceneUI;
        }
        
        CheckInit();
    }
    
    private void OnInitGetDkoPatchStorageSceneNumber()
    {
        if (_getDkoPatch.Init == true)
        {
            _getDkoPatch.OnInit -= OnInitGetDkoPatchStorageSceneNumber;
            CheckInit();
        }
    }
    
    private void OnInitSceneUI()
    {
        if (_sceneUI.IsInit == true)
        {
            _sceneUI.OnInit -= OnInitSceneUI;
            CheckInit();
        }
    }
   
    private void CheckInit()
    {
        if (_getDkoPatch.Init == true && _sceneUI.IsInit == true)  
        {
            _storageSceneNumber = _getDkoPatch.GetDKO<DKODataInfoT<StorageSceneNumber>>().Data;
            
            InitData();
        }
    }

    private void InitData()
    {
        if (_storageSceneNumber.ContainNumberScene(_sceneUI.GetName()) == false)
        {
            _storageSceneNumber.OnUpdateData -= OnUpdateDataStorageSceneNumber;
            _storageSceneNumber.OnUpdateData += OnUpdateDataStorageSceneNumber;
        }
        else
        {
            GetNumberScene();
        }
    }

    private void OnUpdateDataStorageSceneNumber()
    {
        if (_storageSceneNumber.ContainNumberScene(_sceneUI.GetName()) == true)
        {
            _storageSceneNumber.OnUpdateData -= OnUpdateDataStorageSceneNumber;
            GetNumberScene();
        }
    }

    private void GetNumberScene()
    {
        if (_textNewLine == true)
        {
            //Потом тут другую логику можно написать
            _text.text = _textLevel + "\n" + _storageSceneNumber.GetNumberScene(_sceneUI.GetName()).ToString();
        }
        else
        {
            //Потом тут другую логику можно написать
            _text.text = _textLevel + _storageSceneNumber.GetNumberScene(_sceneUI.GetName()).ToString();
        }
    }
}
