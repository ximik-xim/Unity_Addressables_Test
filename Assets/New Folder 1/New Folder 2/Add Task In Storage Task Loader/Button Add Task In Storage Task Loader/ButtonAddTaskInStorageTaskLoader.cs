using UnityEngine;
using UnityEngine.UI;

public class ButtonAddTaskInStorageTaskLoader : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField]
    private AddTaskInStorageTaskLoader _logicAddTask;
    
    private void Awake()
    {
        if (_logicAddTask.IsInit == false)
        {
            _logicAddTask.OnInit += OnInitTask;
        }
        
        CheckInit();
    }
    
    private void OnInitTask()
    {
        if (_logicAddTask.IsInit == true)
        {
            _logicAddTask.OnInit -= OnInitTask;
            CheckInit();
        }
    }
   
    private void CheckInit()
    {
        if (_logicAddTask.IsInit == true)  
        {
            InitData();
        }
    }
    
    private void InitData()
    {
        _button.onClick.AddListener(ButtonClick);
    }
    
    private void ButtonClick()
    {
        _logicAddTask.StartLoadScene();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ButtonClick);
    }
}
