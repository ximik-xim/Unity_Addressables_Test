using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Логика добавление элемента
/// (может быть совершенно разной, в зависимости от задачи)
/// </summary>
public class ReactionEventAddSpawnElement : MonoBehaviour
{
    [SerializeField]
    private AbsEventAddElement _addElement;
    
    [SerializeField]
    private SpawnerElementMBS_DKO _spawnerElement;

    [SerializeField]
    private StorageElement _storageElement;

    [SerializeField]
    private DKOKeyAndTargetAction _dkoSpawner;
    
    
    private void Awake()
    {
        if (_addElement.IsInit == false) 
        {
            _addElement.OnInit += OnInitAddElement;
        }
        else
        {
            Init();
        }
    }

    private void OnInitAddElement()
    {
        if (_addElement.IsInit == true)
        {
            _addElement.OnInit -= OnInitAddElement;
            
            Init();
        }
    }

    private void Init()
    {
        _addElement.OnAddElement += AddHeaderElementsEvent;
    }

    private void AddHeaderElementsEvent(int count)
    {
        AddHeaderElements(count);
    }

    public List<ElementData> AddHeaderElements(int count)
    {
        List<ElementData> list = new List<ElementData>();
        for (int i = 0; i < count; i++)
        {
            var headerElement = _spawnerElement.CreateElement();
            _storageElement.AddElement(headerElement);
            headerElement.SetDkoSpawner(_dkoSpawner);
            
            list.Add(headerElement);
        }

        return list;
    }

    public ElementData AddHeaderElement(ElementData prefabElementData = null)
    {
        var headerElement = _spawnerElement.CreateElement(prefabElementData);
        _storageElement.AddElement(headerElement);
        headerElement.SetDkoSpawner(_dkoSpawner);
        
        return headerElement;
    }


    private void OnDestroy()
    {
        _addElement.OnAddElement -= AddHeaderElementsEvent;
    }
}
