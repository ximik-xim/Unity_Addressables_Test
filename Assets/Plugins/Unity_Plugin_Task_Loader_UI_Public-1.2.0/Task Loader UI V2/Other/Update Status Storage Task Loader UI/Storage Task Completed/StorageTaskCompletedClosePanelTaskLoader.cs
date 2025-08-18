using UnityEngine;

public class StorageTaskCompletedClosePanelTaskLoader : MonoBehaviour
{
   
    [SerializeField] 
    private SceneStartTask _sceneStartTask;

    [SerializeField] 
    private StorageTaskLoaderUI _storageTaskLoaderUI;


    private void Awake()
    {
        if (_sceneStartTask.ListTask == null)
        {
            _sceneStartTask.OnSetListTask += OnInit;
            return;
        }

        Init();
    }

    private void OnInit()
    {
        _sceneStartTask.OnSetListTask -= OnInit;
        Init();
    }

    private void Init()
    {
        _sceneStartTask.ListTask.OnCompleted += OnCompletedTask;
    }

    private void OnCompletedTask()
    {
        _sceneStartTask.ListTask.OnCompleted -= OnCompletedTask;
        _storageTaskLoaderUI.Close();
    }
}
