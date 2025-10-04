
using System;
using UnityEngine;

public class TestStart : MonoBehaviour
{
   [SerializeField] 
   private StorageAbsGetKeyScene _controllerAddScene;

   [SerializeField]
   private StorageSceneName _storageSceneName;

   [SerializeField]
   private LogicSortingSceneLevel _logicSortingSceneLevel;

   [SerializeField]
   private StorageSceneNumber _storageSceneNumber;
   
   [SerializeField] 
   private FabricPrefabUI _creatorPrefabUI;

   [SerializeField]
   private SceneStartTask _sceneStartTask;

   private void Awake()
   {
      if (_sceneStartTask.IsInit == false)
      {
         _sceneStartTask.OnInit += OnInitSceneTask;
      }
      else
      {
         InitSceneTask();
      }
   }

   private void OnInitSceneTask()
   {
      if (_sceneStartTask.IsInit == true)
      {
         _sceneStartTask.OnInit -= OnInitSceneTask;
         InitSceneTask();
      }
   }

   private void InitSceneTask()
   {
      if (_controllerAddScene.IsInit == false)
      {
         _controllerAddScene.OnInit += OnInitControllerAddScene;
      }
      
      if (_creatorPrefabUI.IsInit == false)
      {
         _creatorPrefabUI.OnInit += OnInitCreatorPrefabUI;
      }

      CheckInit();
   }
    
   private void OnInitControllerAddScene()
   {
      if (_controllerAddScene.IsInit == true) 
      {
         _controllerAddScene.OnInit -= OnInitControllerAddScene;
         CheckInit();
      }
   }
   
   private void OnInitCreatorPrefabUI()
   {
      if (_creatorPrefabUI.IsInit == true) 
      {
         _creatorPrefabUI.OnInit -= OnInitCreatorPrefabUI;
         CheckInit();
      }
   }
    
   private void CheckInit()
   {
      if (_controllerAddScene.IsInit == true && _creatorPrefabUI.IsInit == true)
      {
         var listKeyScene = _controllerAddScene.GetAllKeyScene();

         //получ. отсортир. по порядку список сцен
         listKeyScene = _logicSortingSceneLevel.SortingNumberScene(listKeyScene, 1);
         
         //Да, его нужно вручную отдельно уст. т.к надо знать под каким поряд. номером должна наход сцена
         _storageSceneNumber.SetNumberScene(listKeyScene);
         
         //Добавляю в хран сцены 
         foreach (var VARIABLE in listKeyScene)
         {
            _storageSceneName.AddScene(VARIABLE);
         }
         

      }
   }
}
