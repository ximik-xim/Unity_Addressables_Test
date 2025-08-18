using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleGetUserInfo : MonoBehaviour
{
    [SerializeField] 
    private ExampleUserStorage _userStorage;

    [SerializeField] 
    private GetDataSO_TList_Example _key;
    
    private void Awake()
    {
        if (_userStorage.Init == false)
        {
            _userStorage.OnInit += OnInitStorage;
            return;
        }

        InitStorage();

    }

    private void OnInitStorage()
    {
        _userStorage.OnInit -= OnInitStorage;
        InitStorage();
    }
    
    private void InitStorage()
    {
        if (_userStorage.DataIsInsert(_key.GetData()) == true)
        {
            var userInfo = _userStorage.GetUserInfo(_key.GetData());
            
            Debug.Log("Получены следующие данные");
            Debug.Log("City = " + userInfo.City);
            Debug.Log("Age = " + userInfo.Age);
            
            return;
        }
        
        Debug.Log("По указанному ключу данные не были найдены");
    }
}
