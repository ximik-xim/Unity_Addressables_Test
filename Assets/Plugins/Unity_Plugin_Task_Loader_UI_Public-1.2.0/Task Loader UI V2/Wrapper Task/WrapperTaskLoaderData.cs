using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Обертка, нужная только что бы получать данные об
/// - статусе
/// - проценте выполнения
/// - имяни таски
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
