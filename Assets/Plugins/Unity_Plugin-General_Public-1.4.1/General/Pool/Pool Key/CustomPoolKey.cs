using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class CustomPoolKey<Key, T> where T : IGetKey<Key>
{
    /// <summary>
    /// Эта функция будет вызвана, при запросе создания обьекта
    /// </summary>
    protected readonly Func<Key, T> OnCreateFunc;
    /// <summary>
    /// Этот event сработает при взятии обьекта из пула (не обязателен)
    /// </summary>
    protected readonly Action<Key, T> OnGetObject;
    /// <summary>
    /// Этот event сработает при возращении обьекта в пулл
    /// </summary>
    protected readonly Action<Key, T> OnReleaseObject;
    // /// <summary>
    // /// Этот event сработает при уничтожении экземпляра обьекта 
    // /// </summary>
    // protected readonly Action<Key, T> OnDestroyObject;
   
  

    public CustomPoolKey
    (
        Func<Key, T> createFunc,
        Action<Key, T> actionOnGet = null,
        Action<Key, T> actionOnRelease = null
        // Action<Key, T> actionOnDestroy = null,
    )
    {
        OnCreateFunc = createFunc;
        OnGetObject = actionOnGet;
        OnReleaseObject = actionOnRelease;
        // OnDestroyObject = actionOnDestroy;

#if UNITY_EDITOR
            _activeObjectInspector = new List<AbsKeyData<Key, T>>();
            _disactiveObjectInspector = new List<AbsKeyData<Key, T>>();
#endif


        _active = new Dictionary<Key, List<T>>();
        _disactive = new Dictionary<Key, List<T>>();
    }

#if UNITY_EDITOR
    
    /// <summary>
    /// Будет ли использоваться заполняймый список из инспектора
    /// </summary>
    public bool UseListInspector = false;
    
    [SerializeField]
    protected List<AbsKeyData<Key, T>> _activeObjectInspector = null;
    [SerializeField] 
    protected List<AbsKeyData<Key, T>> _disactiveObjectInspector = null;
#endif

    protected Dictionary<Key, List<T>> _active;
    protected Dictionary<Key, List<T>> _disactive;


    /// <summary>
    /// Вернет по ключу обьект из пула дизактивированных
    /// (если в пуле будет пусто, то автоматически создасть экземпляр обьекта,
    /// занесет обьеки в пулл активированных обьектов и в итоге вернет созданных экземпляр обьекта)
    /// </summary>
    public virtual T GetObject(Key key)
    {
        T obj = default;
        
        if (_disactive.ContainsKey(key) == false)
        {
            _disactive.Add(key, new List<T>());
        }

        if (_disactive[key].Count == 0)
        {
            obj = CreateObject(key);

            if (obj == null) 
            {
                Debug.LogError("Ошибка, обьект равен Null");
                return default;
            }
            
            if (_active.ContainsKey(key) == false)
            {
                _active.Add(key, new List<T>());
            }

            AddPoolDataActive(key, obj);

            return obj;
        }

        obj = _disactive[key][0];
        RemovePoolDataDisactive(key, obj);
        AddPoolDataActive(key, obj);

        var interf = obj as CustomEventInPool;
        if (interf != null)
        {
            interf.OnBeforeGetObject();
        }

        if (OnGetObject != null)
        {
            OnGetObject(key, obj);
        }

        if (interf != null)
        {
            interf.OnAfterGetObject();
        }


        return obj;
    }

    /// <summary>
    /// Просто создаст экземпляр обьекта по ключу и вернет его
    /// (не будет вносить не в какой пулл)
    /// </summary>
    protected virtual T CreateObject(Key key)
    {
        var obj = OnCreateFunc(key);

        var interf = obj as CustomEventInPoolReleaseThis<T>;
        if (interf != null)
        {
            interf.OnReleaseThis -= ReleaseObject;
            interf.OnReleaseThis += ReleaseObject;
        }

        return obj;
    }

    /// <summary>
    /// Вернет обьект в пулл по ключу
    /// (если обьект не был в пулле до этого, то добавит его)
    /// </summary>
    public virtual void ReleaseObject(T obj)
    {
        if (obj == null) 
        {
            Debug.LogError("Ошибка, обьект равен Null");
            return;
        }
        
        var key = obj.GetKey();

        if (_active[key].Contains(obj))
        {
            RemovePoolDataActive(key, obj);
        }

        AddPoolDataDisActive(key, obj);

        var interf = obj as CustomEventInPool;
        if (interf != null)
        {
            interf.OnBeforeReleaseObject();
        }

        if (OnReleaseObject != null)
        {
            OnReleaseObject(key, obj);
        }

        if (interf != null)
        {
            interf.OnAfterReleaseObject();
        }
    }

    /// <summary>
    /// добавит элемент по ключу в пулл
    /// ключ будет взят с обьекта
    /// </summary>
    public void AddPoolElement(T obj, bool isActive)
    {
        var interf = obj as CustomEventInPoolReleaseThis<T>;
        if (interf != null)
        {
            interf.OnReleaseThis -= ReleaseObject;
            interf.OnReleaseThis += ReleaseObject;
        }
        
        var key = obj.GetKey();
        
        if (_active.ContainsKey(key) == false)
        {
            _active.Add(key, new List<T>());
        }
        
        
        if (_disactive.ContainsKey(key) == false)
        {
            _disactive.Add(key, new List<T>());
        }

        if (isActive == true)
        {
            AddPoolDataActive(key, obj);
            return;
        }

        AddPoolDataDisActive(key, obj);
    }

    /// <summary>
    /// удалит элемент по ключу из пулла
    /// ключ будет взят с обьекта
    /// </summary>
    public void RemovePoolElement(T obj)
    {
        if (obj == null) 
        {
            Debug.LogError("Ошибка, обьект равен Null");
            return;
        }
        
        var key = obj.GetKey();

        if (_active.ContainsKey(key) == false) 
        {
            Debug.Log("Ошибка, такого ключа нету");
            return;
        }
        
        
        if (_disactive.ContainsKey(key) == false) 
        {
            Debug.Log("Ошибка, такого ключа нету");
            return;
        }

        if (_active[key].Contains(obj) == true) 
        {
            RemovePoolDataActive(key, obj);
        }
        
        
        if (_disactive[key].Contains(obj) == true) 
        {
            RemovePoolDataDisactive(key, obj);
        }
        
        var interf = obj as CustomEventInPoolReleaseThis<T>;
        if (interf != null)
        {
            interf.OnReleaseThis -= ReleaseObject;
        }
    }

    private void AddPoolDataActive(Key key, T data)
    {
        _active[key].Add(data);
#if UNITY_EDITOR
        if (UseListInspector == true)
        {
            _activeObjectInspector.Add(new AbsKeyData<Key, T>(key,data));
        }
#endif
    }

    private void RemovePoolDataActive(Key key, T data)
    {
        _active[key].Remove(data);
#if UNITY_EDITOR
        if (UseListInspector == true)
        {
            for (int i = 0; i < _activeObjectInspector.Count; i++)
            {
                if (_activeObjectInspector[i].Key.Equals(key) == true && _activeObjectInspector[i].Data.Equals(data) == true) 
                {
                    _activeObjectInspector.RemoveAt(i);
                    return;
                }
            }
        }
#endif
    }

    private void AddPoolDataDisActive(Key key, T data)
    {
        _disactive[key].Add(data);

#if UNITY_EDITOR
        if (UseListInspector == true)
        {
            _disactiveObjectInspector.Add(new AbsKeyData<Key, T>(key,data));
        }
#endif
    }

    
    private void RemovePoolDataDisactive(Key key, T data)
    {
        _disactive[key].Remove(data);

#if UNITY_EDITOR
        if (UseListInspector == true)
        {
            for (int i = 0; i < _disactiveObjectInspector.Count; i++)
            {
                if (_disactiveObjectInspector[i].Key.Equals(key) == true && _disactiveObjectInspector[i].Data.Equals(data) == true) 
                {
                    _disactiveObjectInspector.RemoveAt(i);
                    return;
                }
            }
        }
#endif
    }

    public List<AbsKeyData<Key, List<T>>> GetAllActiveElementPool()
    {
        List<AbsKeyData<Key, List<T>>> data = new List<AbsKeyData<Key, List<T>>>();
        foreach (var VARIABLE in _active.Keys)
        {
            data.Add(new AbsKeyData<Key, List<T>>(VARIABLE,new List<T>()));
            foreach (var VARIABLE2 in _active[VARIABLE])
            {
                data[data.Count - 1].Data.Add(VARIABLE2);
            }
        }

        return data;
    }
    
    public List<AbsKeyData<Key, List<T>>> GetAllDisactiveElementPool()
    {
        List<AbsKeyData<Key, List<T>>> data = new List<AbsKeyData<Key, List<T>>>();
        foreach (var VARIABLE in _disactive.Keys)
        {
            data.Add(new AbsKeyData<Key, List<T>>(VARIABLE,new List<T>()));
            foreach (var VARIABLE2 in _disactive[VARIABLE])
            {
                data[data.Count - 1].Data.Add(VARIABLE2);
            }
        }

        return data;
    }
    
    public List<T> GetAllActiveElementPool(Key key)
    {
        List<T> data = new List<T>();
        
        foreach (var VARIABLE2 in _active[key])
        {
            data.Add(VARIABLE2);
        } 

        return data;
    }
    
    public List<T> GetAllDisactiveElementPool(Key key)
    {
        List<T> data = new List<T>();
        
        foreach (var VARIABLE2 in _disactive[key])
        {
            data.Add(VARIABLE2);
        } 

        return data;
    }
    
    public List<Key> GetAllKeyElementPool()
    {
        List<Key> data = new List<Key>();
        
        foreach (var VARIABLE2 in _disactive.Keys)
        {
            data.Add(VARIABLE2);
        } 

        return data;
    }

    public void ActiveAllElement()
    {
        foreach (var VARIABLE in _disactive.Keys)
        {
            int targetId = _disactive[VARIABLE].Count;
            for (int i = 0; i < targetId ; i++)
            {
                
               var obj = _disactive[VARIABLE][0];
                RemovePoolDataDisactive(VARIABLE, obj);
                AddPoolDataActive(VARIABLE, obj);

                var interf = obj as CustomEventInPool;
                if (interf != null)
                {
                    interf.OnBeforeGetObject();
                }

                if (OnGetObject != null)
                {
                    OnGetObject(VARIABLE, obj);
                }

                if (interf != null)
                {
                    interf.OnAfterGetObject();
                }

                targetId--;
                i--;
            }
        }
    }
    
    
    public void DisactiveAllElement()
    {
        foreach (var VARIABLE in _active.Keys)
        {
            int targetId = _active[VARIABLE].Count;
            for (int i = 0; i < targetId ; i++)
            {
                var obj = _active[VARIABLE][0];
                
                RemovePoolDataActive(VARIABLE, obj);
                AddPoolDataDisActive(VARIABLE, obj);

                var interf = obj as CustomEventInPool;
                if (interf != null)
                {
                    interf.OnBeforeReleaseObject();
                }

                if (OnReleaseObject != null)
                {
                    OnReleaseObject(VARIABLE, obj);
                }

                if (interf != null)
                {
                    interf.OnAfterReleaseObject();
                }

                targetId--;
                i--;
            }
        }
    }
}
