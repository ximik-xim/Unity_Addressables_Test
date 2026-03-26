using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Отвечает за установку текста над полем ввода
/// </summary>
public class CustomInputFieldSetPosText : MonoBehaviour
{
    [SerializeField]
    private LogicSetSelectObjectOnTopKeyboard _logicSetSelectObjectOnTopKeyboard;

    [SerializeField]
    private int _priority;
    
    [SerializeField] 
    private GetDataSODataDKODataKey _keyDkoText;

    [SerializeField]
    private GetDataSODataDKODataKey _keyDkoTargetGM;
    
    [SerializeField]
    private Canvas _canvas;

    [SerializeField]
    private GameObject _visibleGm;
    
    [SerializeField]
    private Text _text;

    [SerializeField]
    private float _offset;
    

    private void Awake()
    {
        _logicSetSelectObjectOnTopKeyboard.OnEndSetPosition.Subscribe(OnEndSetPosition, _priority);
        _logicSetSelectObjectOnTopKeyboard.OnStartReturnPosition.Subscribe(OnStartReturnPosition, _priority);
    }



    /// <summary>
    /// Когда уст. позиции у окна ввода закончилось, тогда приступю к логике расчетов позиции текста
    /// </summary>
    /// <param name="dko"></param>
    private void OnEndSetPosition(DKOKeyAndTargetAction dko)
    {
        if (dko != null)
        {
            if (dko.ActionIsAlready(_keyDkoText.GetData()) == true && dko.ActionIsAlready(_keyDkoTargetGM.GetData()) == true) 
            {
                var dkoText = (DKODataInfoT<AbsGetStringText>)dko.KeyRun(_keyDkoText.GetData());
                string setText = dkoText.Data.GetStringText();
                
                var dkoGm = (DKODataInfoT<AbsGetGm>)dko.KeyRun(_keyDkoTargetGM.GetData());
                GameObject targetGm = dkoGm.Data.GetGm();

                SetPosText(setText, targetGm);
                return;
            }
            
        }
           
        DisactiveText();
    }

    /// <summary>
    /// Когда возвращаю окно ввода в изначальное положение 
    /// </summary>
    /// <param name="obj"></param>
    private void OnStartReturnPosition(DKOKeyAndTargetAction obj)
    {
        DisactiveText();
    }

    
    private void SetPosText(string text, GameObject gm)
    {
        
        RectTransform _canvasTransform = _canvas.GetComponent<RectTransform>();
        Vector2 posUpAndDownCanvas = StaticCustomMethodRectTransform.GetLocalPositRectUpAndDown(_canvasTransform);
        
        RectTransform _gmTransform = gm.GetComponent<RectTransform>();
        Vector2 posUpAndDownGm = StaticCustomMethodRectTransform.GetLocalPositRectUpAndDown(_gmTransform);

        RectTransform _textTransform = _text.GetComponent<RectTransform>();
        
        if (posUpAndDownCanvas.x - posUpAndDownGm.x > _textTransform.rect.height + _offset)
        {
            ActiveText();
            float distanceDownText = _textTransform.rect.height * _textTransform.pivot.y;
            _textTransform.localPosition = new Vector3(_textTransform.localPosition.x, posUpAndDownGm.x + distanceDownText + _offset, _textTransform.localPosition.z);

            _text.text = text;
        }
        else
        {
            DisactiveText();
        }
    }

    private void ActiveText()
    {
        _visibleGm.SetActive(true);
    }

    private void DisactiveText()
    {
        _visibleGm.SetActive(false);
    }
  
}
