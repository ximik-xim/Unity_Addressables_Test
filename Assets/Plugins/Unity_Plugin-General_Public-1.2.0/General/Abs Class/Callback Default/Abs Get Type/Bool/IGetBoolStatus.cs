using System;

public interface IGetBoolStatus 
{
    public bool GetStatus { get; }
    public event Action OnUpdateStatus;
}
