using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageTaskLoaderUI : MonoBehaviour
{
    [SerializeField] 
    private GameObject _generalTaskPanel;
    
    [SerializeField]
    private Image _progressBarLoadPercentageTask;
    [SerializeField]
    private Text _textLoadPercentageTask;

    [SerializeField] 
    private StorageTaskLoader _taskStorage;

    private void Awake()
    {
        _taskStorage.OnUpdateGeneralLoadPercentage += OnGeneralUpdateLoadPercentage;
        _taskStorage.OnUpdateGeneralStatuse += OnGeneralUpdateStatus;
    }


    public void OnGeneralUpdateStatus(TypeStatusTaskLoad status)
    {
 
    }

    public void OnGeneralUpdateLoadPercentage(float loadPercentage)
    {
        _progressBarLoadPercentageTask.fillAmount = loadPercentage / 100f;
        _textLoadPercentageTask.text = loadPercentage + "%";
    }
    

    public void Open()
    {
        _generalTaskPanel.SetActive(true);
    }
    
    public void Close()
    {
        _generalTaskPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        _taskStorage.OnUpdateGeneralLoadPercentage -= OnGeneralUpdateLoadPercentage;
        _taskStorage.OnUpdateGeneralStatuse -= OnGeneralUpdateStatus;
    }
}
