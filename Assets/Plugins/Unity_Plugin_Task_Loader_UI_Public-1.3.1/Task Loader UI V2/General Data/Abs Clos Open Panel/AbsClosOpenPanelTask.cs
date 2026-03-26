using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Нужен что бы включать и выключать,
/// как панель Storage Task UI, так и панель Task UI
/// </summary>
public abstract class AbsClosOpenPanelTask : MonoBehaviour
{
    public abstract void OpenPanel();
    public abstract void ClosePanel();
}
