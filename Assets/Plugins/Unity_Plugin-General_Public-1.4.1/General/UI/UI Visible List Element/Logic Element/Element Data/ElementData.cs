using System;
using UnityEngine;

/// <summary>
/// По факту служит связующим шлюзом. А что будет в этом шлюзе и как работать
/// Определять будет уже конкретная реализация
/// </summary>
public class ElementData : MonoBehaviour
{
    [SerializeField]
    private LogicMessengerDKOBetweenScenes _mbsDkoElement;
    public LogicMessengerDKOBetweenScenes MbsDkoElement => _mbsDkoElement;
    
    public DKOKeyAndTargetAction DkoSpawnerElement => _dkoSpawnerElement;
    private DKOKeyAndTargetAction _dkoSpawnerElement;
    public event Action OnUpdateDataDkoSpawnerElement;

    public void SetDkoSpawner(DKOKeyAndTargetAction dkoSpawnerElement)
    {
        _dkoSpawnerElement = dkoSpawnerElement;
        OnUpdateDataDkoSpawnerElement?.Invoke();
    }
}
