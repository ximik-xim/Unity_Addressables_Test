using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Нужен только для примера
/// для выбора какую из реализации проекций выполнения задач запустить
/// </summary>
public class TL_ExampleSelectUseReplicationTask : MonoBehaviour
{
    [SerializeField] 
    private TL_TaskReplicationWrapperMono _replicationTaskReadyExecutor;

    [SerializeField] 
    private TL_TaskReplicationWrapperMono _replicationTaskExecutorSequence;

    void OnGUI()
    {
        var buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 50;
        buttonStyle.normal.textColor = Color.white;
        buttonStyle.alignment = TextAnchor.MiddleCenter;

        int width = 0;
        int height = 0;
        int x = Screen.width;
        int y = Screen.height / 2;


        if (GUI.Button(new Rect(width, height, x, y), "По мере готовности", buttonStyle) == true)
        {
            _replicationTaskReadyExecutor.StartAction();
        }

        height = y;
        y = (Screen.height - height);

        if (GUI.Button(new Rect(width, height, x, y), "По очереди готовности", buttonStyle) == true)
        {
            _replicationTaskExecutorSequence.StartAction();
        }
    }
}
