using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskLoaderData
{
    public TaskLoaderData(string nameTask, Action startLogic, Action breakTask, Action resetStatusTask, out Action<TypeStatusTaskLoad> onUpdateStatus, out Action<float> onUpdatePercentage, Func<DKOKeyAndTargetAction> getDKO)
    {
        _nameTask = nameTask;
        
        OnStartLogic += startLogic;
        OnBreakTask += breakTask;
        OnResetStatusTask += resetStatusTask;
        
        onUpdateStatus = UpdateStatus;
        onUpdatePercentage = UpdatePercentage;
        
        _getDKO = getDKO;

        _startLogic = startLogic;
        _breakTask = breakTask;
        _resetStatusTask = resetStatusTask;
    }
    
    public event Action<TypeStatusTaskLoad> OnUpdateStatus;
    public event Action<float> OnUpdatePercentComplited;
    
    private event Action OnStartLogic;
    private event Action OnBreakTask;
    private event Action OnResetStatusTask;

    private Func<DKOKeyAndTargetAction> _getDKO;

    private string _nameTask;
    public string NameTask => _nameTask;
    
    private TypeStatusTaskLoad _statusLoad;
    public TypeStatusTaskLoad StatusLoad => _statusLoad;
    private float _percentComplited;
    public float PercentComplited => _percentComplited;

    //нужны только для отписки
    private Action _startLogic;
    private Action _breakTask;
    private Action _resetStatusTask;
    
    public virtual void StartLogic()
    {
        OnStartLogic?.Invoke();
    }

    public virtual void BreakTask()
    {
        OnBreakTask?.Invoke();
    }

    public virtual void ResetStatusTask()
    {
        OnResetStatusTask?.Invoke();
    }

    public DKOKeyAndTargetAction GetObjectDKO()
    {
        return _getDKO.Invoke();
    }
    
    private void UpdateStatus(TypeStatusTaskLoad status)
    {
        if (status == TypeStatusTaskLoad.Start)
        {
            _percentComplited = 0;
        }
        
        _statusLoad = status;
        OnUpdateStatus?.Invoke(_statusLoad);
    }
    

    private void UpdatePercentage(float value)
    {
        if (value > 100)
        {
            value = 100;
        }

        if (value < 0)
        {
            value = 0;
        }

        _percentComplited = value;
        OnUpdatePercentComplited?.Invoke(_percentComplited);
    }

    public void DestroyObject()
    {
        OnStartLogic -= _startLogic;
        OnBreakTask -= _breakTask;
        OnResetStatusTask -= _resetStatusTask;
    }
}
