using System;
using UnityEngine;
using UnityEngine.UI;

public class SceneUI : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField] 
    private AbsSceneLoader _sceneLoader;
    
    private KeyNameScene _nameScene;

    private void Awake()
    {
        _button.onClick.AddListener(ButtonClick);
    }

    public void SetNameScene(KeyNameScene nameScene)
    {
        _nameScene = nameScene;
    }

    private void ButtonClick()
    {
        _sceneLoader.LoadScene(_nameScene.GetKey());
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ButtonClick);
    }
}
