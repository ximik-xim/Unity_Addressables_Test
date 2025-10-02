using System;
using UnityEngine;
using UnityEngine.UI;

public class SceneUI : MonoBehaviour
{
    [SerializeField] 
    private LoadTargetSceneSetKey _sceneLoader;
    
    public void SetNameScene(KeyNameScene nameScene)
    {
        _sceneLoader.SetKeyScene(nameScene.GetKey());
    }
}
