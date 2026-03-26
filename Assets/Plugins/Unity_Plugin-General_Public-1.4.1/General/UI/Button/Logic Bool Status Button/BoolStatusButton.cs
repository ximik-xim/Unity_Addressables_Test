using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Выполняет роль переключателя статуса bool для кнопки.
/// </summary>
public class BoolStatusButton : MonoBehaviour
{
   [SerializeField]
   private Button _button;

   [SerializeField]
   private bool _statusSelect = false;
   public bool StatusSelect => _statusSelect;
   
   public WrapperCustomEventPriorityT<bool> OnUpdateStatusSelect => OnEventUpdateStatusSelect.WrapperCustomEventPriorityT;
   private HostCustomEventPriorityT<bool> OnEventUpdateStatusSelect = new HostCustomEventPriorityT<bool>();
   
   
   private void Awake()
   {
      _button.onClick.AddListener(OnClick);
   }

   private void OnClick()
   {
      _statusSelect = !_statusSelect;
      OnEventUpdateStatusSelect.CustomEventPriorityT.Invoke(_statusSelect);
   }

   public void SetStatus(bool status)
   {
      _statusSelect = status;
      OnEventUpdateStatusSelect.CustomEventPriorityT.Invoke(_statusSelect);
   }

   private void OnDestroy()
   {
      _button.onClick.RemoveListener(OnClick);
   }
}
