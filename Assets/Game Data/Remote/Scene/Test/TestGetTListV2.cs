using System;
using UnityEngine;

public class TestGetTListV2 : MonoBehaviour
{
    [SerializeField]
    private GetDataSO_NameScene _dataSoNameScene;

    private void Awake()
    {
        Debug.Log("ПОЛУЧЕННЫЦ ИНДИФ 2 = " + _dataSoNameScene.GetSOIndif());
        Debug.Log("ПОЛУЧЕННЫЦ Данные 2 = " + _dataSoNameScene.GetData().GetKey());
    }
}
