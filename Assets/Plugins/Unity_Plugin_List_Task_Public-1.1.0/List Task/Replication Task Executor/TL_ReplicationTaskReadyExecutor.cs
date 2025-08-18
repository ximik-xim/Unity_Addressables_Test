using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Данный скрипт проецирует поведение обработчика задач
/// В этой реализации следующая задача будет запущена не смотря на то, что прощлая задача еще не закончила свое выполнение
/// ------------------------------------------------------------------------------------------------------------------------------------------------
/// Главное преймущество использование этого класса, в том, что реализацию не сильно важна, то есть нет привязки к конкретной
/// абстракции у которой нужно вызвать конкретный метод и при этом еще и передать конкретные атрибуты
/// К примеру метод запуска может быть любым(и естественно атрибуты передоваймые в него, тоже могут быть любые)
/// Метод получение статуса задачи тоже может быть любым и каким угодно, глвное получить итоговый статус(true/false) о состаянии задачи
/// </summary>
[System.Serializable]
public class TL_ReplicationTaskReadyExecutor 
{
#if UNITY_EDITOR
    [SerializeField]
    private bool _debugLog = true;
#endif

    /// <summary>
    /// Собщает id задачи которую пора начинать выполнять
    /// </summary>
    public event Action<int> OnStartAction;
    
    /// <summary>
    /// Проверка выполнена ли задача по указанному id 
    /// </summary>
    public event Tl_DelegateCheckCompleted.CheckCompleted OnCheckCompleted;
    
    /// <summary>
    /// Сработает когда одна из задач будет выполнена(укажет id этой задачи) 
    /// </summary>
    public event Action<int> OnCompletedElement;
    
    /// <summary>
    /// Список id задач которые не выполнены
    /// </summary>
    [SerializeField] 
    private List<int> _listAction;

    /// <summary>
    /// Сообщает об успешном выполнении всех указанных задач
    /// </summary>
    public event Action OnCompleted;
    public bool IsCompleted => _isCompleted;
    private bool _isCompleted = true;

    private bool _isStart = false;
    /// <summary>
    /// Запускает логику обработки списка задач
    /// В списке указываю id тех задач, которые еще не выполнены(Completed == false)
    /// </summary>
    public void StartAction(List<int> listId)
    {
        if (_isCompleted == true)
        {
            _listAction = listId;
            _isCompleted = false;
            
            if (_listAction.Count > 0)
            {
                //Пока добавлю блокировку проверки через переменную, что бы старт успел у всех отработать до проверки
                _isStart = true;
                
                foreach (var VARIABLE in _listAction)
                {
                    OnStartAction.Invoke(VARIABLE);
                }

                _isStart = false;

                ActionCompleted();
            }
            else
            {
                Completed();
            }
        }
    }
    
    /// <summary>
    /// Вызывать когда любая задача из ранее указаного списка была выполнена
    /// </summary>
    public void ActionCompleted()
    {
        if (_isStart == false)
        {
            int targetCount = _listAction.Count;
            for (int i = 0; i < targetCount; i++)
            {
                //делаю запрос закончила ли выполнение задача, если да, то удаляю id из списка
                if (OnCheckCompleted.Invoke(_listAction[i]) == true)
                {
#if UNITY_EDITOR

                    if (_debugLog == true)
                    {
                        Debug.Log("Удаление завершенной задач по id = " + _listAction[i]);
                    }
#endif
                    OnCompletedElement?.Invoke(_listAction[i]);
                    _listAction.RemoveAt(i);
                    i--;
                    targetCount--;
                }
            }

            if (_listAction.Count == 0)
            {
                Completed();
            }
        }
    }

    private void Completed()
    {
        if (_isCompleted == false)
        {
            _isCompleted = true;
            OnCompleted?.Invoke();    
        }
    }
    
}
