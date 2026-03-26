
using UnityEngine;

public class LogicDestroyElementGM : AbsDestroyElement
{
    [SerializeField]
    private GameObject _targetElementDestroy;

    public override void StartDestroyElement()
    {
        Destroy(_targetElementDestroy);
    }
    
}
