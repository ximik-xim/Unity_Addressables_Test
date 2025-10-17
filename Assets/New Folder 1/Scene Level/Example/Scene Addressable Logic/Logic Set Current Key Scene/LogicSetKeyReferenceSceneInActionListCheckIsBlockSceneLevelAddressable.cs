using System;
using UnityEngine;

/// <summary>
/// берет ключ через KeyReferenceScene
/// передает его в логику проверки заблокирован ли Scene Level
/// </summary>
public class LogicSetKeyReferenceSceneInActionListCheckIsBlockSceneLevelAddressable : MonoBehaviour
{
    [SerializeField]
    private ActionListCheckIsBlockSceneLevel _checkIsBlockSceneLeve;

    [SerializeField]
    private KeyNameSceneInGetDataSO_KeyReferenceScene _keyReferenceScene;

    /// <summary>
    /// Подпис ли на обновл данных об списке заблокир. сцен
    /// </summary>
    [SerializeField]
    private bool _isAutoCheckBlock;
    
    /// <summary>
    /// Логика зарг след сцены(не нужно ждать её иниц, как минимум до момента пока не уст её ключ)
    /// </summary>
    [SerializeField]
    private LoadTargetSceneSetKeyAddressable _loadTargetSceneSetKey;
    
    private void Awake()
    {
        if (_keyReferenceScene.IsInit == false)
        {
            _keyReferenceScene.OnInit += OnInitKeyReferenceScene;
        }
        
        CheckInit();
    }
    
    private void OnInitKeyReferenceScene()
    {
        if (_keyReferenceScene.IsInit == true)
        {
            _keyReferenceScene.OnInit -= OnInitKeyReferenceScene;
            CheckInit();
        }
    }
   
    private void CheckInit()
    {
        if (_keyReferenceScene.IsInit == true)  
        {
            _loadTargetSceneSetKey.SetKeyScene(_keyReferenceScene.GetKeySceneName().GetKey());
            _checkIsBlockSceneLeve.SetNameKey(_keyReferenceScene.GetKeySceneName(),true);
        }
    }
}
