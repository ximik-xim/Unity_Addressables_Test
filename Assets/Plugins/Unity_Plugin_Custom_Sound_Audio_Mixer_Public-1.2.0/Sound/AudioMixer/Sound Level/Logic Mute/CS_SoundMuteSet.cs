using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Нужен для упращение замены вариации, того кто будет отвечать за мут(Toggle, Button или может скрипт)
/// Содержит в себе основные ключи
/// Устанавливает мут указаному контроллерам уровня звука 
/// </summary>
public class CS_SoundMuteSet : MonoBehaviour
{
    [SerializeField]
    private CS_AbsTriggerStatusMute _statusMute;

    [SerializeField] 
    private GetDKOPatch _pathSoundLevel;
    private CS_AudioMixerGetSoundLevelMono _audioMixerSoundLevel;

    [SerializeField] 
    private GetDataSODataSoundLevelAudioMixer _keySoundValue;
    
    [SerializeField] 
    private GetDataSODataTaskMuteSoundLevel _taskMute;
    private CS_TaskDataMuteSoundLevel _taskDataMute;
    [SerializeField] 
    private string _textMute;

    /// <summary>
    /// Убрать ли Task на блокировку при уничтожении этого обьекта
    /// (Если это сделать, то статус мута не сохраниться, если все Task будут удалены)
    /// </summary>
    [SerializeField] 
    private bool _removeKeyDestroy = false;
    
    private void Awake()
    {
        if (_pathSoundLevel.Init == false)
        {
            _pathSoundLevel.OnInit += OnInitPatchMixerSoundLevel;
            return;
        }

        InitPatchMixerSoundLevel();
        
    }

    private void OnInitPatchMixerSoundLevel()
    {
        _pathSoundLevel.OnInit -= OnInitPatchMixerSoundLevel;
        InitPatchMixerSoundLevel();
    }
    
    private void InitPatchMixerSoundLevel()
    {
        var dkoData = (DKODataInfoT<CS_AudioMixerGetSoundLevelMono>)_pathSoundLevel.GetDKO();
        _audioMixerSoundLevel = dkoData.Data;
        
        if (_audioMixerSoundLevel.Init == false)
        {
            _audioMixerSoundLevel.OnInit += OnInitMixer;
        }
        
        if (_statusMute.IsInit == false)
        {
            _statusMute.OnInit += OnInitStatusMute;
        }

        CheckInit();
    }

    private void OnInitMixer()
    {
        _audioMixerSoundLevel.OnInit -= OnInitMixer;
        CheckInit();
    }
    
    private void OnInitStatusMute()
    {
        _statusMute.OnInit -= OnInitStatusMute;
        CheckInit();
    }


    private void CheckInit()
    {
        if (_audioMixerSoundLevel.Init == true && _statusMute.IsInit == true) 
        {
            Init();
        }
    }
    
    private void Init()
    {
        _taskDataMute = new CS_TaskDataMuteSoundLevel(_textMute);
        
        _statusMute.SetStatusMuteNotUseEvent(_audioMixerSoundLevel.GetSoundLevelData(_keySoundValue.GetData()).IsMute);
        
        _statusMute.OnUpdateMuteData += OnUpdateStatusMute;
        OnUpdateStatusMute();
    }

    private void OnUpdateStatusMute()
    {
        if (_statusMute.GetStatusMute() == true)
        {
            SetKey();
        }
        
        if (_statusMute.GetStatusMute() == false)
        {
            RemoveKey();
        }
    }

    private void SetKey()
    {
        var soundLeve = _audioMixerSoundLevel.GetSoundLevelData(_keySoundValue.GetData());
        if (soundLeve.IsKeyMute(_taskMute.GetData()) == false)
        {
            soundLeve.SetKeyMute(_taskMute.GetData(), _taskDataMute);
        }
    }

    private void RemoveKey()
    {
        var soundLeve = _audioMixerSoundLevel.GetSoundLevelData(_keySoundValue.GetData());
        if (soundLeve.IsKeyMute(_taskMute.GetData()) == true)
        {
            soundLeve.RemoveKeyMute(_taskMute.GetData());
        }
    }

    private void OnDestroy()
    {
        _statusMute.OnUpdateMuteData -= OnUpdateStatusMute;

        if (_removeKeyDestroy == true)
        {
            RemoveKey();    
        }
        
    }
}
