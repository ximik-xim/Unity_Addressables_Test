using UnityEngine;

/// <summary>
/// Пример запуска задачи DKO
/// </summary>
public class DkoExampleInvokeDkoKey : MonoBehaviour
{
    [SerializeField] 
    private DKOKeyAndTargetAction _keyAndTargetAction;

    [SerializeField] 
    private GetDataSODataDKODataKey _keyAction;
    
    private void Start()
    {
        _keyAndTargetAction.KeyRun(_keyAction.GetData());
    }
}
