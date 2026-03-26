using UnityEngine;

/// <summary>
/// Логика для запуска удаления элемента
/// (при удалении из списка, тупо уничтожаем обьект)
/// (логика удаления будет находиться на самом элементе)
/// </summary>
public class LogicStartDestroyElement : MonoBehaviour
{
   [SerializeField]
   private StorageElement _storageElement;
   
   [SerializeField] 
   private GetDataSO_MBS_DKO _keyDkoAddoneLogicElement;
    
   [SerializeField] 
   private GetDataSODataDKODataKey _keyLogicDestroyElement;
   
    public void RemoveElement(ElementData headerData)
    {
       _storageElement.RemoveElement(headerData);

        var DkoUiLogic = headerData.MbsDkoElement.GetDKO(_keyDkoAddoneLogicElement.GetData());
        AbsDestroyElement destroyElementLogic = DkoUiLogic.KeyRun<DKODataInfoT<AbsDestroyElement>>(_keyLogicDestroyElement.GetData()).Data;

       destroyElementLogic.StartDestroyElement();
    }
    
    
}
