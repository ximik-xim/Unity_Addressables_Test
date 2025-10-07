using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Нужно что бы через имя сцены можно было соотнести с ключем сцены
/// (т.к в некоторых случ(пример Addressable) имя сцены != ключ по котор. загружаю сцену
/// (по идеи будет исп. при загрузке сцены, что бы по имяни сцены получить её ключ)
/// </summary>
public class StorageNameSceneAndKeyNameScene : MonoBehaviour
{
    private Dictionary<string, string> _dictionary = new Dictionary<string, string>();

#if  UNITY_EDITOR
    [SerializeField]
    private List<AbsKeyData<string, string>> _visibleData = new List<AbsKeyData<string, string>>();
#endif

    public void AddSceneKey(string nameScene, KeyNameScene keyNameScene)
    {
        
#if  UNITY_EDITOR
        _visibleData.Add(new AbsKeyData<string, string>(nameScene, keyNameScene.GetKey()));
#endif
        _dictionary.Add(nameScene,keyNameScene.GetKey());
    }

    public KeyNameScene GetSceneKey(string nameScene)
    {
        return new KeyNameScene(_dictionary[nameScene]);
    }

    /// <summary>
    /// Есть ли это имя сцены в списке
    /// </summary>
    /// <returns></returns>
    public bool IsContainsNameScene(string nameScene)
    {
        return _dictionary.ContainsKey(nameScene);
    }
}
