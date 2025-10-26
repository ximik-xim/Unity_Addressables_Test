using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSceneLoaderNameDefault : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField]
    private string _nameScene;

    [SerializeField]
    private AbsSceneLoader _sceneLoader;
    
    private void Awake()
    {
        if (_sceneLoader.IsInit == false)
        {
            _sceneLoader.OnInit += OnInitSceneLoader;
            return;
        }

        InitSceneLoader();
    }

    private void OnInitSceneLoader()
    {
        if (_sceneLoader.IsInit == true) 
        {
            _sceneLoader.OnInit -= OnInitSceneLoader;
            InitSceneLoader();
        }
        
    }
    
    private void InitSceneLoader()
    {
        InitData();
    }

    private void InitData()
    {
        _button.onClick.AddListener(ButtonClick);
    }

    public void SetNameScene(string sceneName)
    {
        _nameScene = sceneName;
    }
    
    private void ButtonClick()
    {
        _sceneLoader.LoadScene(_nameScene);
    }
    
    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ButtonClick);
    }
}
