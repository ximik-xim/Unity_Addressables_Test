using UnityEngine;

public abstract class AbsServerRequestDataWrapper<DataType>
{
   public AbsServerRequestDataWrapper(int id)
   {
      _data = new ServerRequestDataPublic<DataType>(id);
      _dataGet = new GetServerRequestData<DataType>(_data);
   }
   
   private ServerRequestDataPublic<DataType> _data;
   public ServerRequestDataPublic<DataType> Data => _data;
   
   private GetServerRequestData<DataType> _dataGet;
   public GetServerRequestData<DataType> DataGet => _dataGet;
   
}
