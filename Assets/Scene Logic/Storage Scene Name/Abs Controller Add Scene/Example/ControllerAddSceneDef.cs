using System;
using UnityEngine;


public class ControllerAddScene : AbsControllerAddScene
{
    public override bool IsInit => true;
    public override event Action OnInit;
    public override event Action OnComplitedAddScene;
    
    [SerializeField] 
    private AbsGetStorageKeyNameScene _sceneLevel;

    [SerializeField] 
    private StorageSceneName _storageScene;

    public override void StartAddScene()
    {
        //получ. список сцен
        var listScene = _sceneLevel.GetData().GetAllData();

        foreach (var VARIABLE in listScene)
        {
            _storageScene.AddScene(VARIABLE);
        }
        
    }
}
