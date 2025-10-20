using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

/// <summary>
/// Логика для получения списка имен сцен и ключей к ним для Asset Ref Scene
/// </summary>
public class GetNameSceneAndKeyNameSceneInStorageReferenceScene : AbsGetNameSceneAndKeyNameScene
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
    
    private List<AbsKeyData<string, KeyNameScene>> _listNameSceneAndKey;
    private void Awake()
    {
        //Получаю список Ref Scene
        List<KeyReferenceScene> listKey = _storageKeyRef.GetAllData();
   
        
        foreach (var VARIABLE in _listExceptions)
        {
            _exceptionsData.Add(VARIABLE.Key.GetData().GetRefScene().RuntimeKey, VARIABLE.Data);
        }
        
        StartGetListNameSceneAndKey(listKey);
    }
    
    
    private void StartGetListNameSceneAndKey(List<KeyReferenceScene> list)
    {
        //Нужен что бы в конце создать пару (имя сцены, ключ к ней)
        List<KeyReferenceScene> buffer = list;
        
        //Нужен что бы можно было соотнести Scene Ref и ключ к ней
        Dictionary<object, AsyncOperationHandle<IList<IResourceLocation>>> _data = new Dictionary<object, AsyncOperationHandle<IList<IResourceLocation>>>();
        
        List<AsyncOperationHandle<IList<IResourceLocation>>> listCallback = new List<AsyncOperationHandle<IList<IResourceLocation>>>();
        
        bool _isStart = false;

        StartLogic();

        void StartLogic()
        {
            _isStart = true;
            
            for (int i = 0; i < buffer.Count; i++)
            {
                if (_exceptionsData.ContainsKey(buffer[i].GetRefScene().RuntimeKey) == false)
                {
                    var callback = Addressables.LoadResourceLocationsAsync(buffer[i].GetRefScene().RuntimeKey);
                    _data.Add(buffer[i].GetRefScene().RuntimeKey, callback);

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
            List<AbsKeyData<string,KeyNameScene>> listKeyScene = new List<AbsKeyData<string,KeyNameScene>>();
            
            foreach (var VARIABLE in buffer)
            {
                if (_exceptionsData.ContainsKey(VARIABLE.GetRefScene().RuntimeKey) == false)
                {
                    string key = _data[VARIABLE.GetRefScene().RuntimeKey].Result[0].PrimaryKey;
                    listKeyScene.Add(new AbsKeyData<string, KeyNameScene>(VARIABLE.GetNameScene(), new KeyNameScene(key)));

                    if (_data[VARIABLE.GetRefScene().RuntimeKey].IsValid() == true)
                    {
                        Addressables.Release(_data[VARIABLE.GetRefScene().RuntimeKey]);
                    }
                }
                else
                {
                    listKeyScene.Add(new AbsKeyData<string, KeyNameScene>(VARIABLE.GetNameScene(), new KeyNameScene(_exceptionsData[VARIABLE.GetRefScene().RuntimeKey])));
                }
            }

            _listNameSceneAndKey = listKeyScene;
            
            _isInit = true;
            OnInit?.Invoke();
        }
    }
    
    public override List<AbsKeyData<string, KeyNameScene>> GetData()
    {
        return _listNameSceneAndKey;
    }

}
