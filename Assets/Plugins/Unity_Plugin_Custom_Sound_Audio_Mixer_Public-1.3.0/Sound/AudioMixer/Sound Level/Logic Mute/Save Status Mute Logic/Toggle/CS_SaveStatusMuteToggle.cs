using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Тут сохраняю именно статус у Toggle, а потом востанавливаю его из превсов
/// Но нужено дождаться пока Trigger инициализируеться, что бы он подписался на Toggle и тода изменив статус у Trigger(у IsMute)
/// (Нужен, если статус мута сохраняеться не у конкретного элемента, а к примеру у группы элементов, и за эту группу отвечает 1 триггер)
/// </summary>
public class CS_SaveStatusMuteToggle : MonoBehaviour
{
    [SerializeField] 
    private CS_AbsTriggerStatusMute _trigger;

    [SerializeField] 
    private Toggle _toggle;
    
    [SerializeField] 
    private string _pathSaveData;
    private void Start()
    {
        if (_trigger.IsInit == false)
        {
            _trigger.OnInit += OnInitTrigger;
            return;
        }
        
        InitTrigger();
    }

    private void OnInitTrigger()
    {
        _trigger.OnInit -= OnInitTrigger;
        InitTrigger();
    }

    private void InitTrigger()
    {
        //Тут именно isOn, что бы сработал event у Toggle
        _toggle.isOn = GetStatus();
        _toggle.onValueChanged.AddListener(UpdateStatus);
    }
    
    private void UpdateStatus(bool arg0)
    {
        OnUpdateMuteData();
    }

    private void OnUpdateMuteData()
    {
        if (_toggle.isOn == true) 
        {
            PlayerPrefs.SetString(_pathSaveData, "1");
        }


        if (_toggle.isOn == false) 
        {
            PlayerPrefs.SetString(_pathSaveData, "0");
        }
        
        PlayerPrefs.Save();
    }

    private bool GetStatus()
    {
        var status = PlayerPrefs.GetString(_pathSaveData);

        if (status ==  "1")
        {
            return true;
        }

        return false;
    }

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(UpdateStatus);
    }
}
