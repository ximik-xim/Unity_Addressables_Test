
public class ExampleSwitcherKeyGetKeyMono : AbsSwitcherKeyGetKeyMono<SwithcerExampleTypePlatform,Example1GetKeyDataMono,Example1GetKeyData>
{
    private void Awake()
    {
        StartAwake();
    }

    private void OnValidate()
    {
        //На случай если SDK не ответит, нужно установить этот тип(как дефолтное значение)
        AddType(SwithcerExampleTypePlatform.None);
        
        AddType(SwithcerExampleTypePlatform.Desktop);
        AddType(SwithcerExampleTypePlatform.Mobile);
        AddType(SwithcerExampleTypePlatform.Tablet);
        AddType(SwithcerExampleTypePlatform.Tv);

        DeletionDuplication();
    }
    
    private void OnEnable()
    {
        if (_keyData != null) 
        {
            CheckEnable();
        }
    }
}

