using UnityEngine;

public class GetTransPositionXYZGizmo : AbsGetPositionBoolXYZ
{
   [SerializeField] 
   private GameObject _targetPos;

   public override Vector3 GetPosition()
   {
      return _targetPos.transform.position;
   }
   
#if  UNITY_EDITOR
    
   [SerializeField] 
   private Color _colorGizmo = Color.green;
    
   private void OnDrawGizmos()
   {
      Gizmos.color = _colorGizmo;
      Gizmos.DrawCube(this._targetPos.transform.position, new Vector3(0.2f, 0.2f, 0.2f));
   }
    
#endif
}
