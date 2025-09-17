using System;
using UnityEngine;

public class SceneLoaderNumberDefault : MonoBehaviour
{
   [SerializeField] 
   private int _numberLoadScene;
   
   [SerializeField] 
   private AbsSceneLoader _sceneLoader;

   [SerializeField] 
   private bool _useAwake = false;
   
   [SerializeField] 
   private bool _useStart = false;
   
   [SerializeField] 
   private bool _useOnEnable = false;
   
   [SerializeField] 
   private bool _useOnDisable = false;
   
   [SerializeField] 
   private bool _useOnDestroy = false;

   public void StartLoadScene()
   {
      _sceneLoader.LoadScene(_numberLoadScene);
   }

   private void Awake()
   {
      if (_useAwake == true)
      {
         StartLoadScene();
      }
   }
   
   private void Start()
   {
      if (_useStart == true)
      {
         StartLoadScene();
      }
   }
   
   private void OnEnable()
   {
      if (_useOnEnable == true)
      {
         StartLoadScene();
      }
   }
   
   private void OnDisable()
   {
      if (_useOnDisable == true)
      {
         StartLoadScene();
      }
   }
   
   private void OnDestroy()
   {
      if (_useOnDestroy == true)
      {
         StartLoadScene();
      }
   }
   
}
