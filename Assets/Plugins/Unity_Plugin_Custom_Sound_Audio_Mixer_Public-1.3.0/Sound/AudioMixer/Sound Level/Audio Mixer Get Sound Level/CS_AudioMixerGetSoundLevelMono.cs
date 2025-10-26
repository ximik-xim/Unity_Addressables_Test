    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Более продвинутый SAudioMixerGetAndSetVariable
/// Нужен для управления именно громкостью звука
///
/// Может
/// - Мутить звук если добавлина Task на мут(сохраняя перед этим уровень звука который был)
/// Если изменить уровень звука при муте, то устоновленный уровень звука сохраниться и будет применен когда будут удлаены все Task на мут
/// - Имеет абстрактную логику сохранения уровня звука(тупо сохраняет float переменную)
/// </summary>
public class CS_AudioMixerGetSoundLevelMono : MonoBehaviour
{
    [SerializeField]
    private List<AbsKeyData<GetDataSODataSoundLevelAudioMixer, CS_SoundLevelAudioMixerData>> _list = new List<AbsKeyData<GetDataSODataSoundLevelAudioMixer, CS_SoundLevelAudioMixerData>>(); 

    private Dictionary<string, CS_SoundLevelAudioMixerData> _dictionary = new Dictionary<string, CS_SoundLevelAudioMixerData>();
        
    private bool _init = false;
    public bool Init => _init;
    public event Action OnInit;
    
    /// <summary>
    /// Тут обязательно нужно именно метод Start, т.к SetFloat у Audio Mixer работает только после метода Start(если попытаться в Awake, то значение тупо не установиться)  
    /// </summary>
    ///
    
    private void Start()
    {
        foreach (var VARIABLE in _list)
        {
            _dictionary.Add(VARIABLE.Key.GetData().GetKey(),VARIABLE.Data);    
            VARIABLE.Data.Init();
        }

        StartActionInit();
    }
    
    public void StartActionInit()
    {
        List<CS_SoundLevelAudioMixerData> _buffer = new List<CS_SoundLevelAudioMixerData>();
        bool _isStart = false;
        
        StartLogic();

        void StartLogic()
        {
            //теперь буду просто блокировать проверку готовности задачи пока не отработает метод StartLogic у всех задач
            _isStart = true;

            foreach (var VARIABLE in _list)
            {
                //т.к CheckCompleted будет выполнен по любому после того как будет вызвать у всех элементов метод StartLogic,
                //то тут бага с тем что не успеют сбориться bool IsCompleted на момент проверки, просто такого бага не будет
                VARIABLE.Data.Init();
                if (VARIABLE.Data.IsInit == false)
                {
                    _buffer.Add(VARIABLE.Data);
                    VARIABLE.Data.OnInit += CheckCompleted;
                }
            }

            //теперь буду просто блокировать проверку готовности задачи пока не отработает метод StartLogic у всех задач
            _isStart = false;
            
            CheckCompleted();
        }

        void CheckCompleted()
        {
            if (_isStart == false)
            {
                int targetCount = _buffer.Count;
                for (int i = 0; i < targetCount; i++)
                {
                    if (_buffer[i].IsInit == true)
                    {
                        _buffer[i].OnInit -= CheckCompleted;
                        _buffer.RemoveAt(i);
                        i--;
                        targetCount--;
                    }
                }

                if (_buffer.Count == 0)
                {
                    Completed();
                }
            }
        }
    }

    private void Completed()
    {
        if (_init == false)
        {
            _init = true;
            OnInit?.Invoke();
        }
    }
    
    public CS_SoundLevelAudioMixerData GetSoundLevelData(CS_KeySoundLevelAudioMixer key)
    {
        return _dictionary[key.GetKey()];
    }

    public bool IsAddSoundLevelData(CS_KeySoundLevelAudioMixer key)
    {
        return _dictionary.ContainsKey(key.GetKey());
    }

    public void AddSoundLevelData(CS_KeySoundLevelAudioMixer key, CS_SoundLevelAudioMixerData data)
    {
        if (IsAddSoundLevelData(key) == false)
        {
            _dictionary.Add(key.GetKey(), data);
            data.Init();
        }
    }
    
    public void RemoveVariableLogic(CS_KeySoundLevelAudioMixer key)
    {
        if (IsAddSoundLevelData(key) == true)
        {
            _dictionary.Remove(key.GetKey());
        }
    }
}
