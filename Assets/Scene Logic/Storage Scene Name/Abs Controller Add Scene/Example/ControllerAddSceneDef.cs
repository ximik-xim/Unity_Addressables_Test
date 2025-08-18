using System;
using UnityEngine;


public class ControllerAddScene : AbsControllerAddScene
{
    public override bool IsInit => true;
    public override event Action OnInit;
    public override event Action OnComplitedAddScene;
    
    [SerializeField] 
    private SO_Data_NameScene _sceneLevel;

    [SerializeField] 
    private StorageSceneName _storageScene;

    public override void StartAddScene()
    {
        //получ. список сцен
        var listScene = _sceneLevel.GetAllData();

        foreach (var VARIABLE in listScene)
        {
            _storageScene.AddScene(VARIABLE);
        }
        
    }
}
