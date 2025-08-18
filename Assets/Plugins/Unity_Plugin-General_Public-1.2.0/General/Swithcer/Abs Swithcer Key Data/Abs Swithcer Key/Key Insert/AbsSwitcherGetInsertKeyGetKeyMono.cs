using UnityEngine;

public class AbsSwitcherGetInsertKeyGetKeyMono<Key,GetKey,GetKeyDataMono,KeyData> : AbsSwitcherKeyData<Key> where GetKeyDataMono: AbsGetKeyDataMono<GetKey,KeyData> where KeyData: AbsGetKeyData<GetKey> where GetKey : IGetKey<Key>
{
    //Ожидаемо, так вышла полная шляпа т.к нету конкретного типа
    // [SerializeField] 
    // private sadasd<Key> _getKeyDataMono;
    
    [SerializeField] 
    protected GetKeyDataMono _getKeyDataMono;

    //Сдесь тоже по хорошему нужен конкретный тип данных
    // [SerializeField]
    // private awrwa<Key> _keyData;
    //[SerializeField]
    protected KeyData _keyData;
    
    protected override Key GetCurrentKey()
    {
        return _keyData.GetCurrentKey.GetKey();
    }
    
    
    protected void StartAwake()
    {
        
        if (_getKeyDataMono.IsInit == false)
        {
            _getKeyDataMono.OnInit += OnInitGetData;  
            return;
        }

        InitGetData();

    }

    private void OnInitGetData()
    {
        _getKeyDataMono.OnInit -= OnInitGetData;
        InitGetData();
    }


    private void InitGetData()
    {
        _keyData = _getKeyDataMono.GetKeyData();
            
        if (_checkEvent == true)
        {
            _keyData.OnUpdateKey += CheckEvent;
        }
        
        CheckAwake();
            
    }
    
    private void OnDestroy()
    {
        if (_keyData != null)
        {
            _keyData.OnUpdateKey -= CheckEvent;    
        }
    }
}
