using UnityEngine;

public class SpawnerElementMBS_DKO : MonoBehaviour
{
    [SerializeField]
    private ElementData _elementData;
    
    [SerializeField] 
    private GameObject _parent;
    

    public ElementData CreateElement(ElementData elementData = null)
    {
        if (elementData == null) 
        {
            elementData = _elementData;
        }
        
        var element = Instantiate(elementData, _parent.transform);

        return element;
    }
}
