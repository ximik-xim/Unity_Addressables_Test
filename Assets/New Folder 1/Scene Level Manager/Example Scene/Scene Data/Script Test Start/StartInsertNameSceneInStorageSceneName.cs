
using System;
using UnityEngine;

/// <summary>
/// Заполняет StorageSceneName именами сцен
/// </summary>
public class StartInsertNameSceneInStorageSceneName : MonoBehaviour
{
   [SerializeField]
   private GetDKOPatch _dkoPatchStorageAbsGetKeyScene;
   
   private AbsGetStorageKeyNameScene _controllerAddScene;

   [SerializeField]
   private StorageSceneName _storageSceneName;

   private void Awake()
   {
      if (_dkoPatchStorageAbsGetKeyScene.Init == false)
      {
         _dkoPatchStorageAbsGetKeyScene.OnInit += OnInitDkoPatchStorageAbsGetKeyScene;
      }

      CheckInit();
   }

   private void OnInitDkoPatchStorageAbsGetKeyScene()
   {
      if (_dkoPatchStorageAbsGetKeyScene.Init == true)
      {
         _dkoPatchStorageAbsGetKeyScene.OnInit -= OnInitDkoPatchStorageAbsGetKeyScene;
         CheckInit();
      }
   }
    
   private void CheckInit()
   {
      if (_dkoPatchStorageAbsGetKeyScene.Init == true)
      {
         _controllerAddScene = _dkoPatchStorageAbsGetKeyScene.GetDKO<DKODataInfoT<StorageAbsGetKeyScene>>().Data;

         if (_controllerAddScene.IsInit == false)
         {
            _controllerAddScene.OnInit += OnInitControllerAddScene;
         }
         else
         {
            InitControllerAddScene();
         }
      }
   }

   private void OnInitControllerAddScene()
   {
      if (_controllerAddScene.IsInit == true)
      {
         _controllerAddScene.OnInit -= OnInitControllerAddScene;
         InitControllerAddScene();
      }
   }

   private void InitControllerAddScene()
   {
      //Добавляю в хран сцены 
      foreach (var VARIABLE in _controllerAddScene.GetData())
      {
         _storageSceneName.AddScene(VARIABLE);
      }
   }
}
