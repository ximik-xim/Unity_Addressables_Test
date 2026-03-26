using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Тригерит нажатие по ссылки в TMP Text и выполняет переход по этой ссылке
/// </summary>
public class LogicOpenUrlTextTPM : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private TextMeshProUGUI _textMeshPro;

    public bool IsBlock => _isBlock;
    private bool _isBlock = false;
    public event Action OnUpdateStatusBlock;

    public event Action<PointerEventData> OnPointerClickHandler;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isBlock == false)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(_textMeshPro, eventData.position, eventData.pressEventCamera);

            if (linkIndex != -1)
            {
                TMP_LinkInfo linkInfo = _textMeshPro.textInfo.linkInfo[linkIndex];
                Application.OpenURL(linkInfo.GetLinkID());
            }    
        }
        
        OnPointerClickHandler?.Invoke(eventData);
    }

    public void SetStatusBlockLinc(bool statusBlock)
    {
        _isBlock = statusBlock;
        OnUpdateStatusBlock?.Invoke();
    }
    
}
