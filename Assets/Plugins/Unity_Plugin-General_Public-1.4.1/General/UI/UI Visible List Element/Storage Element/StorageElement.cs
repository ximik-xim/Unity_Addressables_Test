using System.Collections.Generic;
using UnityEngine;

public class StorageElement : MonoBehaviour
{
    [SerializeField]
    private List<ElementData> _storageElement = new List<ElementData>();

    public void AddElement(ElementData headerData)
    {
        _storageElement.Add(headerData);
    }

    public void RemoveElement(int id)
    {
        _storageElement.RemoveAt(id);
    }
    
    public void RemoveElement(ElementData headerData)
    {
        _storageElement.Remove(headerData);
    }

    public int GetCountElement()
    {
        return _storageElement.Count;
    }

    public ElementData GetElement(int id)
    {
        return _storageElement[id];
    }
}
