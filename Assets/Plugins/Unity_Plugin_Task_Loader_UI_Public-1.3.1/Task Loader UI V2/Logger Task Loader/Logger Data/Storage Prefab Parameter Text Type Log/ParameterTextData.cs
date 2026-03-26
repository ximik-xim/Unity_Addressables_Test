using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Параметры текста для опр. типа лога
/// (пока только цвет)
/// </summary>
[System.Serializable]
public class ParameterTextData
{
    [SerializeField] 
    private Color _color;

    public Color GetColorText()
    {
        return _color;
    }
}
