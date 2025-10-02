
using System;
using UnityEngine;

/// <summary>
/// Нужен что бы установить у GM родителя взятого из Key Storage GM 
/// При уничтожении скрипта, можно уничтожить GM обьект
/// </summary>
public class SetCanvasParentGmDontDestroy : MonoBehaviour
{
   /// <summary>
   /// Уничтожать ли GM при Destroy этого скрипта
   /// </summary>
   [SerializeField]
   private bool _isGmDestroy;
   
   [SerializeField]
   private GetDKOPatch _getDkoPatch;
   
   [SerializeField]
   private ListActionGmSetParent _setParent;
   
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

      GameObject parent = storageKeyAndGm.GetGM(_keyGetParent.GetData());
      _setParent.StartAction(parent);
   }

   private void OnDestroy()
   {
      if (_isGmDestroy == true)
      {
         foreach (var VARIABLE in _setParent.GetListGm())
         {
            Destroy(VARIABLE);
         }
      }
   }
}
