using UnityEngine;
[CreateAssetMenu(menuName = "ExampleSaveJSON/ScriptObjData")]

public class ExampleSaveJsonScriptObj : ScriptableObject
{
    [SerializeField] 
    private ExampleClassDataSaveJson1 _exampleUserClass;
}
