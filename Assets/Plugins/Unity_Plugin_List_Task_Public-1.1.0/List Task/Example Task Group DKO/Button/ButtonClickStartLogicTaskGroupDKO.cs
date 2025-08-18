using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickStartLogicTaskGroupDKO : MonoBehaviour
{
    [SerializeField] 
    private Button _button;
    
    [SerializeField] 
    private DKOKeyAndTargetAction _dko;

    [SerializeField] 
    private LogicTaskGroupDKO _logicTaskGroup;
    
    [SerializeField] 
    private KeyCode _keyClick = KeyCode.None;

    private void Awake()
    {
        _button.onClick.AddListener(ButtonClick);
    }

    private void Update()
    {
        if (Input.GetKey(_keyClick) == true)
        {
            ButtonClick();
        }
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
