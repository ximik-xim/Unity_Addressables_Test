using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIGamePanelInfoStorage : MonoBehaviour
{

    [SerializeField] 
    private List<AbsKeyData<GetDataSO_KeyUIGamePanelInfo, UIGamePanelInfo>> _listInsertDataInspector;

    private Dictionary<string, UIGamePanelInfo> _dictionary = new Dictionary<string, UIGamePanelInfo>();

    private bool _init = false;
    public bool IsInit => _init;
    public event Action OnInit;

    public event Action OnUpdateData;
    
#if UNITY_EDITOR
   [SerializeField] 
   private bool _visibleData;
   [SerializeField] 
   private List<AbsKeyData<string, UIGamePanelInfo>> _listVisible = new List<AbsKeyData<string, UIGamePanelInfo>>();
#endif
   
    private void Awake()
       {
           foreach (var VARIABLE in _listInsertDataInspector)
           {
               _dictionary.Add(VARIABLE.Key.GetData().GetKey(), VARIABLE.Data);
               
   #if UNITY_EDITOR
               if (_visibleData == true)
               {
                   AddKeyVisible(VARIABLE.Key.GetData(), VARIABLE.Data);
               }
   #endif
               
           }
   
           _init = true;
           OnInit?.Invoke();
       }
       
       public bool PanelIsInsert(KeyUIGamePanelInfo key)
       {
           return _dictionary.ContainsKey(key.GetKey());
       }
       public UIGamePanelInfo GetPanel(KeyUIGamePanelInfo key)
       {
           return _dictionary[key.GetKey()];
       }
   
       public void AddPanel(KeyUIGamePanelInfo key, UIGamePanelInfo gameObject)
       {
           _dictionary.Add(key.GetKey(), gameObject);
           
   #if UNITY_EDITOR
           if (_visibleData == true)
           {
               AddKeyVisible(key,gameObject);
           }
   #endif
           OnUpdateData?.Invoke();
       }
   
       public void RemovePanel(KeyUIGamePanelInfo key)
       {
           _dictionary.Remove(key.GetKey());
           
   #if UNITY_EDITOR
           if (_visibleData == true)
           {
               RemoveKeyVisible(key);
           }
   #endif
           OnUpdateData?.Invoke();
       }
   
   #if UNITY_EDITOR
       private AbsKeyData<string, UIGamePanelInfo> IsKeyVisible(KeyUIGamePanelInfo key)
       {
           foreach (var VARIABLE in _listVisible)
           {
               if (VARIABLE.Key == key.GetKey())
               {
                   return VARIABLE;
               }     
           }
   
           return null;
       }
   
       private void AddKeyVisible(KeyUIGamePanelInfo key,UIGamePanelInfo panelInfo )
       {
           var data = IsKeyVisible(key);
           if (data == null)
           {
               var newData = new AbsKeyData<string, UIGamePanelInfo>(key.GetKey(), panelInfo);
               
               _listVisible.Add(newData);
           }
           
           
       }
   
       private void RemoveKeyVisible(KeyUIGamePanelInfo key)
       {
           var data = IsKeyVisible(key);
           if (data != null)
           {
               _listVisible.Remove(data);
           }
       }
   #endif
    
}
