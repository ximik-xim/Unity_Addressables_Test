using System;
using UnityEngine;


public class GetServerRequestData<Data> 
{
  public GetServerRequestData(ServerRequestDataPublic<Data> data)
  {
    _data = data;
  }

  private ServerRequestDataPublic<Data> _data;
  
  public event Action OnGetDataCompleted
  {
    add
    {
      _data.OnGetDataCompleted += value;
    }

    remove
    {
      _data.OnGetDataCompleted -= value;
    }
  }

  public bool IsGetDataCompleted => _data.IsGetDataCompleted;

  public int IdMassage => _data.IdMassage;

  public Data GetData => _data.GetData;

  public StatusCallBackServer StatusServer => _data.StatusServer;
}
