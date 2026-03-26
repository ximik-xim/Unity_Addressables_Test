using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Логика подсказки
/// (элемента в выпод. списке)
/// </summary>
public class ElementVariantText : MonoBehaviour
{
   public event Action<int, string, string> OnSelectItem;

   [SerializeField]
   private AbsTriggerClickElementVariantText _trigger;
   
   //Это текст который будет возращен
   private string _textReturn;
      
   //Это текст который будет виден в подсказке
   [SerializeField]
   private Text _text;
   
   private int _id;

   [SerializeField]
   private GameObject _visibleGm;

   [SerializeField]
   private RectTransform _parent;
   
   /// <summary>
   /// Нужен что бы когда обьект был не активен, его размер установить в 0
   /// (что бы не уничтожать кучу раз)
   /// </summary>
   private float _lastHeight = 0;
   private void Awake()
   {
      _trigger.OnButtonClick += OnButtonClick;
      _lastHeight = _parent.rect.height;
   }

   private void OnButtonClick()
   {
      SelectVariant();
   }

   /// <summary>
   /// Выбрать этот вариант подсказки
   /// </summary>
   public void SelectVariant()
   {
      OnSelectItem?.Invoke(_id, _text.text, _textReturn);
   }

   public void SetId(int id)
   {
      _id = id;
   }

   public int GetId()
   {
      return _id;
   }

   public void SetTextVisible(string text)
   {
      _text.text = text;
   }

   public string GetTextVisible()
   {
      return _text.text;
   }

   public void SetTextReturn(string text)
   {
      _textReturn = text;
   }
   
   public string GetTextReturn()
   {
      return _textReturn;
   }
   
   public void ActiveVisible()
   {
      _visibleGm.SetActive(true);
      _parent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _lastHeight);
   }

   public void DisactiveVisible()
   {
      if (_lastHeight == 0) 
      {
         _lastHeight = _parent.rect.height;
         Debug.Log($"Внимание, при отключении уст высота возврата = 0. Была установлена новая высота возврата = {_parent.rect.height}");
      }
      
      _visibleGm.SetActive(false);
      _parent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
   }
   
   private void OnDestroy()
   {
      _trigger.OnButtonClick -= OnButtonClick;
   }
}
