using System;

public class SetAndGetStatusBool : IGetBoolStatus
{
    public bool Status;
    /// <summary>
    /// Будет вызываться при попытки взятия занчения(в этот момент по идеи я и буду подменять значение)
    /// </summary>
    public event Action OnGetValue;

    public bool GetStatus => GetBoolStatus();
    public event Action OnUpdateStatus;

    private bool GetBoolStatus()
    {
        OnGetValue?.Invoke();
        return Status;
    }
    
    public void InvokeUpdateStatus()
    {
       OnUpdateStatus?.Invoke();
    }
    
}
