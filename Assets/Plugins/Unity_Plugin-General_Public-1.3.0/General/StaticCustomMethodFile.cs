using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class StaticCustomMethodFile 
{

#if UNITY_EDITOR
    //Начинаться должен с Assets/..../..../
    //Пр Assets/Plagin
    //Создаст путь из фаилов
    public static void CreatPathUnity(string targetPatch)
    {
        string[] filesName = targetPatch.Split('/');
                                
        string patch = filesName[0];
        for (int i = 1; i < filesName.Length; i++)
        {
                                
            if (AssetDatabase.IsValidFolder(patch + "/" + filesName[i]) == false) 
            {
                AssetDatabase.CreateFolder(patch, filesName[i]);
                                
            }
            
            patch = patch + "/" + filesName[i];
        }
        
        AssetDatabase.SaveAssets();
    }

     
    //Проверяет наличие всего пути
    //Но работает только в пределах фаилов в Assets
    //Пример пути Assets/Plagin
    //Вернет true если путь существует
    //Не должен оканчиваться на /
    public static bool CheckPathUnity(string targetPatch)
    {
        return AssetDatabase.IsValidFolder(targetPatch);
    }

    
    public static List<UnityEngine.Object> LoadAllObjectPath(string[] targetPaths)
    {
        List<UnityEngine.Object> targetObjects = new List<UnityEngine.Object>();

        var objectGUID = AssetDatabase.FindAssets("", targetPaths);
        
        foreach (var asset in objectGUID)
        {
            string path = AssetDatabase.GUIDToAssetPath(asset);

            UnityEngine.Object targetObject = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);

            if (targetObject != null)
            {
                targetObjects.Add(targetObject);
            }

        }
        
        
        return targetObjects;
    }


    public static List<T> LoadAllObjectPath<T>(string[] targetPaths) where T : UnityEngine.Object
    {
        List<T> targetObjects = new List<T>();

        var objectGUID = AssetDatabase.FindAssets("", targetPaths);
        
        foreach (var asset in objectGUID)
        {
            string path = AssetDatabase.GUIDToAssetPath(asset);

            T targetObject = AssetDatabase.LoadAssetAtPath<T>(path);

            if (targetObject != null)
            {
                targetObjects.Add(targetObject);
            }

        }
        
        return targetObjects;
    }

    //Удалит все фаилы по указаному пути
    public static void DeletedAllFilesPath(string[] targetPaths)
    {
        foreach (var asset in AssetDatabase.FindAssets("", targetPaths))
        {
            var path = AssetDatabase.GUIDToAssetPath(asset);
            AssetDatabase.DeleteAsset(path);
        }

        AssetDatabase.SaveAssets();
    }
    
    //Вернет путь до обьекта
    public static string GetPathUnityObject(UnityEngine.Object obj)
    {
        return AssetDatabase.GetAssetPath(obj);
    }

    //Сохранит текущее состояние обьекта (особенно актуально для SO)
    public static void SaveStatusUnityObject(UnityEngine.Object obj)
    {
        EditorUtility.SetDirty(obj);
        AssetDatabase.SaveAssets();
    }
#endif
    
    
    //
    public static void CreatPathSystem(string targetPatch)
    {
        Directory.CreateDirectory(targetPatch);
    }

    //Проверяет только путь(папки) к фаилу, не нужно указывать имя и тип фаила
    //Пример пути Assets/Plagin
    //Вернет true если путь существует
    //Не должен оканчиваться на /
    public static bool CheckPathSystem(string targetPatch)
    {
        return Directory.Exists(targetPatch);
    }
    

    
    
    public static void CreatFileSystem(string targetPatch)
    {
        File.Create(targetPatch);
    }

    //Проверяет только наличие фаила
    //Пример пути Assets/Plagin/ExampleSO.asset
    //Вернет true фаил существует
    public static bool CheckFileSystem(string targetPatch)
    {
        return File.Exists(targetPatch);
    }

}

