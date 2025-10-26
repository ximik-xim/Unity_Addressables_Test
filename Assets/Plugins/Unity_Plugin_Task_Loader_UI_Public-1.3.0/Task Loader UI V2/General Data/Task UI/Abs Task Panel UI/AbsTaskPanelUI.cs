using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Абстракция для UI панели у Task
/// (нужна, что бы можно было реализовать разный UI для Task)
/// </summary>
public abstract class AbsTaskPanelUI : AbsClosOpenPanelTask
{
    public abstract void SetData(WrapperTaskLoaderData data);
    public abstract DKOKeyAndTargetAction GetDKO();
}
