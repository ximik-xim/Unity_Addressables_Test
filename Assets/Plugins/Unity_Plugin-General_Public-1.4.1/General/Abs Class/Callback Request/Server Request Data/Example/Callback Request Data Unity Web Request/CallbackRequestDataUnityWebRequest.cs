using UnityEngine;
using UnityEngine.Networking;

public class CallbackRequestDataUnityWebRequest : AbsServerRequestDataWrapper<UnityWebRequest>
{
    public CallbackRequestDataUnityWebRequest(int id) : base(id)
    {
    }
}
