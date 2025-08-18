using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Обертка над SAudioMixerGetAndSetVariableData предназначеная для работы с уровнем громкости
///
/// Может
/// - Мутить звук если добавлина Task на мут(сохраняя перед этим уровень звука который был)
/// Если изменить уровень звука при муте, то устоновленный уровень звука сохраниться и будет применен когда будут удлаены все Task на мут
/// - Имеет абстрактную логику сохранения уровня звука(тупо сохраняет float переменную)
/// </summary>
[System.Serializable]
public class CS_SoundLevelAudioMixerData
{
    [SerializeField] 
    private CS_AudioMixerGetAndSetVariableData _variableData;
    
    //Тут абстракция, куда буду сохранять данные об уровне звука(может через хранилеще, а может в тупую через привсы пока сделаю)
    [SerializeField] 
    private CS_SoundLevelAbsPatchStorageValue _patchStorageValue;
        
    public event Action OnUpdateStatusMute;
    private bool _isMute = false;
    public bool IsMute => _isMute;
   
    public event Action OnUpdateCountTaskMute;
    /// <summary>
    /// Решил, что тут оставлю внутри список Task, а не буду его брать из плагина List Task
    /// </summary>
    private Dictionary<string, CS_TaskDataMuteSoundLevel> _taskDataMute = new Dictionary<string, CS_TaskDataMuteSoundLevel>();
    
    private float _muteValue = -80f;

    public event Action OnUpdateValue;

    public event Action OnInit;
    private bool _init = false;
    public bool IsInit => _init;

    /// <summary>
    /// Сохранять ли статус мута(иногда нужно не у элемента его сохранять, а у конкретного обьекта, пример Toggle, что бы потом уст. значение IsMute целой группе элементов)
    /// (легко может вызвать проблемы если будет несколько Task на блокировку, это нужно учесть)
    /// </summary>
    [SerializeField] 
    private bool _isSaveStatusMute = false;
    
    public void Init()
    {
        if (_init == false)
        {
            if (_patchStorageValue.IsInit == false) 
            {
                _patchStorageValue.OnInit -= OnInitPatch;
                _patchStorageValue.OnInit += OnInitPatch;
                return;
            }

            InitPatch();
        }
    }

    private void OnInitPatch()
    {
        _patchStorageValue.OnInit -= OnInitPatch;
        InitPatch();
    }
    
    private void InitPatch()
    {
        _patchStorageValue.OnUpdateData += UpdateDataStorage;
        UpdateDataStorage();
        
        _init = true;
        OnInit?.Invoke();
    }

    
    private void UpdateDataStorage()
    {
        if (_isSaveStatusMute == true)
        {
            _isMute = _patchStorageValue.GetValueVolumeAndMute().IsMute;

            if (_isMute == true)
            {
                _variableData.SetValue(_muteValue);
                OnUpdateValue?.Invoke();
                
                OnUpdateStatusMute?.Invoke();
                return;
            }
        }

        if (_isMute == false)
        {
            if (_variableData.GetValue() != _patchStorageValue.GetValueVolumeAndMute().Volume) 
            {
                _variableData.SetValue(_patchStorageValue.GetValueVolumeAndMute().Volume);
                OnUpdateValue?.Invoke();
            }
        }
    }
    
    public void SetKeyMute(CS_KeyTaskDataMuteSoundLevel key,CS_TaskDataMuteSoundLevel task)
    {
        _taskDataMute.Add(key.GetKey(),task);
        OnUpdateCountTaskMute?.Invoke();
        
        if (_isMute == false)
        {
            _isMute = true;
            SetMute();
            OnUpdateStatusMute?.Invoke();
        }
    }
    public void RemoveKeyMute(CS_KeyTaskDataMuteSoundLevel key)
    {
        _taskDataMute.Remove(key.GetKey());
        OnUpdateCountTaskMute?.Invoke();
        
        if (_taskDataMute.Count == 0) 
        {
            if (_isMute == true)
            {
                _isMute = false;
                RemoveMute();
                OnUpdateStatusMute?.Invoke();
            }
        }
    }
    
    public bool IsKeyMute(CS_KeyTaskDataMuteSoundLevel key)
    {
        return _taskDataMute.ContainsKey(key.GetKey());
    }
    
    private void SetMute()
    {
        float soundLevel = _variableData.GetValue();
        
        //чтобы не создавать понапрасну новый экземпляр, тупо берем старый и перезаписываем его
        var data = _patchStorageValue.GetValueVolumeAndMute();
        data.Volume = soundLevel;

        if (_isSaveStatusMute == true)
        {
            data.IsMute = true;    
        }
        
        _patchStorageValue.SetValueVolumeAndMute(data);
        _patchStorageValue.SaveData(new TaskInfo("tt"));
        
        _variableData.SetValue(_muteValue);
        OnUpdateValue?.Invoke();
    }

    private void RemoveMute()
    {
        //чтобы не создавать понапрасну новый экземпляр, тупо берем старый и перезаписываем его
        var data = _patchStorageValue.GetValueVolumeAndMute();
        _variableData.SetValue(data.Volume);

        if (_isSaveStatusMute == true)
        {
            data.IsMute = false;

            _patchStorageValue.SetValueVolumeAndMute(data);
            _patchStorageValue.SaveData(new TaskInfo("tt"));
        }

        OnUpdateValue?.Invoke();
    }


    public void SetValue(float soundLevel)
    {
        if (_isMute == false)
        {
            _variableData.SetValue(soundLevel);
            OnUpdateValue?.Invoke();
        }
        
        //чтобы не создавать понапрасну новый экземпляр, тупо берем старый и перезаписываем его
        var data = _patchStorageValue.GetValueVolumeAndMute();
        data.Volume = soundLevel;
        
        _patchStorageValue.SetValueVolumeAndMute(data);
        _patchStorageValue.SaveData(new TaskInfo("tt"));
    }

    public float GetValue()
    {
        if (_isMute == true) 
        {
            return _patchStorageValue.GetValueVolumeAndMute().Volume;
        }
        
        return _variableData.GetValue();
    }
    
    
    public void Destory()
    {
        _patchStorageValue.OnUpdateData -= UpdateDataStorage;
    }
}
