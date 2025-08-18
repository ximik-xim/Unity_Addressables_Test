using UnityEngine;

[System.Serializable]
public class MessengerBetweenSceneKeyDKO 
{
    [SerializeField]
    private string _key;

    public string GetKey()
    {
        return _key;
    }
}
