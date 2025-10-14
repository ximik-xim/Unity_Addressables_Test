
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskPanelUIDefault : AbsTaskPanelUI
{
    [SerializeField] 
    private DKOKeyAndTargetAction _objectDKO;
    
    [SerializeField] 
    private GameObject _parent;
    
    [SerializeField]
    private RawImage _imageTask;
    
    [SerializeField]
    private Image _progressBarLoadPercentageTask;
    [SerializeField]
    private Text _textLoadPercentageTask;
    
    [SerializeField]
    private Text _headingTask;

    private WrapperTaskLoaderData _taskWrapper;

    [SerializeField] 
    private Texture2D ImageTask;

    [SerializeField]
    private Color _colorImageTask;
    
    public override void SetData(WrapperTaskLoaderData data)
    {
        if (_taskWrapper != null)
        {
            _taskWrapper.OnUpdatePercentComplited -= OnUpdatePercentage;
        }

        _taskWrapper = data;
        
        UpdateUI();
        data.OnUpdatePercentComplited += OnUpdatePercentage;
    }

    public override DKOKeyAndTargetAction GetDKO()
    {
        return _objectDKO;
    }

    private void OnUpdatePercentage(float percentComplited)
    {
        _progressBarLoadPercentageTask.fillAmount = (_taskWrapper.PercentComplited / 100f);
        _textLoadPercentageTask.text = percentComplited + "%";
    }

    private void UpdateUI()
    {
        _imageTask.texture = ImageTask;
        _imageTask.color = _colorImageTask;
        
        OnUpdatePercentage(_taskWrapper.PercentComplited);

        _headingTask.text = _taskWrapper.NameTask;
    }

    public override void OpenPanel()
    {
        _parent.SetActive(true);
    }

    public override void ClosePanel()
    {
        _parent.SetActive(false);
    }
    
    private void OnDestroy()
    {
        if (_taskWrapper != null)
        {
            _taskWrapper.OnUpdatePercentComplited -= OnUpdatePercentage;
        }
    }
}
