using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Хранит в себе порядковый номер сцены по её ключи(имяни сцены)
/// </summary>
public class StorageSceneNumber : MonoBehaviour
{
    private Dictionary<string, int> _dataNumberScene = new Dictionary<string, int>();

#if  UNITY_EDITOR
    [SerializeField]
    private List<AbsKeyData<string, int>> _visibleData = new List<AbsKeyData<string, int>>();
#endif

    
    public event Action OnUpdateData;
    /// <summary>
    /// сюда должен перед. остортированный по порядку список сцен
    /// (порядковый номер в списке и будет исп для индексации
    /// </summary>
    public void SetNumberScene(List<KeyNameScene> list)
    {
#if  UNITY_EDITOR
        _visibleData.Clear();
#endif
        
        _dataNumberScene.Clear();

        for (int i = 0; i < list.Count; i++)
        {
            
#if  UNITY_EDITOR
            _visibleData.Add(new AbsKeyData<string, int>(list[i].GetKey(), i));
#endif
            
            _dataNumberScene.Add(list[i].GetKey(), i);
        }
        
        OnUpdateData?.Invoke();
    }

    public void SetNumberScene(List<AbsKeyData<int, KeyNameScene>> list)
    {
#if  UNITY_EDITOR
        _visibleData.Clear();
#endif
        
        _dataNumberScene.Clear();

        foreach (var VARIABLE in list)
        {
#if  UNITY_EDITOR
            _visibleData.Add(new AbsKeyData<string, int>(VARIABLE.Data.GetKey(), VARIABLE.Key));
#endif
            
            _dataNumberScene.Add(VARIABLE.Data.GetKey(), VARIABLE.Key);
        }
        
        OnUpdateData?.Invoke();
    }

    /// <summary>
    /// Вернет текущий список ключей и номеров сцен к ним
    /// </summary>
    /// <returns></returns>
    public List<AbsKeyData<KeyNameScene, int>> GetCurrentNumberData()
    {
        List<AbsKeyData<KeyNameScene, int>> listData = new List<AbsKeyData<KeyNameScene, int>>();
        foreach (var VARIABLE in _dataNumberScene.Keys)
        {
            var data = new AbsKeyData<KeyNameScene, int>(new KeyNameScene(VARIABLE), _dataNumberScene[VARIABLE]);
            listData.Add(data);
        }

        return listData;
    }
    
    public bool ContainNumberScene(KeyNameScene keyNameScene)
    {
        return _dataNumberScene.ContainsKey(keyNameScene.GetKey());
    }
    
    public int GetNumberScene(KeyNameScene keyNameScene)
    {
        return _dataNumberScene[keyNameScene.GetKey()];
    }

    /// <summary>
    /// Вернет след по номеру сцену, если она есть
    /// </summary>
    public KeyNameScene GetNextNumberScene(KeyNameScene keyNameScene)
    {
        if (ContainNumberScene(keyNameScene) == true)
        {
            int currentNumber = GetNumberScene(keyNameScene);
            
            return GetNextNumberScene(currentNumber);
        }
        
        return null;
    }
    
    /// <summary>
    /// Вернет след по номеру сцену, если она есть
    /// </summary>
    public KeyNameScene GetNextNumberScene(int numberScene)
    {
        foreach (var VARIABLE in _dataNumberScene.Keys)
        {
            if (_dataNumberScene[VARIABLE] > numberScene)
            {
                return new KeyNameScene(VARIABLE);
            }
        }
        
        return null;
    }
}
