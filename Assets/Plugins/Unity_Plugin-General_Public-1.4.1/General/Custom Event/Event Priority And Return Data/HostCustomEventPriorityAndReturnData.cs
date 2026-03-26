using UnityEngine;
/// <summary>
/// Это обертка нужна что бы
/// 1) не нужно было в Awake, Start и т.д вызывать создание new CustomEventPriorityT и т.д
/// т.к тогда надо ждать инициализации скрипта и только потом разрешать подписку, а это не всегда возможно и усложняет весь код
/// 2) т.к при обьявлении ГЛОБАЛЬНОй переменной в new CustomEventPriorityT(аргументы), можно указывать только статические обьекты(методы, event и т.д)
/// по этому пришлось вот так это все обернуть. Зато теперь глобальную переменную можно обьявить как .... и все будет работать(и не надо вызывать ни какую инициализацию и т.д)
/// </summary>
/// <typeparam name="InvokeType"></typeparam>
public class HostCustomEventPriorityAndReturnData<ReturnType, InvokeType>
{
    public HostCustomEventPriorityAndReturnData()
    {
        _customEventPriorityT = new CustomEventPriorityAndReturnData<ReturnType, InvokeType>();
        _wrapperCustomEventPriorityT = new WrapperCustomEventPriorityAndReturnData<ReturnType, InvokeType>(CustomEventPriorityT);
    }

    private CustomEventPriorityAndReturnData<ReturnType, InvokeType> _customEventPriorityT;
    private WrapperCustomEventPriorityAndReturnData<ReturnType, InvokeType> _wrapperCustomEventPriorityT;

    public CustomEventPriorityAndReturnData<ReturnType, InvokeType> CustomEventPriorityT => _customEventPriorityT;
    public WrapperCustomEventPriorityAndReturnData<ReturnType, InvokeType> WrapperCustomEventPriorityT => _wrapperCustomEventPriorityT;
}
