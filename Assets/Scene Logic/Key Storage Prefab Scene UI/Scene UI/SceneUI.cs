using System;
using UnityEngine;
using UnityEngine.UI;

public class SceneUI : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField] 
    private LoadTargetSceneSetKey _sceneLoader;

    [SerializeField]
    private AddTaskInStorageTaskLoader _logicAddTask;
    
    private void Awake()
    {
        _button.onClick.AddListener(ButtonClick);
    }

    public void SetNameScene(KeyNameScene nameScene)
    {
        _sceneLoader.SetKeyScene(nameScene.GetKey());
    }
    
    private void ButtonClick()
    {
        _logicAddTask.StartLoadScene();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ButtonClick);
    }
}
