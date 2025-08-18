using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsTaskPanelUI : AbsClosOpenPanelTask
{
    public abstract void SetData(WrapperTaskLoaderData data);
    public abstract DKOKeyAndTargetAction GetDKO();
}
