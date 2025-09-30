using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsTaskLoaderDataMono : MonoBehaviour
{
    public event Action OnInit;
    public bool IsInit => _isInit;
    protected bool _isInit = false;
    
    protected TaskLoaderData _taskData;
    
    private event Action<TypeStatusTaskLoad> OnUpdateStatus;
    private event Action<float> OnUpdatePercentage;

    [SerializeField] 
    private string _nameTask = "Обычная задача";
    
    [SerializeField] 
    protected DKOKeyAndTargetAction _objcetDKO;

    protected Action<TypeStatusTaskLoad> _onUpdateStatus;
    protected Action<float> _onUpdatePercentage;
    
    protected void InitTask()
    {
        if (_isInit == false)
        {
            _taskData = new TaskLoaderData(_nameTask, StartLogic, BreakTask, ResetStatusTask, out Action<TypeStatusTaskLoad> onUpdateStatus, out Action<float> onUpdatePercentage, GetDKO);

            _onUpdateStatus = onUpdateStatus;
            _onUpdatePercentage = onUpdatePercentage;

            OnUpdateStatus += onUpdateStatus;
            OnUpdatePercentage += onUpdatePercentage;
        }
    }

    protected virtual void Init()
    {
        _isInit = true;
        OnInit?.Invoke();
    }

    public virtual TaskLoaderData GetTaskInfo()
    {
        if (_taskData == null) 
        {
            InitTask();
        }
        
        if (_isInit == false)
        {
            Init();
        }
        
        return _taskData;
    }

    protected abstract void StartLogic();

    protected abstract void BreakTask();

    protected abstract void ResetStatusTask();

    protected void UpdateStatus(TypeStatusTaskLoad status)
    {
        OnUpdateStatus?.Invoke(status);
    }
    
    protected void UpdatePercentage(float value)
    {
        OnUpdatePercentage?.Invoke(value);
    }

    private DKOKeyAndTargetAction GetDKO()
    {
        return _objcetDKO;
    }

    protected void DestroyLogic()
    {
        _taskData.DestroyObject();
        
        OnUpdateStatus -= _onUpdateStatus;
        OnUpdatePercentage -= _onUpdatePercentage;
    }
    
    protected void OnDestroy()
    {
        DestroyLogic();
    }
}
