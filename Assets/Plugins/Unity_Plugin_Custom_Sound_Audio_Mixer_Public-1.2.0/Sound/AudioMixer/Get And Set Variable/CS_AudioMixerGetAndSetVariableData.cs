using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
/// <summary>
/// Нужен для того что бы уст. значение переменной в audioMixer ( эта переменна должна быть настроена в самом audioMixer(назначена) и затем внесена в ручную в Tlist)
/// Установка значений, должна происходить после метода Start, иначе SetFloat не будет нормально работать
/// </summary>
public class CS_AudioMixerGetAndSetVariableData 
{
  [SerializeField] 
  private AudioMixer _audioMixer;

  [SerializeField]
  private GetDataSODataAudioMixerVariable _keyVariable;
  
  public void SetValue(float value)
  {
      _audioMixer.SetFloat(_keyVariable.GetData().GetKey(), value);
  }

  public float GetValue()
  {
    float getValue = 0f;
    _audioMixer.GetFloat(_keyVariable.GetData().GetKey(), out getValue);
     
     return getValue;
  }
}
