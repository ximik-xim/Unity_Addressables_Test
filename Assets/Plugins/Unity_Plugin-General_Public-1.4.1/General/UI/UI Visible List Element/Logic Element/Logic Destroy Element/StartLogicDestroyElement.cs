using UnityEngine;

/// <summary>
/// Логика для запуска логики удаления элемента, которая будет находиться на самом элементе
/// </summary>
public class StartLogicDestroyElement : MonoBehaviour
{
    [SerializeField]
    private ElementData _elementData;
    
    [SerializeField] 
    private GetDataSODataDKODataKey _keyLogicRemoveElement;
    
    public void StartRemoveElement()
    {
        LogicStartDestroyElement logicRemoveElement = _elementData.DkoSpawnerElement.KeyRun<DKODataInfoT<LogicStartDestroyElement>>(_keyLogicRemoveElement.GetData()).Data;
        logicRemoveElement.RemoveElement(_elementData);
    }
}
