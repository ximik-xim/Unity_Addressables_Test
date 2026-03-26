using UnityEngine;

[System.Serializable]
public class KeyDropdown
{
    public KeyDropdown()
    {
        _key = "";
    }
    
    public KeyDropdown(string key)
    {
        _key = key;
    }
    
    [SerializeField]
    private string _key;

    public string GetKey()
    {
        return _key;
    }
}
