using UnityEngine;
using UnityEngine.UI;

public class ButtonStartDestroyElement : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField]
    private StartLogicDestroyElement _logicDestroyElement;
    
    private void Awake()
    {
        _button.onClick.AddListener(ButtonClick);
    }

    private void ButtonClick()
    {
        _logicDestroyElement.StartRemoveElement();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ButtonClick);
    }
}
