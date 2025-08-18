using System;

public class Example1GetKeyDataMono : AbsGetKeyDataMono<SwithcerExampleTypePlatform, Example1GetKeyData>
{
    public override bool IsInit { get; }
    public override event Action OnInit;
    //тут типа откудо то(возможно через Dko) взяли ключ
    public override Example1GetKeyData GetKeyData()
    {
        throw new NotImplementedException();
    }
}
