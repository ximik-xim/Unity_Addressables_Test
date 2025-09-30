using UnityEngine;
using UnityEngine.AddressableAssets;

public class TestGetTListV1 : MonoBehaviour
{
    [SerializeField]
    private AssetReference _key;
   
    [SerializeField] 
    private AbsCallbackGetDataTAddressables _getDataAddressables;

    [SerializeField]
    private GetDataSO_NameScene _dataSoNameScene;
    
    private void Awake()
    {
        if (_getDataAddressables.IsInit == false)
        {
            Debug.Log("Ожид. Иниц  get");
            _getDataAddressables.OnInit += OnInitGetData;
            return;
        }

        InitGetData();

    }
    private void OnInitGetData()
    {
        if (_getDataAddressables.IsInit == true) 
        {
            Debug.Log("законч ожит иниц  get");
            _getDataAddressables.OnInit -= OnInitGetData;
            InitGetData();
        }
      
    }
   
    private void InitGetData()
    {
        Debug.Log("Послан запрос на получения данных GameObject");
        var dataCallback = _getDataAddressables.GetData<SO_Data_NameScene>(_key);

        if (dataCallback.IsGetDataCompleted == true)
        {
            CompletedGetData();
        }
        else
        {
            dataCallback.OnGetDataCompleted += OnCompletedGetData;
        }
      
        void OnCompletedGetData()
        {
            if (dataCallback.IsGetDataCompleted == true)
            {
                dataCallback.OnGetDataCompleted -= OnCompletedGetData;
                CompletedGetData();
            }
        }

        void CompletedGetData()
        {
            if (dataCallback.StatusServer == StatusCallBackServer.Ok) 
            {
                Debug.Log("ПОЛУЧЕННЫЦ ИНДИФ = " + _dataSoNameScene.GetSOIndif());
                Debug.Log("ПОЛУЧЕННЫЦ Данные = " + _dataSoNameScene.GetData().GetKey());
            }
            else
            {
                Debug.LogError("Ошибка, при загрузки хранилеща со списком сцен из Addrassable");
            }

        }

    }
}
