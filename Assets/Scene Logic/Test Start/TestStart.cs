
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

   private void Start()
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
         
            ю
         //уст порядковый номер(и тут вопрос ТУТ ли это делать или сделать подписку на метод ADD)?
         //Для тестов пока пусть тут будет, но потом наверное в подписку это сделаю(а может быть и нет, т.к в теор. подписка не успеет отраб, а значит номер должна быть проставлена заранее, крч надо думать)
         _storageSceneNumber.SetNumberScene(listKeyScene);
         
         //Добавляю в хран сцены 
         foreach (var VARIABLE in listKeyScene)
         {
            _storageSceneName.AddScene(VARIABLE);
         }
         

      }
   }
}
