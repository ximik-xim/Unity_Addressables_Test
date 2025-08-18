using System;
using UnityEngine;

public class DublicatePositionGameObject : MonoBehaviour
{
      [SerializeField] 
      private GameObject _targetObj;
      
      [SerializeField] 
      private GameObject _currentObj;
      
      [SerializeField] 
      protected TypeDublicatePositionGameObject _dublicateX = TypeDublicatePositionGameObject.Dublicate;
      [SerializeField] 
      protected TypeDublicatePositionGameObject _dublicateY = TypeDublicatePositionGameObject.Dublicate;
      [SerializeField] 
      protected TypeDublicatePositionGameObject _dublicateZ = TypeDublicatePositionGameObject.Dublicate;

      private bool _isStart = false;

      [SerializeField] 
      private Vector3 _offset = Vector3.zero;
      
      [SerializeField] 
      private bool _startAwake = false;
      
      private void Awake()
      {
          if (_startAwake == true)
          {
              StartLogic();   
          }
      }

      private void FixedUpdate()
      {
          if (_isStart == true)
          {
              Vector3 vector3 = _currentObj.transform.position - _offset;
              
              switch (_dublicateX)
              {
                  case TypeDublicatePositionGameObject.Dublicate:
                  {
                      vector3.x = _targetObj.transform.position.x;
                  }
                      break;
                  
                  
                  case TypeDublicatePositionGameObject.DublicateMoreValue:
                  {
                      if (_currentObj.transform.position.x < _targetObj.transform.position.x + _offset.x) 
                      {
                          vector3.x = _targetObj.transform.position.x;
                      }
                  }
                      break;
                  
                  case TypeDublicatePositionGameObject.DublicateLessValue:
                  {
                      if (_currentObj.transform.position.x > _targetObj.transform.position.x + _offset.x) 
                      {
                          vector3.x = _targetObj.transform.position.x;
                      }
                      
                  }
                      break;
              }
              
              switch (_dublicateY)
              {
                  case TypeDublicatePositionGameObject.Dublicate:
                  {
                      vector3.y = _targetObj.transform.position.y;
                  }
                      break;
                  
                  
                  case TypeDublicatePositionGameObject.DublicateMoreValue:
                  {
                      if (_currentObj.transform.position.y < _targetObj.transform.position.y + _offset.y) 
                      {
                          vector3.y = _targetObj.transform.position.y;
                      }
                  }
                      break;
                  
                  case TypeDublicatePositionGameObject.DublicateLessValue:
                  {
                      if (_currentObj.transform.position.y > _targetObj.transform.position.y + _offset.y) 
                      {
                          vector3.y = _targetObj.transform.position.y;
                      }
                      
                  }
                      break;
              }
              
              switch (_dublicateZ)
              {
                  case TypeDublicatePositionGameObject.Dublicate:
                  {
                      vector3.z = _targetObj.transform.position.z;
                  }
                      break;
                  
                  case TypeDublicatePositionGameObject.DublicateMoreValue:
                  {
                      if (_currentObj.transform.position.z < _targetObj.transform.position.z + _offset.z) 
                      {
                          vector3.z = _targetObj.transform.position.z;
                      }
                  }
                      break;
                  
                  case TypeDublicatePositionGameObject.DublicateLessValue:
                  {
                      if (_currentObj.transform.position.z > _targetObj.transform.position.z + _offset.z) 
                      {
                          vector3.z = _targetObj.transform.position.z;
                      }
                  }
                      break;
              }

              _currentObj.transform.position = vector3 + _offset;
          }
      }

      public void StartLogic()
      {
          _isStart = true;
      }

      public void StopLogic()
      {
          _isStart = false;
      }
      
}

public enum TypeDublicatePositionGameObject
{
    None,
    Dublicate,
    DublicateMoreValue,
    DublicateLessValue
}