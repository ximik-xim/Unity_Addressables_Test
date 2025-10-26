using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Логика самого хранилеща с DKO(но по задумке тут нужны именно унаследоваться, для получения разных реализаций взаимодействия)
/// </summary>
public abstract class LogicMessengerDKOBetweenScenes : MonoBehaviour
{
    private bool _isInit = false;
    public bool IsInit => _isInit;
    public event Action OnInit;
    public event Action<string> OnInitElement;
    
    [SerializeField]
    private List<AbsKeyData<GetDataSO_MBS_DKO, DKOKeyAndTargetAction>> _list = new List<AbsKeyData<GetDataSO_MBS_DKO, DKOKeyAndTargetAction>>();

    private Dictionary<string, DKOKeyAndTargetAction> _dictionary = new Dictionary<string, DKOKeyAndTargetAction>();

#if  UNITY_EDITOR
    [SerializeField]
    private List<AbsKeyData<string, DKOKeyAndTargetAction>> _visibleData = new List<AbsKeyData<string, DKOKeyAndTargetAction>>();
#endif
    
    private void Awake()
    {
        LocalAwake();
    }

    protected void LocalAwake()
    {
        for (int i = 0; i < _list.Count; i++)
        {
            if (_list[i].Key == null)
            {
                Debug.LogError($"Ошибка при инициализации. SO ключа == null. Элемент под номером {i}");    
            }
            
            if (_list[i].Key.GetData() == null)
            {
                Debug.LogError($"Ошибка при инициализации. Внимание пришел null вместо ключа. Элемент под номером {i}");
            }

            if (_list[i].Key.GetData().GetKey() == null)
            {
                Debug.LogError($"Ошибка при инициализации. Внимание проблема с ключем, возвращаемым им ключ == Null. Элемент под номером {i}");
            }

#if  UNITY_EDITOR
            _visibleData.Add(new AbsKeyData<string, DKOKeyAndTargetAction>(_list[i].Key.GetData().GetKey(), _list[i].Data));
#endif
            _dictionary.Add(_list[i].Key.GetData().GetKey(), _list[i].Data);
        }

        StartInit();
    }
    
    private void StartInit()
    {
        List<AbsKeyData<GetDataSO_MBS_DKO, DKOKeyAndTargetAction>> _buffer = new List<AbsKeyData<GetDataSO_MBS_DKO, DKOKeyAndTargetAction>>();
        bool _isStart = false;
        
        StartLogic();

        void StartLogic()
        {
            if (_isInit == false)
            {
                _isStart = true;

                foreach (var VARIABLE in _list)
                {
                    if (VARIABLE.Data.IsInit == false)
                    {
                        _buffer.Add(VARIABLE);
                        VARIABLE.Data.OnInit += CheckInitCompleted;
                    }
                    else
                    {
                        OnInitElement?.Invoke(VARIABLE.Key.GetData().GetKey());
                    }
                    
                }

                _isStart = false;

                CheckInitCompleted();
            }
        }

        void CheckInitCompleted()
        {
            if (_isStart == false) 
            {
                int targetCount = _buffer.Count;
                for (int i = 0; i < targetCount; i++)
                {
                    if (_buffer[i].Data.IsInit == true)
                    {
                        _buffer[i].Data.OnInit -= CheckInitCompleted;
                        OnInitElement?.Invoke(_buffer[i].Key.GetData().GetKey());
                        _buffer.RemoveAt(i);
                        i--;
                        targetCount--;
                    }
                }

                if (_buffer.Count == 0)
                {
                    InitCompleted();
                }
            }
        }
    }

    private void InitCompleted()
    {
        _isInit = true;
        OnInit?.Invoke();
    }
    
    public int GetCountAction()
    {
        return _dictionary.Count;
    }

    public bool EnableKeyInitData(MessengerBetweenSceneKeyDKO keyAction)
    {
        foreach (var VARIABLE in _list)
        {
            if (VARIABLE.Key.GetData().GetKey() == keyAction.GetKey())
            {
                return true;
            }
        }

        return false;
    }


    public DKOKeyAndTargetAction GetDKO(MessengerBetweenSceneKeyDKO key)
    {

        if (key == null)
        {
            Debug.LogError($"Внимание пришел null вместо ключа.");    
        }

        if (key.GetKey() == null)
        {
            Debug.LogError($"Внимание проблема с ключем, возвращаемым им ключ == Null");
        }
        
        return _dictionary[key.GetKey()];
    }

    public bool DKOIsAlready(MessengerBetweenSceneKeyDKO key)
    {
        return _dictionary.ContainsKey(key.GetKey());
    }
    
    public void AddDKO(MessengerBetweenSceneKeyDKO key,DKOKeyAndTargetAction DKO)
    {
        if (DKOIsAlready(key) == false)
        {
#if  UNITY_EDITOR
            _visibleData.Add(new AbsKeyData<string, DKOKeyAndTargetAction>(key.GetKey(), DKO));
#endif
            
            _dictionary.Add(key.GetKey(), DKO);
            OnInitElement?.Invoke(key.GetKey());
        }
    }

    public void RemoveDKO(MessengerBetweenSceneKeyDKO key)
    {
        if (DKOIsAlready(key) == true)
        {
#if  UNITY_EDITOR
            for (int i = 0; i < _visibleData.Count; i++)
            {
                if (_visibleData[i].Key == key.GetKey())
                {
                    _visibleData.RemoveAt(i);
                    break;
                }
            }
#endif
            
            _dictionary.Remove(key.GetKey());
        }
    }

    public List<AbsKeyData<MessengerBetweenSceneKeyDKO, DKOKeyAndTargetAction>> GetCopyData()
    {
        List<AbsKeyData<MessengerBetweenSceneKeyDKO, DKOKeyAndTargetAction>> copyData = new List<AbsKeyData<MessengerBetweenSceneKeyDKO, DKOKeyAndTargetAction>>();
        
        foreach (var VARIABLE in _dictionary.Keys)
        {
            var data = new AbsKeyData<MessengerBetweenSceneKeyDKO, DKOKeyAndTargetAction>(new MessengerBetweenSceneKeyDKO(VARIABLE), _dictionary[VARIABLE]);
            copyData.Add(data);
        }

        return copyData;
    }
    
    public void CopyDataTargetStorage(LogicMessengerDKOBetweenScenes storage)
    {
        var copyData = storage.GetCopyData();
        
        foreach (var VARIABLE in copyData)
        {
            if (DKOIsAlready(VARIABLE.Key) == false)
            {
                AddDKO(VARIABLE.Key, VARIABLE.Data);
            }
            else
            {
                Debug.LogError($"Ошибка при копир. данных с MBS DKO. Ключ |{VARIABLE.Key.GetKey()}| уже есть в словаре, пропускаю. ");
            }
        }
    }
}
