using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Пример хранилище на превсах, для сохранения уровня звука
/// </summary>
public class CS_SoundLevelPatchStoragePrefs : CS_SoundLevelAbsPatchStorageValue
{
    [SerializeField] 
    private string _prefsPath;

    public override event Action OnInit;
    public override bool IsInit => _isInit;
    private bool _isInit = false;
    
    public override event Action OnUpdateData;

    private S_SoundData _soundData = null;
    
    private void Start()
    {
        GetSoundDataJS();

        _isInit = true;
        OnInit?.Invoke();
        OnUpdateData?.Invoke();
    }

    public override void SetValueVolumeAndMute(S_SoundData soundData)
    {
        _soundData = soundData;
        SetSoundDataJS();
        
        OnUpdateData?.Invoke();
    }

    public override S_SoundData GetValueVolumeAndMute()
    {
        GetSoundDataJS();
        return _soundData;
    }
    
    public override event Action OnSaveData;
    public override void SaveData(TaskInfo taskInfo, bool urgentSaving = false)
    {
        PlayerPrefs.Save();
        OnSaveData?.Invoke();
    }

    public override StatusStorageAction LastStatusSaveData { get => StatusStorageAction.Ok; }

    private void GetSoundDataJS()
    {
        if (_soundData == null)
        {
            _soundData = JsonUtility.FromJson<S_SoundData>(PlayerPrefs.GetString(_prefsPath));
            if (_soundData == null)
            {
                _soundData = new S_SoundData();
            }
        }
    }

    private void SetSoundDataJS()
    {
        PlayerPrefs.SetString(_prefsPath, JsonUtility.ToJson(_soundData));
    }
}

[System.Serializable]
public class S_SoundData
{
    public float Volume;
    public bool IsMute;
}