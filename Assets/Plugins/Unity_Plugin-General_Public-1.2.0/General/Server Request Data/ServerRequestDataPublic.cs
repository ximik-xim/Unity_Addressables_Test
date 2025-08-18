using System;
using UnityEngine;

public class ServerRequestDataPublic<Data>
{
    public ServerRequestDataPublic(int id)
    {
        _idMassage = id;
    }
    
    public event Action OnGetDataCompleted;
    public bool IsGetDataCompleted = false;

    public int IdMassage => _idMassage;
    private int _idMassage;

    public Data GetData;

    public StatusCallBackServer StatusServer;

    public void Invoke()
    {
        OnGetDataCompleted?.Invoke();
    }
}
