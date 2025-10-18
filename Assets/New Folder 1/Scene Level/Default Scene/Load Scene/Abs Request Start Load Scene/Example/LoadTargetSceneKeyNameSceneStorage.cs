using System;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Нужен что бы удобно указать ключ сцены через хранилище с ключами
/// </summary>
public class LoadTargetSceneKeyNameSceneStorage : AbsRequestStartLoadScene
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
    private GetDataSO_NameSceneAndKeyString _nameScene;

    public override GetServerRequestData<RequestStartLoadSceneData> StartLoadScene()
    {
        return _loadScene.StartLoadScene(_nameScene.GetData().GetKey());
    }
    


    
}
