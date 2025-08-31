using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

/// <summary>
/// Тестовая загр. элемента
/// (пока без в тупую, для проверки)
/// </summary>
public class ButtonLoadDef : MonoBehaviour
{
    [SerializeField]
    private Button _button;
        
    [SerializeField]
    private GameObject _parent;
        
    /// <summary>
    /// ссылка на обьект
    /// </summary>
    [SerializeField] 
    private AssetReference _assetReference;
   
    [SerializeField] 
    private AbsCallbackGetDataAddressables _getDataAddressables;

    
    private void Awake()
    {
        if (_getDataAddressables.IsInit == false)
        {
            _getDataAddressables.OnInit += OnInitGetData;
            return;
        }

        InitGetData();

    }
    private void OnInitGetData()
    {
        if (_getDataAddressables.IsInit == true) 
        {
            _getDataAddressables.OnInit -= OnInitGetData;
            InitGetData();
        }
      
    }
    
    private void InitGetData()
    {
        _button.onClick.AddListener(ButtonClick);
    }


    private void ButtonClick()
    {
        StartLogic();
    }
   
    private void StartLogic()
    {
        Debug.Log("Послан запрос на получения данных GameObject");
        var dataCallback = _getDataAddressables.GetData<GameObject>(_assetReference);

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
            Debug.Log("----- Данные получены ----");
            Debug.Log("Статус запроса = " + dataCallback.StatusServer.ToString());
            Debug.Log("Получен обьект = " + dataCallback.GetData);
            Debug.Log("Проверка на null = " + (dataCallback.GetData == null));

            Instantiate(dataCallback.GetData, _parent.transform);
        }

    }
}
