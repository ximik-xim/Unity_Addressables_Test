using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

[System.Serializable]
/// <summary>
/// Логика для получения через Asset Ref обычные ключи сцены
/// Обертка над обычной логикой получ. ключа 
/// </summary>
public class KeyNameSceneInGetDataSO_KeyReferenceScene 
{
    public event Action OnInit;
    public bool IsInit => _isInit;
    private bool _isInit = false;
    
   [SerializeField]
   private GetDataSO_KeyReferenceScene _getKeyRefScene;

   private KeyNameScene _keyNameScene;
   
   public void StartInit()
   {
       var callback = Addressables.LoadResourceLocationsAsync(_getKeyRefScene.GetData().GetRefScene().RuntimeKey);

       if (callback.IsDone == false)
       {
           callback.Completed += OnCheckCompletedCallback;
       }
       else
       {
           CheckCompletedCallback();
       }
       
       void OnCheckCompletedCallback(AsyncOperationHandle<IList<IResourceLocation>> obj)
       {
           if (callback.IsDone == true)
           {
               callback.Completed -= OnCheckCompletedCallback;
               CheckCompletedCallback();
           }
       }
       
       void CheckCompletedCallback()
       {
           _keyNameScene = new KeyNameScene(callback.Result[0].PrimaryKey);
           
           _isInit = true;
           OnInit?.Invoke();
       }
   }

   public KeyNameScene GetKeySceneName()
   {
       return _keyNameScene;
   }
}
