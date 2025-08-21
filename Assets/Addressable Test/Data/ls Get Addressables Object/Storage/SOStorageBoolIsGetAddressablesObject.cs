using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

[CreateAssetMenu(menuName = "Storage Is Get Addressables Object")]
public class SOStorageBoolIsGetAddressablesObject : ScriptableObject
{

    /// <summary>
    /// это списоки разрешенных или запрещенных к загр. обьектов 
    /// </summary>
    [SerializeField] 
    private bool _isBlockList;
    
    [SerializeField] 
    private List<AssetReference> _refObj;
    
    [SerializeField] 
    private List<string>  _key;
    
    [SerializeField] 
    private List<Hash128>  _GUID;
    
    [SerializeField] 
    private List<string>  _iResourceLocationLocal;
    
    
    public bool IsGetObject(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        
        // AssetReference
        if (obj is AssetReference assetRef)
        {
            //Если это список разрешенных обьектов
            if (_isBlockList == false)
            {
                if (_refObj.Contains(assetRef) == true)
                {
                    return true;
                }

                return false;
            }

            //Если это список запрещенных обьектов
            if (_refObj.Contains(assetRef) == true)
            {
                return false;
            }

            return true;

        }
        
        // Key
        if (obj is string strKey)
        {
            //Если это список разрешенных обьектов
            if (_isBlockList == false)
            {
                if (_key.Contains(strKey) == true)
                {
                    return true;
                }

                return false;
            }

            //Если это список запрещенных обьектов
            if (_key.Contains(strKey) == true)
            {
                return false;
            }

            return true;

        }
        
        // Hash128 (GUID)
        if (obj is Hash128 guid)
        {
            //Если это список разрешенных обьектов
            if (_isBlockList == false)
            {
                if (_GUID.Contains(guid) == true)
                {
                    return true;
                }

                return false;
            }

            //Если это список запрещенных обьектов
            if (_GUID.Contains(guid) == true)
            {
                return false;
            }

            return true;
        }


        // IResourceLocation
        if (obj is IResourceLocation iResourceLocation)
        {
            //Если это список разрешенных обьектов
            if (_isBlockList == false)
            {
                if (_iResourceLocationLocal.Contains(iResourceLocation.InternalId) == true)
                {
                    return true;
                }

                return false;
            }

            //Если это список запрещенных обьектов
            if (_iResourceLocationLocal.Contains(iResourceLocation.InternalId) == true)
            {
                return false;
            }

            return true;
        }
        
        return false;
    }
}
