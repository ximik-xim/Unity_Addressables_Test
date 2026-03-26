using System;

public interface IGetBoolStatus 
{
    public bool IsInit { get; }
    public event Action OnInit;
    
    public bool GetStatus { get; }
    public event Action OnUpdateStatus;
}
