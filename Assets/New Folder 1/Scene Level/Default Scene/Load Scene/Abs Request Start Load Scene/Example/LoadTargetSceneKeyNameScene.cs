using System;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Нужен что бы удобно указать ключ сцены через инспектор
/// </summary>
public class LoadTargetSceneKeyNameScene : AbsRequestStartLoadScene
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
