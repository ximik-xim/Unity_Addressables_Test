using System;
using UnityEngine;

public class Collider3D_TriggerGG : AbsTriggerGG
{
    public override event Action OnInit;
    public override bool IsInit => true;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override event Action OnTriggerGG;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<GG>() == true) 
        {
            OnTriggerGG?.Invoke();
        }
    }
}
