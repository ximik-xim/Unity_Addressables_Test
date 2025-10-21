using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Обертка, нужная только что бы инкапсулировать все данные кроме
/// - статусе
/// - проценте выполнения
/// - имени Task
///
/// По идеи эти данные пойдут в UI панель у Task
/// </summary>
public class WrapperTaskLoaderData
{
    public WrapperTaskLoaderData(TaskLoaderData data)
    {
        _data = data;
    }
    
    private TaskLoaderData _data;
 
    public event Action<TypeStatusTaskLoad> OnUpdateStatus
    {
        add
        {
            _data.OnUpdateStatus += value;
        }
        remove
        {
            _data.OnUpdateStatus -= value;
        }
    }
    
    public event Action<float> OnUpdatePercentComplited
    {
        add
        {
            _data.OnUpdatePercentComplited += value;
        }
        remove
        {
            _data.OnUpdatePercentComplited -= value;
        }
    }
    
    public TypeStatusTaskLoad StatusLoad => _data.StatusLoad;
    public float PercentComplited => _data.PercentComplited;
    public string NameTask => _data.NameTask;
}
