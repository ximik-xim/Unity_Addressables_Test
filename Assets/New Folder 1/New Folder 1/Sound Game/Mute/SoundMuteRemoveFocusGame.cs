using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMuteRemoveFocusGame : MonoBehaviour
{
    [SerializeField] 
    private CS_AudioMixerGetSoundLevelMono _audioMixerSoundLeve;

    [SerializeField] 
    private List<GetDataSODataSoundLevelAudioMixer> _keySoundValue;
    
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
        if (_audioMixerSoundLeve.Init == false)
        {
            _audioMixerSoundLeve.OnInit += OnInit;
            return;
        }

        Init();
    }

    private void OnInit()
    {
        _audioMixerSoundLeve.OnInit -= OnInit;
        Init();
    }

    private void Init()
    {
        _taskDataMute = new CS_TaskDataMuteSoundLevel(_textMute);
    }

    /// <summary>
    /// Срабатывает когда будет выбрана другая вкладка в браузере или кагда браузер будет свернут, крч когда пропадет фокус с приложения
    /// </summary>    
   private void OnApplicationFocus(bool hasFocus)
   {
       if (_audioMixerSoundLeve.Init == true)
       {
           if (hasFocus == false)
           {
               SetKey();
           }
           else
           {
               RemoveKey();
           }
       }
   }

    
 

    private void SetKey()
    {
        foreach (var VARIABLE in _keySoundValue)
        {
            var soundLeve = _audioMixerSoundLeve.GetSoundLevelData(VARIABLE.GetData());
            if (soundLeve.IsKeyMute(_taskMute.GetData()) == false)
            {
                soundLeve.SetKeyMute(_taskMute.GetData(), _taskDataMute);
            }
        }
    }

    private void RemoveKey()
    {
        foreach (var VARIABLE in _keySoundValue)
        {
            var soundLeve = _audioMixerSoundLeve.GetSoundLevelData(VARIABLE.GetData());
            if (soundLeve.IsKeyMute(_taskMute.GetData()) == true)
            {
                soundLeve.RemoveKeyMute(_taskMute.GetData());
            }
        }
    }

    private void OnDestroy()
    {
        if (_removeKeyDestroy == true)
        {
            RemoveKey();    
        }
    }
}
