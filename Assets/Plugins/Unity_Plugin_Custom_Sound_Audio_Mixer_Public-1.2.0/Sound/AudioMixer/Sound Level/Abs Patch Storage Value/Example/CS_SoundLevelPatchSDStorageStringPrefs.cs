using System;
using UnityEngine;

/// <summary>
/// Пример хранилище на превсах, для сохранения уровня звука(через хранилеще float)
/// </summary>
public class CS_SoundLevelPatchSDStorageStringPrefs : CS_SoundLevelAbsPatchStorageValue
{
    [SerializeField] 
    private SD_StorageDataStringPrefs _storageSaveData;
    [SerializeField] 
    private GetDataSO_SD_KeyStorageStringVariable _key;
    
    public override bool IsInit => _storageSaveData.IsInit;

    public override event Action OnInit
    {
        add
        {
            _storageSaveData.OnInit += value;
        }

        remove
        {
            _storageSaveData.OnInit -= value;
        }
    }

    public override event Action OnUpdateData;


    private S_SoundData _soundData = null;
    
    private void Awake()
    {
        _storageSaveData.OnUpdateValue += OnUpdateValueStorage;
        _storageSaveData.OnUpdateData += OnUpdateDataStorage;
    }

    private void OnUpdateDataStorage()
    {
        _soundData = null;
        GetSoundDataJS();
        OnUpdateData?.Invoke();
    }

    private void OnUpdateValueStorage(SD_KeyStorageStringVariable obj)
    {
        if (obj == _key.GetData()) 
        {
            _soundData = null;
            GetSoundDataJS();
            OnUpdateData?.Invoke();
        }
    }
    
    public override void SetValueVolumeAndMute(S_SoundData soundData)
    {
        _soundData = soundData;
        SetSoundDataJS();
    }

    public override S_SoundData GetValueVolumeAndMute()
    {
        GetSoundDataJS();
        return _soundData;
    }

    public override event Action OnSaveData
    {
        add
        {
            _storageSaveData.OnSaveDataComplited += value;
        }

        remove
        {
            _storageSaveData.OnSaveDataComplited -= value;
        }
    }
    public override void SaveData(TaskInfo taskInfo, bool urgentSaving = false)
    {
        _storageSaveData.SaveData(taskInfo, urgentSaving);
    }

    public override StatusStorageAction LastStatusSaveData => _storageSaveData.LastStatusSaveData;

    private void GetSoundDataJS()
    {
        if (_soundData == null)
        {
            _soundData = JsonUtility.FromJson<S_SoundData>(_storageSaveData.GetData(_key.GetData()));
            if (_soundData == null)
            {
                _soundData = new S_SoundData();
            }
        }
    }

    private void SetSoundDataJS()
    {
        _storageSaveData.SetData(_key.GetData(), JsonUtility.ToJson(_soundData));
    }
    
    private void OnDestroy()
    {
        _storageSaveData.OnUpdateValue -= OnUpdateValueStorage;
        _storageSaveData.OnUpdateData -= OnUpdateDataStorage;
    }
}
