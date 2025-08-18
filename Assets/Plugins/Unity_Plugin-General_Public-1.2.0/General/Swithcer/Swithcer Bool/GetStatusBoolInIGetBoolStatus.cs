using System;

public class GetStatusBoolInIGetBoolStatus : IGetBoolStatus 
{
   // public bool Status;
   //
   // public void InvokeUpdateStatus()
   // {
   //    OnUpdateStatus?.Invoke();
   // }

   private IGetBoolStatus _getBoolStatus;
   public GetStatusBoolInIGetBoolStatus(IGetBoolStatus getBoolStatus)
   {
      _getBoolStatus = getBoolStatus;
   }
   public bool GetStatus => _getBoolStatus.GetStatus;
   public event Action OnUpdateStatus
   {
      add
      {
         _getBoolStatus.OnUpdateStatus += value;
      }
      remove
      {
         _getBoolStatus.OnUpdateStatus -= value;
      }
   }
}
