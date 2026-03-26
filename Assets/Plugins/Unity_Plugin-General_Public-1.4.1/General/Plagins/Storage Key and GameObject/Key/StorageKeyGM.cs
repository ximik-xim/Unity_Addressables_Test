using UnityEngine;

[System.Serializable]
public class StorageKeyGM 
{
    [SerializeField]
    private string _key;

    public string GetKey()
    {
        return _key;
    }
}
