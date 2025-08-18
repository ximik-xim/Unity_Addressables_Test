using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleUseUIPanel : MonoBehaviour
{
    [SerializeField] 
    private GetUIGamePanelInfoStorageInStorage _getUIGamePanel;
    
    [SerializeField] 
    private SBI_SetAndRemoveTask _setAndRemoveTaskBlockOpen;
    [SerializeField] 
    private SBI_SetAndRemoveTask _setAndRemoveTaskBlockClose;
    private void Start()
    {
        //тут по хорошему еще нужна логика ожидания инициализации всех элементов, но для примера и так сойдет
        
        Debug.Log("Получение панели");
        var panel = _getUIGamePanel.GetPanel();
    
        Debug.Log("Добавление блокировки на окрытие панели");
        _setAndRemoveTaskBlockOpen.SetTask();

        if (panel.GetTaskDataOpen().IsThereTasks() == true)
        {
            Debug.Log("Открытие окна было заблокировано, ожидание пока сниметься блокировка");
            panel.GetTaskDataOpen().OnUpdateStatus += OnUpdateStatusOpen;
        }

        StartCoroutine(WaitRemoveBlockOpen());
    }
    
    IEnumerator WaitRemoveBlockOpen()
    {
        yield return new WaitForSeconds(1);
        
        Debug.Log("Удаляем Task на блокировку");
        _setAndRemoveTaskBlockOpen.RemoveTask();
    }


    private void OnUpdateStatusOpen()
    {
        var panel = _getUIGamePanel.GetPanel();
        Debug.Log("Включение панели");
        panel.Open();
        
        Debug.Log("Добавление блокировки на закрытие панели");
        _setAndRemoveTaskBlockClose.SetTask();

        if (panel.GetTaskDataClose().IsThereTasks() == true)
        {
            Debug.Log("Закрытие окна было заблокировано");
        }
        
        StartCoroutine(WaitClose());
    }
    
    IEnumerator WaitClose()
    {
        yield return new WaitForSeconds(2);
        
        var panel = _getUIGamePanel.GetPanel();
        
        Debug.Log("Закрываем панель игнорируя таски на блокировку");
        panel.CloseIgnoreTask();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
   
}
