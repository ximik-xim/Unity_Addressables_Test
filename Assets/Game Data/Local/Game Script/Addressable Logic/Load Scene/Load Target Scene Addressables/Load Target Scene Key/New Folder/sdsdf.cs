using System;
using UnityEngine;
using UnityEngine.UI;

public class sdsdf : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField]
    private AbsLoadTargetSceneKey _loadScene;
    
    private void Awake()
    {
        if (_loadScene.IsInit == false)
        {
            _loadScene.OnInit += OnInitCheckAndDownloadUpdateObject;
        }
        

        CheckInit();
    }

    private void OnInitCheckAndDownloadUpdateObject()
    {
        if (_loadScene.IsInit == true)
        {
            _loadScene.OnInit -= OnInitCheckAndDownloadUpdateObject;
            CheckInit();
        }
    }
    

    private void CheckInit()
    {
        if (_loadScene.IsInit == true)
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
        _loadScene.StartLoadScene();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ButtonClick);
    }
}
