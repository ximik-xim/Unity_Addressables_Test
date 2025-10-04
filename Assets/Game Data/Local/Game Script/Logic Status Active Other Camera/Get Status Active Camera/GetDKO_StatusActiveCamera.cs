using System;
using UnityEngine;

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
