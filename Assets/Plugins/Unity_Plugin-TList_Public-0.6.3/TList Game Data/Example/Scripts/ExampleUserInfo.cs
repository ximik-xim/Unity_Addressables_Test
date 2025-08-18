using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class ExampleUserInfo
{

    [SerializeField]
    private string city;
    public string City => city;
    
    [SerializeField]
    private int age;
    public int Age => age;
}
