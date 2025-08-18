using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Наглядный пример использование проекции логики выполнения задач
/// В этой реализации следующая задача будет запущена только после завершение выполнения предыдущей задачи
/// </summary>
public class TL_ExampleUseReplicationTaskExecutorSequence : TL_TaskReplicationWrapperMono
{
    [SerializeField]
    private TL_ReplicationTaskExecutorSequence _replicationTaskExecutorSequence;

    /// <summary>
    /// тут определяем список каких то задач(может быть любой класс)
    /// (любых, и в любом виде, главное что бы можно было узнать, выполнена ли она или нет, и вызвать выполнение)
    /// </summary>
    [SerializeField]
    private List<TL_ExampleReplicationTask> _listTask;
    
    public override event Action OnCompleted
    {
        add
        {
            _replicationTaskExecutorSequence.OnCompleted += value;
        }
        remove
        {
            _replicationTaskExecutorSequence.OnCompleted -= value;
        }
    }
    
    public override bool IsCompleted => _replicationTaskExecutorSequence.IsCompleted;
    
    /// <summary>
    /// Исключать ли из списка выполнение задачи
    /// (Нужен чисто для примера, если есть список с дохрена задачами
    /// которые уже выполняються и нужно выбрать те которые еще не начали выполение)
    /// </summary>
    [SerializeField] 
    private bool _exceptionCompletedLogic = false;


    private void Awake()
    {
        _replicationTaskExecutorSequence.OnCheckCompleted += OnCheckCompleted;
        _replicationTaskExecutorSequence.OnStartAction += OnStartAction;
        _replicationTaskExecutorSequence.OnCompleted += OnCompletedLogic;
        _replicationTaskExecutorSequence.OnCompletedElement += OnElementCompleted;
    }
    
    private void OnElementCompleted(int id)
    {
        _listTask[id].OnCompletedLogic -= OnCompletedElement;
    }

    public override void StartAction()
    {
        //тут ищю задачи которые еще не закончили выполнение, и сохраняю их Id из списка 
        List<int> id = new List<int>();
        if (_exceptionCompletedLogic == true)
        {
            for (int i = 0; i < _listTask.Count; i++)
            {
                if (_listTask[i].IsCompletedLogic == false)
                {
                    id.Add(i);
               
                    _listTask[i].OnCompletedLogic += OnCompletedElement;
                }
            }
        }
        else
        {
            for (int i = 0; i < _listTask.Count; i++)
            {
                id.Add(i);
               
                _listTask[i].OnCompletedLogic += OnCompletedElement;
            }
        }
        
        //Запускаю выполнение задачи
        _replicationTaskExecutorSequence.StartAction(id);
    }

   
    /// <summary>
    ///Когда задача выполниться, она сообщит об этом в логику по выполнению задач
    ///(при этом допустим тут в аргументы метода OnCompletedElement можно передать какие то еще параметры
    ///и на основе их выполнить ряд логики, до вызова метода ActionCompleted
    /// </summary>
    private void OnCompletedElement()
    {
        _replicationTaskExecutorSequence.ActionCompleted();
    }
    
    /// <summary>
    /// Тут, логика выполнение задач определяет какой будет вызван следующий элемент
    /// Опять же, к примеру для старта задачи нужны будут какие особые параметры и таким не хитрым способом
    /// можно передать в аргументы задачи любые нужные поля(ведь тут главное id задачи которое будет запущено следующее)
    /// </summary>
    private void OnStartAction(int id)
    {
        _listTask[id].StartLogic("запуск логики под id = " + id);
    }

    /// <summary>
    /// Тут логика задач проверяет какая из задач уже выполнена, а какие еще нет
    /// </summary>
    private bool OnCheckCompleted(int id)
    {
        Debug.Log("Проверка задачи на статус = " + _listTask[id].IsCompletedLogic + " по id = " + id);
        
        return _listTask[id].IsCompletedLogic;
    }
    
    /// <summary>
    /// сработает когда все задачи будут выполнены в логике задач
    /// </summary>
    private void OnCompletedLogic()
    {
       Debug.Log("Выполнение логики закончено");
    }

    private void OnDestroy()
    {
        _replicationTaskExecutorSequence.OnCheckCompleted -= OnCheckCompleted;
        _replicationTaskExecutorSequence.OnStartAction -= OnStartAction;
        _replicationTaskExecutorSequence.OnCompleted -= OnCompletedLogic;
        _replicationTaskExecutorSequence.OnCompletedElement -= OnElementCompleted;

    }
}
