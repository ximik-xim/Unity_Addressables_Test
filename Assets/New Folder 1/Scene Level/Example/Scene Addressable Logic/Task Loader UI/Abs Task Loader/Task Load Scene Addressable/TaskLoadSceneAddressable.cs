using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

/// <summary>
/// Отвечает за загрузку и переход на сцену через Task
/// В итоге должен сам уничтожиться на следующей сцене(когда будет происходить уходи на другую сцену)
/// </summary>
public class TaskLoadSceneAddressable : AbsTaskLoggerLoaderDataMono
{
    [Header("---------")]
    [SerializeField]
    private AbsRequestStartLoadSceneAddressable _sceneLoad;

    private AsyncOperationHandle<SceneInstance> _handleScene;

    /// <summary>
    /// Производит ли автоматически удаление данных сцены из опер. памяти при переходе на другую сцену
    /// </summary>
    [SerializeField]
    private bool _isAutoUnloadScene = true;

    
    private void Awake()
    {
        if (_sceneLoad.IsInit == false)
        {
            _sceneLoad.OnInit += OnInitSceneLoad;
        }
        
        CheckInit();
    }
    
    private void OnInitSceneLoad()
    {
        if (_sceneLoad.IsInit == true)
        {
            _sceneLoad.OnInit -= OnInitSceneLoad;
            CheckInit();
        }
    }
    
    private void CheckInit()
    {
        if (_sceneLoad.IsInit == true)
        {
            Init();
        }
    }
    
    public override TaskLoaderData GetTaskInfo()
    {
        if (_taskData == null) 
        {
            InitTask();
        }
        
        //тут убир. авто иниц.
        
        return _taskData;
    }

    protected override void StartLogic()
    {
        UpdatePercentage(50f);

        _storageLog.DebugLog(_storageTypeLog.GetKeyDefaultLog(), "- Начинаю загрузку сцены");
        
        if (_isAutoUnloadScene == true)
        {
            DontDestroyOnLoad(this.gameObject);
            this.gameObject.transform.parent = null;
        }

        var callback = _sceneLoad.StartLoadScene();

        if (_isAutoUnloadScene == true)
        {
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
                _handleScene = callback.GetData;


                StartAutoUnloadScene();
            }
        }

    }



    protected override void BreakTask()
    {
    
    }

    protected override void ResetStatusTask()
    {
    
    }
    
    
    // т.к при загрузку сцены сразу и переходим на неё, то вызыв OnDestroy будет осуществлен в момент перехода с текущей сцена на сцену котор загружаем,
    // а значит если в этот момент вызову Addressables.UnloadSceneAsync, то получиться, что я загружаю сцену XXXX и сразу же эту сцену(XXXX) уничтожаю 
    // что мне не подходит
    //     
    //По этому задумка в след. До момента перехода на сцену XXXX(которую загружаем), мы делаем GM на котором находиться этот скрипт DontDestroy
    //И когда перейдем на сцену XXXX, то этот GM переместим с DontDestroy на сцену XXXX. Тем самым сделаем так, что бы метод OnDestroy сработал, когда будем уходить со сцены,
    //а значит и в этом OnDestroy мы можем выгрузить(удалить) сцену из оперативки
 
    private void StartAutoUnloadScene()
    {
        if (_handleScene.IsDone == false) 
        {
            _handleScene.Completed += OnCheckIsDoneLoadScene;
        }
        else
        {
            CheckIsDoneLoadScene();
        }
    }

    private void OnCheckIsDoneLoadScene(AsyncOperationHandle<SceneInstance> obj)
    {
        if (_handleScene.IsDone == true) 
        {
            _handleScene.Completed -= OnCheckIsDoneLoadScene;
            CheckIsDoneLoadScene();
        }
    }

    private void CheckIsDoneLoadScene()
    {
        UpdatePercentage(100f);  
        UpdateStatus(TypeStatusTaskLoad.Comlite);
        
        if (_handleScene.Result.Scene.name != SceneManager.GetActiveScene().name)
        {
            SceneManager.sceneLoaded += OnCheckSceneLoaded;
        }
        else
        {
            MoveCurrentScriptInCurrentScene();
        }
    }

    private void OnCheckSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (_handleScene.Result.Scene.name == SceneManager.GetActiveScene().name)
        {
            SceneManager.sceneLoaded -= OnCheckSceneLoaded;
            MoveCurrentScriptInCurrentScene();
        }
    }

    private void MoveCurrentScriptInCurrentScene()
    {
        SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetActiveScene());
    }

    private void OnDestroy()
    {
        DestroyLogic();
        
        if (_handleScene.IsValid() == true) 
        {
            Addressables.UnloadSceneAsync(_handleScene);
        }
    }
}
