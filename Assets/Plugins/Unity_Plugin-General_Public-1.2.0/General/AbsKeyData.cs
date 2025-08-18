[System.Serializable]
public class AbsKeyData<K,D> 
{
    public AbsKeyData(K key,D data)
    {
        Key = key;
        Data = data;
    }

    public K Key;
    public D Data;
}
