using UnityEngine;
using UnityEngine.Networking;

public class DataUnityWebRequestAndText
{
    public DataUnityWebRequestAndText(UnityWebRequest unityWebRequest,string textErrorPush)
    {
        _unityWebRequest = unityWebRequest;
        _textErrorPush = textErrorPush;
    }

    public UnityWebRequest UnityWebRequest => _unityWebRequest;
    private UnityWebRequest _unityWebRequest;

    public string TextErrorPush => _textErrorPush;
    private string _textErrorPush;
}
