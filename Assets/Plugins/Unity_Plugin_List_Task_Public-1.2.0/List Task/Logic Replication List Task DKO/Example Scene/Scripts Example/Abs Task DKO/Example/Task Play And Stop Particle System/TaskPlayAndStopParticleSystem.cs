using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Запустит/Остановит систему частит при запуске Task 
/// </summary>
public class TaskPlayAndStopParticleSystem : TL_AbsTaskLogicDKO
{
    public override event Action OnInit;
    public override bool IsInit => true;
    
    public override event Action OnCompletedLogic;
    public override bool IsCompletedLogic => _isCompletedLogic;
    private bool _isCompletedLogic = true;

    [SerializeField] 
    private List<ParticleSystem>  _particleSystem;

    [SerializeField] 
    private TaskTypeActionParticleSystem _typeActionParticleSysteam;
    
    
    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override void StartLogic(DKOKeyAndTargetAction tileDKO)
    {
        _isCompletedLogic = false;

        switch (_typeActionParticleSysteam)
        {
            case TaskTypeActionParticleSystem.Play:
            {
                foreach (var VARIABLE in _particleSystem)
                {
                    VARIABLE.Play();
                }
            } 
            break;
            
            case TaskTypeActionParticleSystem.Stop:
            {
                foreach (var VARIABLE in _particleSystem)
                {
                    VARIABLE.Stop(false, ParticleSystemStopBehavior.StopEmitting);
                }
            } 
                break;
            
            case TaskTypeActionParticleSystem.StopAndClear:
            {
                foreach (var VARIABLE in _particleSystem)
                {
                    VARIABLE.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
            } 
                break;
        }
        
        _isCompletedLogic = true;
        OnCompletedLogic?.Invoke();
    }
}

public enum TaskTypeActionParticleSystem
{
    Play,
    Stop,
    StopAndClear
}