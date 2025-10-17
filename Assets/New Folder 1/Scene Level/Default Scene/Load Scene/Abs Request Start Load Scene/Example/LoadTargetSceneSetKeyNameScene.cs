
using System;
using UnityEngine;

/// <summary>
/// Нужен что бы указать ключ из вне
/// </summary>
public class LoadTargetSceneSetKeyNameScene : AbsRequestStartLoadScene
{
    public override event Action OnInit
    {
        add
        {
            _loadScene.OnInit += value;
        }

        remove
        {
            _loadScene.OnInit -= value;
        }
    }

    public override bool IsInit => _loadScene.IsInit;

    [SerializeField]
    private LoadTargetScene _loadScene;
        
    [SerializeField] 
    private string _nameScene;

    public void SetKeyScene(string nameScene)
    {
        _nameScene = nameScene;
    }

    public override GetServerRequestData<RequestStartLoadSceneData> StartLoadScene()
    {
        return _loadScene.StartLoadScene(_nameScene);
    }
}
