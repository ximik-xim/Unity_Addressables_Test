using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Просто панель с ифнормацией(заложенной через UI)
/// (В теории можно унаследоваться или просто использовать как отдельную базовую функцию любой панели)
/// </summary>
public class UIGamePanelInfo : MonoBehaviour
{
    /// <summary>
    /// Нужен что бы менять родителей у панели
    /// И удалять потом панель при переходе на другую сцену
    /// </summary>
    [SerializeField] 
    private GameObject _parent;
    
    /// <summary>
    /// Панель которую будем включать и отключать(может не совподать с parent)
    /// </summary>
    [SerializeField] 
    private GameObject _panelInfo;

    /// <summary>
    /// Статус панели(открыта или закрыта)
    /// </summary>
    [SerializeField]
    private bool _isOpen = false;
    public bool IsOpen => _isOpen;

    /// <summary>
    /// Сработает при обновлении состояния(открыта или закрыта панель)
    /// </summary>
    public event Action OnUpdateStatus;
 
    /// <summary>
    /// Путь до хранилеща с Task на блокировку для открытия или закрытия панели
    /// </summary>
    [SerializeField]
    private GetDKOPatch _patchTaskData;
    private TSG_StorageKeyTaskDataMono _taskData;

    /// <summary>
    /// Ключ для списка Task на блокировка, на открытие панели
    /// </summary>
    [SerializeField] 
    private GetDataSO_TSG_KeyStorageTask _keyTaskDataOpen;
    /// <summary>
    /// Ключ для списка Task на блокировка, на открытие панели
    /// </summary>
    [SerializeField] 
    private GetDataSO_TSG_KeyStorageTask _keyTaskDataClose;

    
    private void Awake()
    {
        if (_patchTaskData.Init == false)
        {
            _patchTaskData.OnInit += OnInit;
            return;
        }

        GetDataDKO();
    }

    private void OnInit()
    {
        _patchTaskData.OnInit -= OnInit;
        GetDataDKO();
    }

    private void GetDataDKO()
    {
        var DKOData = (DKODataInfoT<TSG_StorageKeyTaskDataMono>)_patchTaskData.GetDKO();
        _taskData = DKOData.Data;

        if (_taskData.IsKey(_keyTaskDataOpen.GetData()) == false) 
        {
            _taskData.AddTaskData(_keyTaskDataOpen.GetData(), new TSG_StorageTaskDefaultData());    
        }

        if (_taskData.IsKey(_keyTaskDataClose.GetData()) == false)
        {
            _taskData.AddTaskData(_keyTaskDataClose.GetData(), new TSG_StorageTaskDefaultData());
        }
    }

    /// <summary>
    /// Откроет(включит) панель, если нету Task на блокировку(на открытие)
    /// </summary>
    public void Open()
    {
        if (_taskData.GetTaskData(_keyTaskDataOpen.GetData()).IsThereTasks() == false) 
        {
            OpenPanel();    
        }
    }

    /// <summary>
    /// Откроет(включит) панель, игнорируя Task на блокировку(на открытие)
    /// </summary>
    public void OpenIgnoreTask()
    {
        OpenPanel();
    }

    private void OpenPanel()
    {
        _isOpen = true;
        _panelInfo.SetActive(true);
        OnUpdateStatus?.Invoke(); 
    }
    
    /// <summary>
    /// Закроет(выключит) панель, если нету Task на блокировку(на закрытие)
    /// </summary>
    public void Close()
    {
        if (_taskData.GetTaskData(_keyTaskDataClose.GetData()).IsThereTasks() == false) 
        {
            ClosePanel();    
        }
    }
    
    /// <summary>
    /// Закроет(выключит) панель, игнорируя Task на блокировку(на закрытие)
    /// </summary>
    public void CloseIgnoreTask()
    {
        ClosePanel();
    }
    
    private void ClosePanel()
    {
        _isOpen = false;
        _panelInfo.SetActive(false);
        OnUpdateStatus?.Invoke();
    }

    /// <summary>
    /// Вернеть установленного родителя
    /// </summary>
    /// <returns></returns>
    public GameObject GetParent()
    {
        return _parent;
    }

    /// <summary>
    /// Вернет хранилеще с Task на блокировку открытие
    /// </summary>
    /// <returns></returns>
    public TSG_StorageTaskDefaultData GetTaskDataOpen()
    {
        return _taskData.GetTaskData(_keyTaskDataOpen.GetData());
    }
    
    /// <summary>
    /// Вернет хранилеще с Task на блокировку закрытия
    /// </summary>
    /// <returns></returns>
    public TSG_StorageTaskDefaultData GetTaskDataClose()
    {
        return _taskData.GetTaskData(_keyTaskDataClose.GetData());
    }

    private void OnDestroy()
    {
        _taskData.RemoveTaskData(_keyTaskDataOpen.GetData());
        _taskData.RemoveTaskData(_keyTaskDataClose.GetData());
    }
}
