using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Логика сортировки порядкового номера сцен, с учетом исключений
/// </summary>
public class LogicSortingSceneLevel : MonoBehaviour
{
    public event Action OnInit;
    public bool IsInit => _isInit;
    private bool _isInit = false;
    
    //Список исключений. Нужен в случ. если опр. сцене, нужно задать опр. номер на сцене
    [SerializeField]
    private AbsExceptionsListInLogicSortingSceneLevel _listExceptions;

    private Dictionary<string, int> _exceptionsNumberScene = new Dictionary<string, int>();
    
    private void Awake()
    {
        if (_listExceptions.IsInit == false)
        {
            _listExceptions.OnInit += OnInitListExceptions;
        }

        CheckInit();
    }

    private void OnInitListExceptions()
    {
        if (_listExceptions.IsInit == true)
        {
            _listExceptions.OnInit -= OnInitListExceptions;
            CheckInit();
        }
    }

    private void CheckInit()
    {
        if (_listExceptions.IsInit == true)
        {
            foreach (var VARIABLE in _listExceptions.GetListExceptions())
            {
                _exceptionsNumberScene.Add(VARIABLE.Key.GetKey(),VARIABLE.Data);
            }

            _isInit = true;
            OnInit?.Invoke();
        }
    }

/// <summary>
/// Логика опр. порядкового номера сцен (и сразу сортировки сцен)
/// (да получилось вообще ужасно с точки зрения кол-ва проходов, но пока пусть так)
/// </summary>
    public List<KeyNameScene> SortingNumberScene(List<KeyNameScene> keyNameScenes, int startNumber = 0)
    {
        //Этот словарь сам потдерживает автомат. сортирует ключи по возрастанию,
        //и по этому в конце не надо будет делать лишний проход, для сортировки по порядку 
        SortedDictionary<int, KeyNameScene> buffID = new SortedDictionary<int, KeyNameScene>();

        //сначало заполняю все исключения(они главнее обычного счета)
        foreach (var Exception in _exceptionsNumberScene.Keys)
        {
            int maxCount = keyNameScenes.Count;
            for (int i = 0; i < maxCount; i++)
            {
                if (keyNameScenes[i].GetKey() == Exception)
                {
                    buffID.Add(_exceptionsNumberScene[Exception], keyNameScenes[i]);
                    keyNameScenes.RemoveAt(i);
                    i--;
                    maxCount--;
                }
            }
        }

        //затем заполняю обычные значения
        //(но т.к из за исключ. порядковый номер может быть нарушен,
        //то делаю смещение на 1 вперед, когда это происходит)
        for (int i = 0; i < keyNameScenes.Count; i++)
        {
            if (buffID.ContainsKey(startNumber) == true)
            {
                startNumber++;
                i--;
                continue;
            }
            else
            {
                buffID.Add(startNumber, keyNameScenes[i]);
                startNumber++;
            }
        }
        
        List<KeyNameScene> listSorting = new List<KeyNameScene>();

        foreach (var VARIABLE in buffID.Keys)
        {
            listSorting.Add(buffID[VARIABLE]);
        }

        return listSorting;
    }



}
