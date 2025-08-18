using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

/// <summary>
/// Пулл обьектов
/// </summary>
[System.Serializable]
public class CustomPool<T>
{
    /// <summary>
    /// Эта функция будет вызвана, при запросе создания обьекта
    /// </summary>
    protected readonly Func<T> OnCreateFunc;
    /// <summary>
    /// Этот event сработает при взятии обьекта из пула (не обязателен)
    /// </summary>
    protected readonly Action<T> OnGetObject;
    /// <summary>
    /// Этот event сработает при возращении обьекта в пулл
    /// </summary>
    protected readonly Action<T> OnReleaseObject;
    // /// <summary>
    // /// Этот event сработает при уничтожении экземпляра обьекта 
    // /// </summary>
    // protected readonly Action<T> OnDestroyObject;
   
    
    public CustomPool
    (
        Func<T> createFunc,
        Action<T> actionOnGet = null,
        Action<T> actionOnRelease = null
        // Action<Key, T> actionOnDestroy = null,
    )
    {
        OnCreateFunc = createFunc;
        OnGetObject = actionOnGet;
        OnReleaseObject = actionOnRelease;
        // OnDestroyObject = actionOnDestroy;

#if UNITY_EDITOR
            _activeObjectInspector = new List<T>();
            _disactiveObjectInspector = new List<T>();
#endif


        _active = new List<T>();
        _disactive = new List<T>();
    }

#if UNITY_EDITOR
    
    /// <summary>
    /// Будет ли использоваться заполняймый список из инспектора
    /// </summary>
    public bool UseListInspector = false;
    
    [SerializeField]
    protected List<T> _activeObjectInspector = null;
    [SerializeField] 
    protected List<T> _disactiveObjectInspector = null;
#endif

    protected  List<T> _active;
    protected  List<T> _disactive;


    /// <summary>
    /// Вернет по ключу обьект из пула дизактивированных
    /// (если в пуле будет пусто, то автоматически создасть экземпляр обьекта,
    /// занесет обьеки в пулл активированных обьектов и в итоге вернет созданных экземпляр обьекта)
    /// </summary>
    public virtual T GetObject()
    {
        T obj = default;
        
        if (_disactive.Count == 0)
        {
            obj = CreateObject();

            if (obj == null) 
            {
                Debug.LogError("Ошибка, обьект равен Null");
                return default;
            }
            
            AddPoolDataActive(obj);

            return obj;
        }

        obj = _disactive[0];
        RemovePoolDataDisactive(obj);
        AddPoolDataActive(obj);

        var interf = obj as CustomEventInPool;
        if (interf != null)
        {
            interf.OnBeforeGetObject();
        }

        if (OnGetObject != null)
        {
            OnGetObject(obj);
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
    protected virtual T CreateObject()
    {
        var obj = OnCreateFunc();

        var interf = obj as CustomEventInPoolReleaseThis<T>;
        if (interf != null)
        {
            interf.OnReleaseThis -= ReleaseObject;
            interf.OnReleaseThis += ReleaseObject;
        }

        return obj;
    }

    /// <summary>
    /// Вернет обьект в пулл
    /// (если обьект не был в пулле до этого, то добавит его)
    /// </summary>
    public virtual void ReleaseObject(T obj)
    {
        if (obj == null) 
        {
            Debug.LogError("Ошибка, обьект равен Null");
            return;
        }
   
        if (_active.Contains(obj))
        {
            RemovePoolDataActive(obj);
        }

        AddPoolDataDisActive(obj);

        var interf = obj as CustomEventInPool;
        if (interf != null)
        {
            interf.OnBeforeReleaseObject();
        }

        if (OnReleaseObject != null)
        {
            OnReleaseObject(obj);
        }

        if (interf != null)
        {
            interf.OnAfterReleaseObject();
        }
    }

    /// <summary>
    /// добавит элемент в пулл
    /// </summary>
    public void AddPoolElement(T obj, bool isActive)
    {
        var interf = obj as CustomEventInPoolReleaseThis<T>;
        if (interf != null)
        {
            interf.OnReleaseThis -= ReleaseObject;
            interf.OnReleaseThis += ReleaseObject;
        }

        
        if (isActive == true)
        {
            AddPoolDataActive(obj);
            return;
        }

        AddPoolDataDisActive(obj);
    }

    /// <summary>
    /// удалит элемент из пулла
    /// ключ будет взят с обьекта
    /// </summary>
    public void RemovePoolElement(T obj)
    {
        if (obj == null) 
        {
            Debug.LogError("Ошибка, обьект равен Null");
            return;
        }

        if (_active.Contains(obj) == true) 
        {
            RemovePoolDataActive(obj);
        }
        
        
        if (_disactive.Contains(obj) == true) 
        {
            RemovePoolDataDisactive(obj);
        }
        
        
        var interf = obj as CustomEventInPoolReleaseThis<T>;
        if (interf != null)
        {
            interf.OnReleaseThis -= ReleaseObject;
        }
    }

    private void AddPoolDataActive(T data)
    {
        _active.Add(data);
#if UNITY_EDITOR
        if (UseListInspector == true)
        {
            _activeObjectInspector.Add(data);
        }
#endif
    }

    private void RemovePoolDataActive(T data)
    {
        _active.Remove(data);
#if UNITY_EDITOR
        if (UseListInspector == true)
        {
            for (int i = 0; i < _activeObjectInspector.Count; i++)
            {
                if (_activeObjectInspector[i].Equals(data) == true) 
                {
                    _activeObjectInspector.RemoveAt(i);
                    return;
                }
            }
        }
#endif
    }

    private void AddPoolDataDisActive( T data)
    {
        _disactive.Add(data);

#if UNITY_EDITOR
        if (UseListInspector == true)
        {
            _disactiveObjectInspector.Add(data);
        }
#endif
    }

    
    private void RemovePoolDataDisactive(T data)
    {
        _disactive.Remove(data);

#if UNITY_EDITOR
        if (UseListInspector == true)
        {
            for (int i = 0; i < _disactiveObjectInspector.Count; i++)
            {
                if (_disactiveObjectInspector[i].Equals(data) == true) 
                {
                    _disactiveObjectInspector.RemoveAt(i);
                    return;
                }
            }
        }
#endif
    }
    
}
