using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// абстракция для сохранения данных об уровне звука
/// (усложнено для возможности сохранения данных на сервере, а значит через callback)
/// </summary>
public abstract class CS_SoundLevelAbsPatchStorageValue : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;

    public abstract event Action OnUpdateData;
    
    /// <summary>
    /// 
    /// (пришлось обьеденить, т.к уст. данные в указ. путь нужно все сразу, а не раздельно)
    /// </summary>
    /// <param name="value"></param>
    public abstract void SetValueVolumeAndMute(S_SoundData soundData);
    public abstract S_SoundData GetValueVolumeAndMute();
    
    public abstract event Action OnSaveData;
    public abstract void SaveData(TaskInfo taskInfo, bool urgentSaving = false);
    public abstract StatusStorageAction LastStatusSaveData { get; }
}
