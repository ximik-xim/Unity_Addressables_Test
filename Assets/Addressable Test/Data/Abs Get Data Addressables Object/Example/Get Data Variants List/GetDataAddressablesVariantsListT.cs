
/// <summary>
/// Содержит список вариантов, откудова можно взять обьект.
/// (при ERROR от сервера, будет переключаться на след. вариант)
/// Будет поочереди перебирать все варианты, пока кто то не вернет статус OK, или пока не закончаться варианты
/// </summary>
public class GetDataAddressablesVariantsListT : AbsCallbackGetDataVariantsListT<AbsCallbackGetDataTAddressables, object>
{
    
}