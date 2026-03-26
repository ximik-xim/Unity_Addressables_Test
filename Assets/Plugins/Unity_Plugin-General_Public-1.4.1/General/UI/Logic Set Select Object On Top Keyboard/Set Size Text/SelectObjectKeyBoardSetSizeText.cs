using UnityEngine;
using UnityEngine.UI;

public class SelectObjectKeyBoardSetSizeText : MonoBehaviour
{
    [SerializeField]
    private LogicSetSelectObjectOnTopKeyboard _logicSetSelectObjectOnTopKeyboard;

    [SerializeField]
    private int _priority;
    
    [SerializeField] 
    private GetDataSODataDKODataKey _keyDkoText;
    
    [SerializeField]
    private int _textSize;


    private int _lastSize = 25;

    private void Awake()
    {
        _logicSetSelectObjectOnTopKeyboard.OnStartSetPosition.Subscribe(OnStartSetPosition, _priority);
        _logicSetSelectObjectOnTopKeyboard.OnStartReturnPosition.Subscribe(OnStartReturnPosition, _priority);
    }

    
    private void OnStartSetPosition(DKOKeyAndTargetAction dko)
    {
        if (dko != null)
        {
            if (dko.ActionIsAlready(_keyDkoText.GetData()) == true) 
            {
                var dkoText = (DKODataInfoT<AbsGetTextUI>)dko.KeyRun(_keyDkoText.GetData());
                Text text = dkoText.Data.GetText();

                _lastSize = text.fontSize;
                
                text.fontSize = _textSize;
            }
            
        }
    }
    
    private void OnStartReturnPosition(DKOKeyAndTargetAction dko)
    {
        if (dko.ActionIsAlready(_keyDkoText.GetData()) == true)
        {
            var dkoText = (DKODataInfoT<AbsGetTextUI>)dko.KeyRun(_keyDkoText.GetData());
            Text text = dkoText.Data.GetText();

            text.fontSize = _lastSize;
        }
    }

    

}
