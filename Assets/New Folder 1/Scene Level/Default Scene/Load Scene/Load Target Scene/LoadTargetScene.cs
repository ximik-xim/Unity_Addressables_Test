using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTargetScene : MonoBehaviour
{
    public event Action OnInit;
    public bool IsInit => true;
    
    [SerializeField]
    private LoadSceneParameters _sceneParameters;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public GetServerRequestData<RequestStartLoadSceneData> StartLoadScene(string nameScene)
    {
        return StartLoadSceneLogic(nameScene, _sceneParameters);
    }

    public GetServerRequestData<RequestStartLoadSceneData> StartLoadScene(string nameScene, LoadSceneParameters sceneParameters)
    {
        return StartLoadSceneLogic(nameScene, sceneParameters);
    }

    private GetServerRequestData<RequestStartLoadSceneData> StartLoadSceneLogic(string nameScene, LoadSceneParameters sceneParameters)
    {
        CallbackRequestDataWrapperT<RequestStartLoadSceneData> callbackData = new CallbackRequestDataWrapperT<RequestStartLoadSceneData>(0);

        var callback = SceneManager.LoadSceneAsync(nameScene, sceneParameters);

        if (callback.isDone == false)
        {
            callback.completed += OnCompleted;
        }
        else
        {
            Complited();
        }
        
        
        void OnCompleted(AsyncOperation obj)
        {
            if (callback.isDone == true)
            {
                callback.completed -= OnCompleted;
                Complited();
            }
            
        }

        void Complited()
        {
            //У AsyncOperation нет поля опр. успешна ли прошла операция, по этому, даже если сцена не загрузиться буду возр TRUE
            callbackData.Data.StatusServer = StatusCallBackServer.Ok;
            callbackData.Data.GetData = new RequestStartLoadSceneData();

            callbackData.Data.IsGetDataCompleted = true;
            callbackData.Data.Invoke();
        }
        
        return callbackData.DataGet;
    }
}
