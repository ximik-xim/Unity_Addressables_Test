using System;
using UnityEngine;

/// <summary>
/// Эта абстракция нужна, что бы можно было реализовать разные способы получения ресурсов игры через Addressables(локально(Locak), через обновление с сервер(Remote) и т.д)
/// - В аргумент вместо object можно передать
/// 1) string (key - ключ, который вручную задаем)
/// 2) AssetReference (прям ссылку)
/// 3) IResourceLocation (можно получить через  Addressables.LoadResourceLocationsAsync)
/// ! НЕ работает с
/// 1) GUID (можно получить к примеру черз тот же AssetReference (экземпляр).AssetGUID)
/// 2) Прямым путем до обьекта(он же InternalId(поле) у IResourceLocation), пример Assets/Addressable Test/Example/Data Example/Example Object.prefab 
/// </summary>
public abstract class AbsCallbackGetDataAddressables : AbsCallbackGetData<object>
{
}
