
using System;
using UnityEngine;

public class TestStart : MonoBehaviour
{
   [SerializeField] 
   private ControllerAddScene _controllerAddScene;
   
   [SerializeField] 
   private CreatorPrefabUI _creatorPrefabUI;

   private void Awake()
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
         _controllerAddScene.StartAddScene();
         _creatorPrefabUI.StartCreateUI();
      }
   }
}
