using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class CS_TransitionToMultipleSnapshotsAudioMixerData 
{
    [SerializeField] 
    private AudioMixer _mixer;

    /// <summary>
    /// Время перехода к указанным снапшотам
    /// </summary>
    [Range(0f,60f)]
    [SerializeField]
    private float _timeTransition;

    [SerializeField]
    private List<CS_SnapshotWeihgtAudioMixerData> _snapshotData;


    /// <summary>
    /// Нужно вызвать при запуске, что бы очистить список от пустых элементов
    /// </summary>
    public void LocalAwake()
    {
        int count = _snapshotData.Count;
        for (int i = 0; i < count; i++)
        {
            if (_snapshotData[i].GetSnapshot() == null)
            {
                _snapshotData.RemoveAt(i);
                i--;
                count--;
            }
        }
    }
    
    /// <summary>
    /// Запустит переход к указанным параметрам Snapshot(снапшотам), с учетом их веса (через интерполяцию всех значений снепшотов, с учетом их веса)
    /// </summary>
    public void StartTransitionSnapshot()
    {
        AudioMixerSnapshot[] snapshots = new AudioMixerSnapshot[_snapshotData.Count];
        float[] weightSnapshots = new float[_snapshotData.Count];
        
        for (int i = 0; i < _snapshotData.Count; i++)
        {
            snapshots[i] = _snapshotData[i].GetSnapshot();
            weightSnapshots[i] = _snapshotData[i].GetWeightSnapshot();
        }

        _mixer.TransitionToSnapshots(snapshots, weightSnapshots, _timeTransition);
    }
}
