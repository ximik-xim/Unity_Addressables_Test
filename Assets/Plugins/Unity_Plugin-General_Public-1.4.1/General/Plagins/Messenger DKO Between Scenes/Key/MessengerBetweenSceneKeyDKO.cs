using UnityEngine;

[System.Serializable]
public class MessengerBetweenSceneKeyDKO 
{
    public MessengerBetweenSceneKeyDKO()
    {
        
    }
    
    public MessengerBetweenSceneKeyDKO(string key)
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
