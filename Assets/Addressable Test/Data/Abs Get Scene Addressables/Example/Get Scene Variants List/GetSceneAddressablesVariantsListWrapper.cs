using System;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;

public class GetSceneAddressablesVariantsListWrapper : AbsCallbackGetSceneAddressables
{
    public override bool IsInit => _absGetData.IsInit;
    public override event Action OnInit
    {
        add
        {
            _absGetData.OnInit += value;
        }

        remove
        {
            _absGetData.OnInit -= value;
        }
    }
    
    [SerializeField]
    private GetSceneAddressablesVariantsList _absGetData;

    private void Awake()
    {
        _absGetData.StartInit();
    }
    
    public override GetServerRequestData<SceneInstance> GetData(DataSceneLoadAddressable data)
    {
        return _absGetData.GetData(data);
    }
}
