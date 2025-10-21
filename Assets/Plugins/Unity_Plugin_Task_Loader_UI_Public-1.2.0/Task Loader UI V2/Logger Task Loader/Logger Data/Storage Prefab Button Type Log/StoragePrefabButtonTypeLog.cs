using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Хранит по ключу(тип лога) префаб кнопки для переключения между типами логов 
/// </summary>
[CreateAssetMenu(menuName = "Task Loader UI/Storage Prefab Button Type Log")]
public class StoragePrefabButtonTypeLog : ScriptableObject, IInitScripObj
{
  public event Action OnInit;
    public bool IsInit => _isInit;
    private bool _isInit = false;

    [SerializeField]
    private List<AbsKeyData<GetDataSO_KeyTaskLoaderTypeLog, ButtonTypeLog>> _listPrefab = new List<AbsKeyData<GetDataSO_KeyTaskLoaderTypeLog, ButtonTypeLog>>();

    private Dictionary<string, ButtonTypeLog> _data = new Dictionary<string, ButtonTypeLog>();
    
        public void InitScripObj()
        {
#if UNITY_EDITOR
        
            EditorApplication.playModeStateChanged -= OnUpdateStatusPlayMode;
            EditorApplication.playModeStateChanged += OnUpdateStatusPlayMode;

            //На случай если event playModeStateChanged не отработает при входе в режим PlayModeStateChange.EnteredPlayMode (такое может быть, и как минимум по этому нужна пер. bool _init)
            if (EditorApplication.isPlaying == true)
            {
                if (_isInit == false)
                {
                    Awake();
                }
            }
            else
            {
                //Нужен, что бы сбросить переменную при запуске проекта(т.к при выходе(закрытии) из проекта, переменная не факт что будет сброшена)
                _isInit = false;
            }
#endif
        }
        
#if UNITY_EDITOR
        private void OnUpdateStatusPlayMode(PlayModeStateChange obj)
        {
            //При выходе из Play Mode произвожу очистку данных(тем самым эмулирую что при след. запуске(вхождение в Play Mode) данные будут обнулены)
            if (obj == PlayModeStateChange.ExitingPlayMode)
            {
                if (_isInit == true)
                {
                    _isInit = false;
                }
            }
        
            // При запуске игры эмулирую иниц. SO(По идеи не совсем верно, т.к Awake должен произойти немного раньше, но пофиг)(как показала практика метод может не сработать)
            if (obj == PlayModeStateChange.EnteredPlayMode)
            {
                if (_isInit == false)
                {
                    Awake();
                }
            
            }
        }
#endif
        
    private void Awake()
    {
        if (_isInit == false)
        {
            _data.Clear();

             foreach (var VARIABLE in _listPrefab)
             {
                 _data.Add(VARIABLE.Key.GetData().GetKey(), VARIABLE.Data);
             }
            
            _isInit = true;
            OnInit?.Invoke();
        }
        
    }
        
    public ButtonTypeLog GetPrefabButton(KeyTaskLoaderTypeLog keyTypeLog)
    {
        return _data[keyTypeLog.GetKey()];
    }

    private void OnDestroy()
    {
#if UNITY_EDITOR
        EditorApplication.playModeStateChanged -= OnUpdateStatusPlayMode;
#endif
    }
}
