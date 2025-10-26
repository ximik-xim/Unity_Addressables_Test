using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
/// <summary>
/// Включает и выключает GM в зависимости от статуса мута
/// </summary>
public class CS_GMIsMuteSoundLevel : MonoBehaviour
{
    [SerializeField] 
    private GameObject _IsMute;

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

        _soundLevelData.OnUpdateStatusMute += OnUpdateStatusMute;
        OnUpdateStatusMute();
    }

    private void OnUpdateStatusMute()
    {
        if (_soundLevelData.IsMute == true)
        {
            _IsMute.SetActive(true);
        }
        else
        {
            _IsMute.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        _soundLevelData.OnUpdateStatusMute -= OnUpdateStatusMute;
    }
}
