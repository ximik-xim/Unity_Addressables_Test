using System;
using UnityEngine;

/// <summary>
/// Нужен что бы получить статус для все побочных камер(и если надо, то отключить их)
/// (во время разработки камеры нужны, но в готовом проекте они должны быть отключены, но они все еще нужны,
/// для логики маштабирования 1 к 1)
/// </summary>
public class GetDKO_StatusActiveCamera : MonoBehaviour
{
   [SerializeField]
   private GetDKOPatch _dkoPatchStatusOtherCamer;

   [SerializeField]
   private GameObject _cameraGM;
   
   private void Awake()
   {
      if (_dkoPatchStatusOtherCamer.Init == false)
      {
         _dkoPatchStatusOtherCamer.OnInit += OnInitGetDkoPatch;
      }
        
      CheckInit();
   }
    
   private void OnInitGetDkoPatch()
   {
      if (_dkoPatchStatusOtherCamer.Init == true)
      {
         _dkoPatchStatusOtherCamer.OnInit -= OnInitGetDkoPatch;
         CheckInit();
      }
        
   }
   
   private void CheckInit()
   {
      if (_dkoPatchStatusOtherCamer.Init == true) 
      {
         InitData();
      }
   }

   private void InitData()
   {
      StatusActiveOtherCamera statusActiveCamera = _dkoPatchStatusOtherCamer.GetDKO<DKODataInfoT<StatusActiveOtherCamera>>().Data;
      _cameraGM.SetActive(statusActiveCamera.GetStatusActiveOtherCamera());
   }
}
