
using UnityEditor;
using UnityEngine;


public class InitLogicScriptObj 
{
    //Срабатывает при переходе в Play Mode, при изменении кода в скрипте(и любом измен. проекта). А так же при запуске проекта
    //[InitializeOnLoadMethod]
    private static void InitOnLoadSO()
    {
        string[] scriptObjGUID = AssetDatabase.FindAssets("t:" + typeof(ScriptableObject).Name);

        foreach (var ObjGUID in scriptObjGUID)
        {
            var pathSO = AssetDatabase.GUIDToAssetPath(ObjGUID);
            var scriptObj = AssetDatabase.LoadAssetAtPath<ScriptableObject>(pathSO);

            var interf = scriptObj as IInitScripObj;
            if (interf != null)
            {
                interf.InitScripObj();
            }
        }
    }

    [InitializeOnLoadMethod]
    private static void InitLogic()
    {
        EditorApplication.update += CustomUpdate;
    }

    private static void CustomUpdate()
    {
        if (EditorApplication.isCompiling == false)
        {
            EditorApplication.update -= CustomUpdate;
            InitOnLoadSO();
        }
    }
}
