using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class CS_TransitionToOneSnapshotAudioMixerData 
{
    [SerializeField] 
    private AudioMixerSnapshot _snapshot;

    /// <summary>
    /// Время перехода к указанному снапшоту
    /// </summary>
    [Range(0f,60f)]
    [SerializeField]
    private float _timeTransition;

    /// <summary>
    /// Запустит переход на указанный Snapshot
    /// </summary>
    public void StartTransitionSnapshot()
    {
        _snapshot.TransitionTo(_timeTransition);
    }
}
