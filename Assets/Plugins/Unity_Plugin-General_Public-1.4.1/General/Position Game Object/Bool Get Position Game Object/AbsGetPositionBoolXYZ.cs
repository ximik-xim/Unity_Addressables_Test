using UnityEngine;

public abstract class AbsGetPositionBoolXYZ : MonoBehaviour
{
   [SerializeField] 
   protected bool _returnX;
   [SerializeField] 
   protected bool _returnY;
   [SerializeField] 
   protected bool _returnZ;

   public bool ReturnX()
   {
      return _returnX;
   }
   
   public bool ReturnY()
   {
      return _returnY;
   }
   
   public bool ReturnZ()
   {
      return _returnZ;
   }
   
   public abstract Vector3 GetPosition();
}
