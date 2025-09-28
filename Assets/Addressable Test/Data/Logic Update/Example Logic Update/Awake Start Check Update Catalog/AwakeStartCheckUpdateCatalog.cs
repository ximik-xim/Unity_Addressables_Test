using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// Просто при запуске отдельно проверяет есть ли обновление каталогов
/// (нужен для тестов)
/// </summary>
public class AwakeStartCheckUpdateCatalog : MonoBehaviour
{
    [SerializeField]
    private bool _isStartInitAddressables = true;
    
    private void Awake()
    {
        if (_isStartInitAddressables == true)
        {
            //!!! ТУТ ОБЯЗ. НУЖНО ИНИЦ Addressables (перед запросом каталогов, если где то еще не был иниц)
            var callback  = Addressables.InitializeAsync();

            if (callback.IsDone == true)
            {
                ComplitedInitAddressables();
            }
            else
            {
                callback.Completed += OnInitComplitedInitAddressables;
            }
        

            void OnInitComplitedInitAddressables(AsyncOperationHandle<IResourceLocator> obj)
            {
                if (callback.IsDone == true)
                {
                    callback.Completed -= OnInitComplitedInitAddressables;
                    ComplitedInitAddressables();
                }
            }
        
            void ComplitedInitAddressables()
            {
                if (callback.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log("Addressables инициализирован");    
                }
                else
                {
                    Debug.LogError("Не удалось инициализировать Addressables");    
                }

                CheckUpdateAwake();
            }

        }
        else
        {
            CheckUpdateAwake();
        }
      
    }
    
    private void CheckUpdateAwake()
    {
        var callback  = Addressables.CheckForCatalogUpdates();

        if (callback.IsDone == true)
        {
            ComplitedInitAddressables();
        }
        else
        {
            callback.Completed += OnInitComplitedInitAddressables;
        }
        

        void OnInitComplitedInitAddressables(AsyncOperationHandle<List<string>> obj)
        {
            if (callback.IsDone == true)
            {
                callback.Completed -= OnInitComplitedInitAddressables;
                ComplitedInitAddressables();
            }
        }
        
        void ComplitedInitAddressables()
        {
            if (callback.Status == AsyncOperationStatus.Succeeded)
            {
                if (callback.Result != null && callback.Result.Count >= 0)
                {
                    Debug.Log($"Найдено обновлений каталогов = {callback.Result.Count}");  
                }
                else
                {
                    Debug.Log("Обновления каталогов отсутствуют");
                }   
            }
            else
            {
                Debug.LogError("Ошибка при запросе обновлений каталогов");    
            }
            
        }
    }
}
