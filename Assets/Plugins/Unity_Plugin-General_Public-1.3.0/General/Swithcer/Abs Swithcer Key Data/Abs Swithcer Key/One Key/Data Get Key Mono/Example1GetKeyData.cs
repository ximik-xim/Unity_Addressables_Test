using System;

[System.Serializable]
public class Example1GetKeyData : AbsGetKeyData<SwithcerExampleTypePlatform>
{
    public override SwithcerExampleTypePlatform GetCurrentKey { get; }
    public override event Action OnUpdateKey;
}

public enum SwithcerExampleTypePlatform
{
    None = 0,
    Desktop = 1,
    Mobile = 2,
    Tablet = 3,
    Tv = 4
}