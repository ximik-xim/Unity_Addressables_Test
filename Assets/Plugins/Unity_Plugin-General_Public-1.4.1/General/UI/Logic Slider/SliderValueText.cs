using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueText : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private Text _textValue;
    
    private void Awake()
    {
        _slider.onValueChanged.AddListener(UpdateValueSlide);
        UpdateValueSlide(_slider.value);
    }

    private void UpdateValueSlide(float arg0)
    {
        _textValue.text = _slider.value.ToString();
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(UpdateValueSlide);
    }
}
