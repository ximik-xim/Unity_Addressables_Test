using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class sssss : MonoBehaviour
{
    [SerializeField]
    private AssetReference _key;
   
    [SerializeField] 
    private AbsCallbackGetDataAddressables _getDataAddressables;

    private void Awake()
    {
        Addressables.LoadSceneAsync(_key);
    }

}
