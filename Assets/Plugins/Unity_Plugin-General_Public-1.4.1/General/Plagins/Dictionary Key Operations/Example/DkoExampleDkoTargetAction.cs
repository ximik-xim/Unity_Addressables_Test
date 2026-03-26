using UnityEngine;

/// <summary>
/// Пример задачи (ничего не принимает, и ничего не возращает)
/// </summary>
public class DkoExampleDkoTargetAction : DKOTargetAction
{
    private void Awake()
    {
        LocalAwake();
    }
    
    protected override DKODataRund InvokeRun()
    {
        Debug.Log("Типа задача запущена");
        return null;
    }
}
