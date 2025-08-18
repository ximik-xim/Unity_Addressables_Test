using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// Устанавливает уровень звука через слайдер
/// </summary>
public class CS_SliderSoundLevel : MonoBehaviour
{
    /// <summary>
    /// У слайдера значение должны быть от -80 до 0
    /// </summary>
    [SerializeField] 
    private Slider _soundLevelSlider;

    [SerializeField] 
    private GetDataSODataSoundLevelAudioMixer _key;

    [SerializeField] 
    private GetDKOPatch _patchAudioMixerGetSoundLevelMono;
    private CS_AudioMixerGetSoundLevelMono _mixerGetSoundLevelData;

    private CS_SoundLevelAudioMixerData _soundLevelData;
    
    private void Awake()
    {
        if (_patchAudioMixerGetSoundLevelMono.Init == false) 
        {
            _patchAudioMixerGetSoundLevelMono.OnInit += OnInitPatch;
            return;
        }

        InitPatch();

    }

    private void OnInitPatch()
    {
        _patchAudioMixerGetSoundLevelMono.OnInit -= OnInitPatch;
        InitPatch();
    }

    private void InitPatch()
    {
        var DKOData = (DKODataInfoT<CS_AudioMixerGetSoundLevelMono>)_patchAudioMixerGetSoundLevelMono.GetDKO();
        _mixerGetSoundLevelData = DKOData.Data;
        
        if (_mixerGetSoundLevelData.Init == false) 
        {
            _mixerGetSoundLevelData.OnInit += OnInit;
            return;
        }

        Init();
    }


  private void OnInit()
  {
      _mixerGetSoundLevelData.OnInit -= OnInit;
      Init();
  }
  private void Init()
  {
      _soundLevelData = _mixerGetSoundLevelData.GetSoundLevelData(_key.GetData());
      _soundLevelSlider.onValueChanged.AddListener(UpdateSliderValue);

      _soundLevelData.OnUpdateValue += OnUpdateValue;

      OnUpdateValue();
  }

  private void OnUpdateValue()
  {
      _soundLevelSlider.SetValueWithoutNotify(_soundLevelData.GetValue());
  }

  private void OnEnable()
  {
      if (_soundLevelData != null)
      {
          if (_soundLevelData.IsInit == true) 
          {
              _soundLevelSlider.SetValueWithoutNotify(_soundLevelData.GetValue());
          }
      }
  }

  private void UpdateSliderValue(float soundValue)
  {
      if (_soundLevelData.GetValue() != soundValue)  
      {
          _soundLevelData.SetValue(soundValue);    
      }
  }

  private void OnDestroy()
  {
      _soundLevelData.OnUpdateValue -= OnUpdateValue;
      _soundLevelSlider.onValueChanged.RemoveListener(UpdateSliderValue);
  }
}
