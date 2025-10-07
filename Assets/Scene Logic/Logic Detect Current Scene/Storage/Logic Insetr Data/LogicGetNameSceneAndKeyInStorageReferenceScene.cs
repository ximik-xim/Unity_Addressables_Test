using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

/// <summary>
/// Логика для получения списка имен сцен и ключей к ним для Asset Ref Scene
/// </summary>
public class LogicGetNameSceneAndKeyInStorageReferenceScene : MonoBehaviour
{
    
    public event Action OnInit;
    public bool IsInit => _isInit;
    private bool _isInit = false;
    
    [SerializeField]
    private SO_Data_KeyReferenceScene _storageKeyRef;

    private List<AbsKeyData<string, KeyNameScene>> _listNameSceneAndKey;
    public List<AbsKeyData<string, KeyNameScene>> GetListNameSceneAndKey => _listNameSceneAndKey;
    private void Awake()
    {
        //Получаю список Ref Scene
        List<KeyReferenceScene> listKey = _storageKeyRef.GetAllData();
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

                var callback = Addressables.LoadResourceLocationsAsync(buffer[i].GetRefScene().RuntimeKey);
                _data.Add(buffer[i].GetRefScene().RuntimeKey, callback);

                if (callback.IsDone == false)
                {
                    listCallback.Add(callback);
                    callback.Completed += CheckCompletedCallback;
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
                string key = _data[VARIABLE.GetRefScene().RuntimeKey].Result[0].PrimaryKey;
                listKeyScene.Add(new AbsKeyData<string, KeyNameScene>(VARIABLE.GetNameScene(), new KeyNameScene(key)));
            }

            _listNameSceneAndKey = listKeyScene;
            
            _isInit = true;
            OnInit?.Invoke();
        }
    }
}
