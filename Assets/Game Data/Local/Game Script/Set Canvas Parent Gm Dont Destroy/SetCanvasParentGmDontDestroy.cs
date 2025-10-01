
using System;
using UnityEngine;

/// <summary>
/// Нужен что бы перенести Canvas сцены в Dont Destroy когда перейдем на сцену
/// И когда уйдем со сцены, удалить этот Canvas из хранлеща и Destroy этот canvas
/// </summary>
public class SetCanvasParentGmDontDestroy : MonoBehaviour
{
   [SerializeField]
   private GetDKOPatch _getDkoPatch;

   [SerializeField]
   private GameObject _targerCanvas;
   
   [SerializeField]
   private GetDataSO_StorageKeyGM _keySetCanvas;
   
   [SerializeField]
   private GetDataSO_StorageKeyGM _keyGetParent;
   
   private void Awake()
   {
      if (_getDkoPatch.Init == false)
      {
         _getDkoPatch.OnInit += OnInitGetDkoPatch;
      }
        
      CheckInit();
   }
    
   private void OnInitGetDkoPatch()
   {
      if (_getDkoPatch.Init == true)
      {
         _getDkoPatch.OnInit -= OnInitGetDkoPatch;
         CheckInit();
      }
        
   }
   
   private void CheckInit()
   {
      if (_getDkoPatch.Init == true) 
      {
         InitData();
      }
   }

   private void InitData()
   {
      StorageKeyAndGM storageKeyAndGm = _getDkoPatch.GetDKO<DKODataInfoT<StorageKeyAndGM>>().Data;
      storageKeyAndGm.AddGM(_keySetCanvas.GetData(), _targerCanvas);

      GameObject parent = storageKeyAndGm.GetGM(_keyGetParent.GetData());
      _targerCanvas.transform.parent = parent.transform;
      
      _targerCanvas.transform.localPosition = Vector3.zero;
      _targerCanvas.transform.localScale = Vector3.one;
   }

   private void OnDestroy()
   {
      if (_getDkoPatch.Init == true) 
      {
         StorageKeyAndGM storageKeyAndGm = _getDkoPatch.GetDKO<DKODataInfoT<StorageKeyAndGM>>().Data;
         var gm = storageKeyAndGm.GetGM(_keySetCanvas.GetData());
         
         Destroy(gm);
         
         storageKeyAndGm.RemoveGM(_keySetCanvas.GetData());
      }
   }
}
