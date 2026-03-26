using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Логика UI панели у StorageTaskLoader
/// (если есть необх. то можно любую другую UI логику написать, но нужно будет сдел. абстр)
/// </summary>
public class StorageTaskLoaderUI : MonoBehaviour
{
    [SerializeField] 
    private CanvasGroup _generalTaskPanel;
    
    [SerializeField]
    private Image _progressBarLoadPercentageTask;
    [SerializeField]
    private Text _textLoadPercentageTask;

    [SerializeField] 
    private StorageTaskLoader _taskStorage;

    private bool _isOpen;
    public bool IsOpen => _isOpen;
    public event Action OnUpdateStatusOpen;
    
    private void Awake()
    {
        _taskStorage.OnUpdateGeneralLoadPercentage += OnGeneralUpdateLoadPercentage;
        _taskStorage.OnUpdateGeneralStatuse += OnGeneralUpdateStatus;
    }


    private void OnGeneralUpdateStatus(TypeStatusTaskLoad status)
    {
 
    }

    private void OnGeneralUpdateLoadPercentage(float loadPercentage)
    {
        _progressBarLoadPercentageTask.fillAmount = loadPercentage / 100f;
        _textLoadPercentageTask.text = loadPercentage + "%";
    }
    

    public void Open()
    {
        _generalTaskPanel.alpha = 1;
        _generalTaskPanel.blocksRaycasts = true;
        
        _isOpen = true;
        OnUpdateStatusOpen?.Invoke();
    }
    
    public void Close()
    {
        //Тут нельзя отключать, т.к дальше в иерархии идут еще обьекты, к которым нужен доступ
        _generalTaskPanel.alpha = 0f;
        _generalTaskPanel.blocksRaycasts = false;

        _isOpen = false;
        OnUpdateStatusOpen?.Invoke();
    }

    private void OnDestroy()
    {
        _taskStorage.OnUpdateGeneralLoadPercentage -= OnGeneralUpdateLoadPercentage;
        _taskStorage.OnUpdateGeneralStatuse -= OnGeneralUpdateStatus;
    }
}
