using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

/// <summary>
/// Логика для получения через Asset Ref обычные ключи сцены(PrimaryKey)
/// Обертка над хранилещем с Asset Ref Scene
/// Есть возможность указать исключение, и указать другой ключ
/// </summary>
public class GetKeyNameSceneStorageAssetRefScene : AbsGetStorageKeyNameScene
{
    public override event Action OnInit;
    public override bool IsInit => _isInit;
    private bool _isInit = false;
    
    [SerializeField]
    private SO_Data_KeyReferenceScene _storageKeyRef;
    
    /// <summary>
    /// Список исключений
    /// (на случай,если надо что бы по ссылке на сцену вернулся какой то опр. ключ)
    /// </summary>
    [SerializeField]
    private List<AbsKeyData<GetDataSO_KeyReferenceScene, string>> _listExceptions = new List<AbsKeyData<GetDataSO_KeyReferenceScene, string>>();
    private Dictionary<object, string> _exceptionsData = new Dictionary<object, string>();

    /// <summary>
    /// Сериализоан для тестов
    /// </summary>
    [SerializeField]
    private List<KeyNameScene> _listKeyScene;
    
    private void Awake()
    {
        foreach (var VARIABLE in _listExceptions)
        {
            _exceptionsData.Add(VARIABLE.Key.GetData().GetRefScene().RuntimeKey, VARIABLE.Data);
        }

        //Получаю список Ref Scene
        List<KeyReferenceScene> listKey = _storageKeyRef.GetAllData();


        List<AsyncOperationHandle<IList<IResourceLocation>>> listCallback = new List<AsyncOperationHandle<IList<IResourceLocation>>>();

        //нужен для сохранения порядкового номера(костыль)
        List<AsyncOperationHandle<IList<IResourceLocation>>> listCallback2 = new List<AsyncOperationHandle<IList<IResourceLocation>>>();
        
        
        bool _isStart = false;

        StartLogic();

        //Получаю нормальный ключ для кажд Ref Scene
        void StartLogic()
        {
            _isStart = true;

            for (int i = 0; i < listKey.Count; i++)
            {
                //если эта ссылка на сцену не наход в списке исключ
                if (_exceptionsData.ContainsKey(listKey[i].GetRefScene().RuntimeKey) == false)
                {
                    //то логика опр. ключа для неё стандартная
                    var callback = Addressables.LoadResourceLocationsAsync(listKey[i].GetRefScene().RuntimeKey);
                    listCallback2.Add(callback);
                    
                    if (callback.IsDone == false)
                    {
                        listCallback.Add(callback);
                        callback.Completed += CheckCompletedCallback;
                    }
                }
            }

            _isStart = false;

            CheckCompleted();
        }

        void CheckCompletedCallback(AsyncOperationHandle<IList<IResourceLocation>> data)
        {
            CheckCompleted();
        }

        void CheckCompleted()
        {
            if (_isStart == false)
            {
                int targetCount = listCallback.Count;
                for (int i = 0; i < targetCount; i++)
                {
                    if (listCallback[i].IsDone == true)
                    {
                        listCallback[i].Completed -= CheckCompletedCallback;
                        listCallback.RemoveAt(i);
                        i--;
                        targetCount--;
                    }
                }

                if (listCallback.Count == 0)
                {
                    Completed();
                }
            }
        }

        void Completed()
        {
       
            List<KeyNameScene> listKeyScene = new List<KeyNameScene>();
                
            //что бы сохранить порядковый номер, пришлось немного покостылить,
            //добавить список callback что бы сохранить очередность
            //и дважды проверять исключения
            int num = 0;
            for (int i = 0; i < listKey.Count; i++)
            {
                //если эта ссылка на сцену не наход в списке исключ
                if (_exceptionsData.ContainsKey(listKey[i].GetRefScene().RuntimeKey) == false)
                {
                    listKeyScene.Add(new KeyNameScene(listCallback2[num].Result[0].PrimaryKey));
                    
                    if (listCallback2[num].IsValid() == true) 
                    {
                        Addressables.Release(listCallback2[num]);
                    }
                    
                    num++;
                }
                else
                {
                    // иначе, беру указ в инспекторе ключ
                    listKeyScene.Add(new KeyNameScene(_exceptionsData[listKey[i].GetRefScene().RuntimeKey]));
                }
            }


            _listKeyScene = listKeyScene;
            
            _isInit = true;
            OnInit?.Invoke();
        }
    }

  
    
    public override List<KeyNameScene> GetData()
    {
        return _listKeyScene;
    }
}
