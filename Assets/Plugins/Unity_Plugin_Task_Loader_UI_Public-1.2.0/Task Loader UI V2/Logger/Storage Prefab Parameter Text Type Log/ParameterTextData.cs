using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
