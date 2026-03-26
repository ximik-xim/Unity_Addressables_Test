using System;
using UnityEngine;

/// <summary>
/// данные самой подсказки
/// </summary>
[Serializable]
public class VariantHints {
    /// <summary>
    /// Текст который будет отображаться(будут выделены буквы слова и т.д)
    /// </summary>
    public string TextVisible;
    /// <summary>
    /// Текст который будет возращен 
    /// </summary>
    public string TextReturn;
    /// <summary>
    /// Коэффициент, отражающий насколько походящее подсказка 
    /// </summary>
    public float OverlapValue;

    public VariantHints(string textVisible,string textReturn, float overlapValue) {
        TextVisible = textVisible;
        TextReturn = textReturn;
        OverlapValue = overlapValue;
    }
}