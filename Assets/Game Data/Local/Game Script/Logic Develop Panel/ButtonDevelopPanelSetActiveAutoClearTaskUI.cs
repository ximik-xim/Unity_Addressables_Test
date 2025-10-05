using UnityEngine;
using UnityEngine.UI;

public class ButtonDevelopPanelSetActiveAutoClearTaskUI : MonoBehaviour
{
    [SerializeField]
    private Button _button;

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
            _button.onClick.AddListener(ButtonClick);
        }
    }

    private void ButtonClick()
    {
        _logicAutoClearTaskUI.SetActiveLogic(_isActiveAutoClear);   
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ButtonClick);  
    }
}
