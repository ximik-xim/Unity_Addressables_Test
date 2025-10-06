using UnityEngine;
using UnityEngine.UI;

public class ToggleDevelopPanelSetActiveAutoClearTaskUI : MonoBehaviour
{
    [SerializeField]
    private Toggle _toggle;

    private GetPatchDKOAutoClearTaskUI _logicAutoClearTaskUI;

    [SerializeField]
    private bool _isActiveAutoClear;
   
    [SerializeField]
    private GetDKOPatch _getDkoPatchLogger;
    private void Awake()
    {
        if (_getDkoPatchLogger.Init == false)
        {
            _getDkoPatchLogger.OnInit += OnInitGetDkoPatch;
        }
        
        CheckInit();
    }
    
    private void OnInitGetDkoPatch()
    {
        if (_getDkoPatchLogger.Init == true)
        {
            _getDkoPatchLogger.OnInit -= OnInitGetDkoPatch;
            CheckInit();
        }
    }
    
    private void CheckInit()
    {
        if (_getDkoPatchLogger.Init == true)  
        {
            _logicAutoClearTaskUI = _getDkoPatchLogger.GetDKO<DKODataInfoT<GetPatchDKOAutoClearTaskUI>>().Data;
            
            _logicAutoClearTaskUI.OnUpdateStatusActive += OnUpdateStatusActive;
            OnUpdateStatusActive();
            
            _toggle.onValueChanged.AddListener(ToggleClick);
        }
    }

    private void OnUpdateStatusActive()
    {
        if (_toggle.isOn != _logicAutoClearTaskUI.IsActive)
        {
            _toggle.SetIsOnWithoutNotify(_logicAutoClearTaskUI.IsActive);    
        }
    }
    
    private void ToggleClick(bool isOn)
    {
        _logicAutoClearTaskUI.SetActiveLogic(isOn);   
    }
    
    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(ToggleClick);

        if (_logicAutoClearTaskUI != null) 
        {
            _logicAutoClearTaskUI.OnUpdateStatusActive -= OnUpdateStatusActive;
        }
    }
}
