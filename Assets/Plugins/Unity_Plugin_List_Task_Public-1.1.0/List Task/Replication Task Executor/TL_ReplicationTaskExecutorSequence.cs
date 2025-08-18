using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Данный скрипт проецирует поведение обработчика задач
/// В этой реализации следующая задача будет запущена только после завершение выполнения предыдущей задачи
/// ------------------------------------------------------------------------------------------------------------------------------------------------
/// Главное преймущество использование этого класса, в том, что реализацию не сильно важна, то есть нет привязки к конкретной
/// абстракции у которой нужно вызвать конкретный метод и при этом еще и передать конкретные атрибуты
/// К примеру метод запуска может быть любым(и естественно атрибуты передоваймые в него, тоже могут быть любые)
/// Метод получение статуса задачи тоже может быть любым и каким угодно, глвное получить итоговый статус(true/false) о состаянии задачи
/// </summary>
[System.Serializable]
public class TL_ReplicationTaskExecutorSequence
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

    private int _targetId = 0;
    
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

            StartLogic();
        }
    }

    private void StartLogic()
    {
        if (_listAction.Count > 0)
        {
            int targetCount = _listAction.Count;
            for (int i = 0; i < _listAction.Count; i++)
            {
                // тут из за принцыпа работы нету проблем не с тем что буффер не успеет заполниться
                // ни с тем что статус не успеет сброситься у всех элементов, т.к проверка идет поочереди
                OnStartAction.Invoke(_listAction[i]);

                if (OnCheckCompleted.Invoke(_listAction[i]) == false)
                {
#if UNITY_EDITOR

                    if (_debugLog == true)
                    {
                        Debug.Log("Ожидание завершение задач по id = " + _listAction[i]);
                    }
#endif
                    _targetId = i;
                    return;
                }
                else
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
        }
        else
        {
            Completed();
        }
    }

    /// <summary>
    /// Вызывать когда прийдет ответ от задачи
    /// </summary>
    public void ActionCompleted()
    {
        if (OnCheckCompleted.Invoke(_listAction[_targetId]) == true)
        {
#if UNITY_EDITOR

            if (_debugLog == true)
            {
                Debug.Log("Удаление завершенной задач по id = " + _listAction[_targetId]);
            }
#endif
            OnCompletedElement?.Invoke(_listAction[_targetId]);
            _listAction.RemoveAt(_targetId);

            StartLogic();
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
