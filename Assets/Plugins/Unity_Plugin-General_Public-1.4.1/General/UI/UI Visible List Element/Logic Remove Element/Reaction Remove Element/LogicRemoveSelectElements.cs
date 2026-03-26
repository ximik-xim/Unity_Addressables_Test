using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Логика для удаления нескольких элементов
/// (у элементов не обез. она будет, это как доп модуль)
/// </summary>
public class LogicRemoveSelectElements : MonoBehaviour
{
    [SerializeField]
    private AbsEventRemoveElement _eventRemoveElement;
        
    [SerializeField]
    private LogicStartDestroyElement _logicDestroyElement;

    [SerializeField]
    private StorageElement _storageElement;

    [SerializeField] 
    private GetDataSO_MBS_DKO _keyDkoAddoneLogicElement;
    
    [SerializeField] 
    private GetDataSODataDKODataKey _keyLogicIsRemoveElement;
    
    private void Awake()
    {
        _eventRemoveElement.OnRemoveElement += StartRemoveElements;
    }

    public void StartRemoveElements()
    {
        List<ElementData> listElementsData = new List<ElementData>();
        
        for (int i = 0; i < _storageElement.GetCountElement(); i++)
        {
            var elementData = _storageElement.GetElement(i);

            if (elementData.MbsDkoElement.DKOIsAlready(_keyDkoAddoneLogicElement.GetData()) == true) 
            {
                var DkoUiLogic = elementData.MbsDkoElement.GetDKO(_keyDkoAddoneLogicElement.GetData());

                if (DkoUiLogic.ActionIsAlready(_keyLogicIsRemoveElement.GetData()) == true) 
                {
                    AbsIsRemoveElement absIsRemoveElement = DkoUiLogic.KeyRun<DKODataInfoT<AbsIsRemoveElement>>(_keyLogicIsRemoveElement.GetData()).Data;

                    if (absIsRemoveElement.IsRemoveElement() == true) 
                    {
                        listElementsData.Add(elementData);
                    }
                    
                }
            }
            
        }
        
        foreach (var VARIABLE in listElementsData)
        {
            _logicDestroyElement.RemoveElement(VARIABLE);
        }
    }

    private void OnDestroy()
    {
        _eventRemoveElement.OnRemoveElement -= StartRemoveElements;
    }
}
