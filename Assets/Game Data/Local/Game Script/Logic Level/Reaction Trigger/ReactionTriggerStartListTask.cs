using UnityEngine;

public class ReactionTriggerStartListTask : MonoBehaviour
{
    [SerializeField]
    private AbsTriggerGG _trigger;

    [SerializeField]
    private LogicListTaskDKO _listTask;

    [SerializeField]
    private DKOKeyAndTargetAction _dko;
        
    private void Awake()
    {
        if (_listTask.IsInit == false)
        {
            _listTask.OnInit += OnInitListTask;
        }

        if (_trigger.IsInit == false)
        {
            _trigger.OnInit += OnInitTrigger;
        }
        
        CheckInit();
    }


    private void OnInitListTask()
    {
        if (_listTask.IsInit == true) 
        {
            _listTask.OnInit -= OnInitListTask;
            CheckInit();
        }
       
    }
    
    private void OnInitTrigger()
    {
        if (_trigger.IsInit == true)
        {
            _trigger.OnInit -= OnInitTrigger;
            CheckInit();
        }
    }


    private void CheckInit()
    {
        if (_listTask.IsInit == true && _trigger.IsInit == true)
        {
            Init();
        }
    }

    private void Init()
    {
        _trigger.OnTriggerGG += OnTriggerGG;
    }

    private void OnTriggerGG()
    {
        _listTask.StartAction(_dko);
    }
}
