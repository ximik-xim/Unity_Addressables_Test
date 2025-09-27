using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Очистит кэш
/// (удалит загр. бандлы, но не загр. данные об каталоге, данные каталога наход. отдельно)
/// </summary>
public class ButtonClickClearCache : MonoBehaviour
{
    [SerializeField]
    private Button _button;
    
    private void Awake()
    {
        Init();
    }

    
    private void Init()
    {
        _button.onClick.AddListener(ButtonClick);
    }


    private void ButtonClick()
    {
        StartLogic();
    }

    private void StartLogic()
    {
        Caching.ClearCache();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ButtonClick);
    }
}
