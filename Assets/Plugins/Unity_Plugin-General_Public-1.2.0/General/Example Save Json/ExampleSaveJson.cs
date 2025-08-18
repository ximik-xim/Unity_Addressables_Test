using System;
using System.Collections.Generic;
using System.IO;
//using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class ExampleSaveJson : MonoBehaviour
{
    [SerializeField] 
    private ExampleClassDataSaveJson1 _exampleUserClass;

    [SerializeField] 
    private MonoBehaviour _exampleMonobeh;

    [SerializeField]
    private ScriptableObject _exampleScriptObj;

    
    private const string SAVE_USER_CLASS = "SaveUserClass";
    private const string SAVE_MONOBEH = "SaveMonobeh";
    private const string SAVE_SCRIPT_OBJ = "SaveScriptObj";
    
    private void OnValidate()
    {
        SaveUserClass();
        SaveMonobehaver();
        SaveScriptableObject();
    }

    private void SaveUserClass()
    {
        string path = Application.persistentDataPath + "/" + SAVE_USER_CLASS;

        Debug.Log("PathSaveUserClass = " + path);

        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }

        string textSave = "";
        // textSave = JsonConvert.SerializeObject(_exampleUserClass);
        // System.IO.File.WriteAllText(path + "/JsonConvert.json", textSave);
        
        //Не все данные сохраняет (к примеру у UnityEngine.Object в случае SO не сохраняет ссылку)
        textSave = JsonUtility.ToJson(_exampleUserClass);
        System.IO.File.WriteAllText(path + "/JsonUtility.json", textSave);
        
#if UNITY_EDITOR
        //Вообще все что можно сохранить, сохраняет
        textSave = EditorJsonUtility.ToJson(_exampleUserClass);
        System.IO.File.WriteAllText(path + "/EditorJsonUtility.json", textSave);
#endif
    }


    private void SaveMonobehaver()
    {
        string path = Application.persistentDataPath + "/" + SAVE_MONOBEH;

        Debug.Log("PathSaveMonobeh = " + path);
        
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }

        string textSave = "";
        // //Крч нормально не робит c MonoBehaviour и ScriptbleObject 
        // textSave = JsonConvert.SerializeObject(_exampleMonobeh);
        // System.IO.File.WriteAllText(path + "/JsonConvert.json", textSave);
        
        //Не все данные сохраняет (к примеру у UnityEngine.Object в случае SO не сохраняет ссылку)
        textSave = JsonUtility.ToJson(_exampleMonobeh);
        System.IO.File.WriteAllText(path + "/JsonUtility.json", textSave);

#if UNITY_EDITOR
        //Вообще все что можно сохранить, сохраняет
        textSave = EditorJsonUtility.ToJson(_exampleMonobeh);
        System.IO.File.WriteAllText(path + "/EditorJsonUtility.json", textSave);
#endif
    }

    private void SaveScriptableObject()
    {
        string path = Application.persistentDataPath + "/" + SAVE_SCRIPT_OBJ;

        Debug.Log("PathSaveSO = " + path);
        
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }

        string textSave = "";        
        // //Крч нормально не робит c MonoBehaviour и ScriptbleObject
        // textSave = JsonConvert.SerializeObject(_exampleScriptObj);
        // System.IO.File.WriteAllText(path + "/JsonConvert.json", textSave);
        
        //Не все данные сохраняет (к примеру у UnityEngine.Object в случае SO не сохраняет ссылку)
        textSave = JsonUtility.ToJson(_exampleScriptObj);
        System.IO.File.WriteAllText(path + "/JsonUtility.json", textSave);

#if UNITY_EDITOR        
        //Вообще все что можно сохранить, сохраняет
        textSave = EditorJsonUtility.ToJson(_exampleScriptObj);
        System.IO.File.WriteAllText(path + "/EditorJsonUtility.json", textSave);
#endif
    }
    
}

[Serializable]
class ExampleClassDataSaveJson1
{
    [SerializeField]
    private List<int> _listInt = new List<int>();

    [SerializeField] 
    private ExampleClassDataSaveJson2 _exampleClassJson2 = new ExampleClassDataSaveJson2();
    
    [SerializeField] 
    private MonoBehaviour exampleMonobeh;

    [SerializeField]
    private ScriptableObject exampleScriptObj;

}

[Serializable]
class ExampleClassDataSaveJson2
{
    [SerializeField]
    private int _intValue;

}