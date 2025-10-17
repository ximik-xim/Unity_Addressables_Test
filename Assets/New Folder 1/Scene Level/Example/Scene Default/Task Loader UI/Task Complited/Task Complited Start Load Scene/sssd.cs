using UnityEngine;

public class SceneStartTaskComplitedStartLoadScene : MonoBehaviour
{
    [SerializeField]
    private SceneStartTask _sceneStartTask;
    
    [SerializeField]
    private AbsRequestStartLoadScene _sceneLoad;

    private GetServerRequestData<RequestStartLoadSceneData> callback;

    /// <summary>
    /// Производит ли автоматически удаление данных сцены из опер. памяти при переходе на другую сцену
    /// </summary>
    [SerializeField]
    private bool _isAutoUnloadScene = true;

    
    private void Awake()
    {
        if (_sceneStartTask.StorageTaskLoader == null)
        {
            _sceneStartTask.OnSetStorageTaskLoader += OnSetListTask;
        }
        else
        {
            SetListTask();
        }
    }
    
    private void OnSetListTask()
    {
        if (_sceneStartTask.StorageTaskLoader != null)
        {
            _sceneStartTask.OnSetStorageTaskLoader -= OnSetListTask;

            SetListTask();
        }
    }

    private void SetListTask()
    {
        _sceneStartTask.StorageTaskLoader.OnCompleted += OnCompletedTaskLoad;
    }

    private void OnCompletedTaskLoad()
    {
        _sceneStartTask.StorageTaskLoader.OnCompleted -= OnCompletedTaskLoad;
        
        DontDestroyOnLoad(this.gameObject);
        this.gameObject.transform.parent = null;


        callback = _sceneLoad.StartLoadScene();


        if (callback.IsGetDataCompleted == false)
        {
            callback.OnGetDataCompleted += OnGetDataCompleted;
        }
        else
        {
            GetDataCompleted();
        }

        void OnGetDataCompleted()
        {
            if (callback.IsGetDataCompleted == true)
            {
                callback.OnGetDataCompleted -= OnGetDataCompleted;
                GetDataCompleted();
            }
        }

        void GetDataCompleted()
        {
            Destroy(this.gameObject);
        }

    }
}
