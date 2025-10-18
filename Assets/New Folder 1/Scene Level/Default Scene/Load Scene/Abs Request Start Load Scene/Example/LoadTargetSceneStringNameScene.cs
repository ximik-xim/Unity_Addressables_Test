using System;
using UnityEngine;

/// <summary>
/// Нужен что бы удобно указать имя сцены через инспектор
/// </summary>
public class LoadTargetSceneStringNameScene : AbsRequestStartLoadScene
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

    public override GetServerRequestData<RequestStartLoadSceneData> StartLoadScene()
    {
        return _loadScene.StartLoadScene(_nameScene);
    }

}
