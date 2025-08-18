using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickStartLogicListTaskDKO : MonoBehaviour
{
    [SerializeField] 
    private Button _button;
    
    [SerializeField] 
    private DKOKeyAndTargetAction _dko;

    [SerializeField] 
    private LogicListTaskDKO _logicTaskGroup;

    private void Awake()
    {
        _button.onClick.AddListener(ButtonClick);
    }

    private void ButtonClick()
    {
        _logicTaskGroup.StartAction(_dko);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ButtonClick);
    }
}
