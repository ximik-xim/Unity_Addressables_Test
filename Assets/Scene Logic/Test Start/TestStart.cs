
using System;
using UnityEngine;

public class TestStart : MonoBehaviour
{
   [SerializeField] 
   private ControllerAddScene _controllerAddScene;
   
   [SerializeField] 
   private CreatorPrefabUI _creatorPrefabUI;

   private void Start()
   {
      _controllerAddScene.StartAddScene();
      _creatorPrefabUI.StartCreateUI();
   }
   
   
}
