using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class CS_SnapshotWeihgtAudioMixerData
{
    [SerializeField] 
    private AudioMixerSnapshot _snapshot;

    /// <summary>
    /// Вес снапшота
    /// </summary>
    [Range(0f,1f)]
    [SerializeField]
    private float _timeWeight;

    public float GetWeightSnapshot()
    {
        return _timeWeight;
    }

    public AudioMixerSnapshot GetSnapshot()
    {
        return _snapshot; 
    }
}
