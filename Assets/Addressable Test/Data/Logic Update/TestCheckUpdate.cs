using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

// тут напис логику для обновлени (подум)
//     
// нормально описать и переименовать
public class TestCheckUpdate : MonoBehaviour
{
    // private AsyncOperationHandle<List<string>> _checkUpdateCatalogHandle;
    //
    // private AsyncOperationHandle<List<IResourceLocator>> _updateCatalogHandle;
    // void Start()
    // {
    //      _checkUpdateCatalogHandle = Addressables.CheckForCatalogUpdates();
    //
    //     //Проверяю закончилась ли операция по проверки наличии обновления 
    //     if (_checkUpdateCatalogHandle.IsDone == true)
    //     {
    //         //Если удалось свезаться с сервером успешно
    //         if (_checkUpdateCatalogHandle.Status == AsyncOperationStatus.Succeeded)
    //         {
    //             CompletedCheckUpdate(_checkUpdateCatalogHandle.Result);    
    //         }
    //         else
    //         {
    //             вот тут вызываю локальнную загр. т.к не удалось выполн. запрос к серверу
    //         }
    //         
    //     }
    //     else
    //     {
    //         _checkUpdateCatalogHandle.Completed += OnCompletedCheckUpdate;
    //     }
    //     
    //     
    //     
    // }
    //
    // private void OnCompletedCheckUpdate(AsyncOperationHandle<List<string>> obj)
    // {
    //     if (_checkUpdateCatalogHandle.IsDone == true) 
    //     {
    //         _checkUpdateCatalogHandle.Completed -= OnCompletedCheckUpdate;
    //         
    //         if (_checkUpdateCatalogHandle.Status == AsyncOperationStatus.Succeeded)
    //         {
    //             CompletedCheckUpdate(_checkUpdateCatalogHandle.Result);    
    //         }
    //         else
    //         {
    //             вот тут вызываю локальнную загр. т.к не удалось выполн. запрос к серверу
    //         }
    //     }
    //     
    // }
    //
    // private void CompletedCheckUpdate(List<string> catalogsUpdate)
    // {
    //     //Если пришел не пустой список начинаю выгрузку обновлений
    //     if (catalogsUpdate != null && catalogsUpdate.Count > 0)
    //     {
    //         //Вызываю обновление данных каталога(список ключей - бандл)
    //         _updateCatalogHandle = Addressables.UpdateCatalogs();
    //           
    //         
    //         if (_updateCatalogHandle.IsDone == true)
    //         {
    //             //Если удалось успешно загрузить новые катологи с сервера
    //             if (_updateCatalogHandle.Status == AsyncOperationStatus.Succeeded)
    //             {
    //                 CompletedUpdate(_updateCatalogHandle.Result);    
    //             }
    //             else
    //             {
    //                 вот тут вызываю локальнную загр. т.к не удалось выполн. запрос к серверу
    //             }
    //         
    //         }
    //         else
    //         {
    //             _updateCatalogHandle.Completed += OnCompletedUpdate;
    //         }
    //         
    //         
    //         
    //
    //     }
    //     
    // }
    //
    //
    //
    // private void OnCompletedUpdate(AsyncOperationHandle<List<IResourceLocator>> obj)
    // {
    //     if (_updateCatalogHandle.IsDone == true) 
    //     {
    //         _updateCatalogHandle.Completed -= OnCompletedUpdate;
    //         
    //         if (_updateCatalogHandle.Status == AsyncOperationStatus.Succeeded)
    //         {
    //             CompletedUpdate(_updateCatalogHandle.Result);    
    //         }
    //         else
    //         {
    //             вот тут вызываю локальнную загр. т.к не удалось выполн. запрос к серверу
    //         }
    //     }
    //     
    // }
    //
    // private string bundleKey;
    // private void CompletedUpdate(List<IResourceLocator> obj)
    // {
    //     Addressables.LoadAssetAsync<GameObject>(bundleKey,я);
    //
    //     AssetReference sss;
    //     sss.
    //     
    //     IResourceLocation d;
    //     Addressables.LoadResourceLocationsAsync(bundleKey);
    //
    //     
    //     
    //     затем нужно вызвать скачивание катологов
    //     Addressables.DownloadDependenciesAsync(obj);
    //     
    //     и вот тут все интереснее
    //         
    //         1) надо проверить можно ли по отдельности делать запросы на скачивание балдов
    //             Если да то по отдельности скачивать каждый элемент
    //             Если нет.... То тут сложнее
    //             
    //         2) Если не удалось скачать конкретный элемент, то пробую перезакачать его дважды
    //             Если не удалось делаю запрос к хранилещу с запасными бандлами и спрашиваю, могу ли вместо скачиного(с сервера) бандла исп локальный бандл(запасной)
    //                 Если да, то просто продолжаю
    //                     Если нет, выкидивыю Error и не продолжаю загрузку
    //                         
    //         3)Если не удалось все разом скачать(нет возм отделять), тогда опять же дел. запрос к хран с запасн. бандлами, и если могу исп лок., то исп их, иначе фатаальная ошибка
    // }
    //
    // // Update is called once per frame
    // void Update()
    // {
    //     
    // }
}
