using System;
using UnityEngine;

/// <summary>
/// Нужен для упрощенного получения пути, до Action
/// </summary>
public class GetDKOPatch : MonoBehaviour
{

    [SerializeField] 
    private FindMBS_DKO_DontDestroy _findMbsDkoDontDestroy;

    [SerializeField] 
    private GetDataSO_MBS_DKO _keyDko;

    private DKOKeyAndTargetAction _Dko;
    [SerializeField] 
    private GetDataSODataDKODataKey _keyGeneralLogic;

    
    /// <summary>
    /// Будет ли сохраняться ссылка на DKODataRund после первого получения(иногда может быть важным нюансом)
    /// </summary>
    [SerializeField] 
    private bool _isSaveDataDKO = true;
    
    private DKODataRund _DkoDataRund;
    
    private bool _init = false;
    public bool Init => _init;
    public event Action OnInit;

    private void Awake()
    {
        if (_findMbsDkoDontDestroy == null)
        {
            _findMbsDkoDontDestroy = FindObjectOfType<FindMBS_DKO_DontDestroy>();
        }
        
        if (_findMbsDkoDontDestroy.Init == false)
        {
            _findMbsDkoDontDestroy.OnInit += OnInitFind;
            return;
        }

        InitFind();
        
    }

    private void OnInitFind()
    {
        _findMbsDkoDontDestroy.OnInit -= OnInitFind;
        InitFind();
    }
    
    private void InitFind()
    {
        if (_isSaveDataDKO == true)
        {
            if (_keyDko == null)
            {
                Debug.LogError($"Внимание у DKO с именем {this.gameObject.name} ключ не был установлен");    
            }

            if (_keyDko.GetData() == null)
            {
                Debug.LogError($"Внимание у DKO с именем {this.gameObject.name} проблема с ключем типа {_keyDko}, возращаемы им ключ == Null");
            }
            
            
            _Dko = _findMbsDkoDontDestroy.GetDontDestroyMbsDko.GetDKO(_keyDko.GetData());
            _DkoDataRund = _Dko.KeyRun(_keyGeneralLogic.GetData());
        }
        
        _init = true;
        OnInit?.Invoke();
    }

    public DKODataRund GetDKO(DKODataRund dkoDataRund = null)
    {
        if (_isSaveDataDKO == true)
        {
            return _DkoDataRund;
        }
        
        _Dko = _findMbsDkoDontDestroy.GetDontDestroyMbsDko.GetDKO(_keyDko.GetData());
        DKODataRund dataDKO = _Dko.KeyRun(_keyGeneralLogic.GetData(), dkoDataRund);
        
        return dataDKO;
    }

    public T GetDKO<T>(DKODataRund dkoDataRund = null) where T : DKODataRund
    {
        DKODataRund dataDKO = GetDKO(dkoDataRund);

        T returnData = null;

        if (dataDKO is T == true)
        {
            returnData = (T)dataDKO;
        }

        return returnData;
    }
}
