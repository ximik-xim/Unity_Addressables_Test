using UnityEngine;

[System.Serializable]
public class AbsKeyData<K,D> 
{
    public AbsKeyData(K key,D data)
    {
        Key = key;
        Data = data;
    }

    [SerializeField]
    public K Key;
    
    [SerializeField]
    public D Data;
}
