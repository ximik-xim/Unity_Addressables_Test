using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Отвечает за создание указанного префаба TaskPanelUI
/// </summary>
public class SpawnerTaskPanelUI : MonoBehaviour
{
    [SerializeField] 
    private AbsTaskPanelUI _prefabUI;

    [SerializeField] 
    private WrapperTaskLoaderDataMono _warpperTask;

    private AbsTaskPanelUI _taskUI;
    
    public event Action OnInit;
    private bool _isInit = false;
    public bool IsInit => _isInit;
    
    public void SetParent(Transform parent)
    {
        _taskUI = Instantiate(_prefabUI, parent);
        _taskUI.SetData(_warpperTask.GetWrapperTask());

        _isInit = true;
        OnInit?.Invoke();
    }

    public AbsTaskPanelUI GetTaskUI()
    {
        return _taskUI;
    }
}
