using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Будет брать с Task данные и упаковывать их в обертку для инкапсуляции данных
/// (инкапсулированные данные по идеи будут передаваться в UI панель для Task) 
/// </summary>
public class WrapperTaskLoaderDataMono : MonoBehaviour
{
    [SerializeField]
    private AbsTaskLoaderDataMono _data;
    private WrapperTaskLoaderData _wrapper;

    private void Awake()
    {
        if (_wrapper == null) 
        {
            _wrapper = new WrapperTaskLoaderData(_data.GetTaskInfo());
        }
    }

    public WrapperTaskLoaderData GetWrapperTask()
    {
        if (_wrapper == null) 
        {
            _wrapper = new WrapperTaskLoaderData(_data.GetTaskInfo());
        }

        return _wrapper;
    }
}