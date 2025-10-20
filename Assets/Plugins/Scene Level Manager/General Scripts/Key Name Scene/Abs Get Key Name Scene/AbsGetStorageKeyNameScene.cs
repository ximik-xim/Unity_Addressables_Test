using System.Collections.Generic;

/// <summary>
/// Нужна, что бы можно было получ список ключей сцен разными способами
/// (в случ Addressable через Asset Ref Scene)
/// (в случ обычной логики, тупо через хранилеще со списком имен сцен)
/// </summary>
public abstract class AbsGetStorageKeyNameScene : AbsInitData<List<KeyNameScene>>
{

}
