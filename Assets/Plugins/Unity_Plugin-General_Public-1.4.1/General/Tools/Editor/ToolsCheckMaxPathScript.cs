using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Нужен что бы проверить максимальную длину пути до скрипта(с учетом имени скрипта)
/// </summary>
public class ToolsCheckMaxPathScript : MonoBehaviour
{
    private const int MAX_PATH_LENGTH = 260;
    private const int TRIGGER_WARMING = 20;

    [MenuItem("Tools/Полная проверка длины пути скриптов")]
    public static void CheckPathsAll()
    {
        string projectPath = Application.dataPath;
        string[] files = Directory.GetFiles(projectPath, "*.cs", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            int pathLength = file.Length;
            
            if (pathLength > MAX_PATH_LENGTH)
            {
                int exceededBy = pathLength - MAX_PATH_LENGTH;

                Debug.Log($"{file}\n" + $"Длина: {pathLength} (превышение длины на {exceededBy} символов)");
            }
            else if (pathLength + TRIGGER_WARMING > MAX_PATH_LENGTH) 
            {
                Debug.Log($"{file}\n" + $"Длина: {pathLength} (приближена к максимальной длине символов)");
            }
        }
    }
    
    [MenuItem("Tools/Проверка максимальной длины пути скриптов")]
    public static void CheckPathsMax()
    {
        string projectPath = Application.dataPath;
        string[] files = Directory.GetFiles(projectPath, "*.cs", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            int pathLength = file.Length;
            
            if (pathLength > MAX_PATH_LENGTH)
            {
                int exceededBy = pathLength - MAX_PATH_LENGTH;

                Debug.Log($"{file}\n" + $"Длина: {pathLength} (превышение длины на {exceededBy} символов)");
            }
        }
    }
    
    [MenuItem("Tools/Проверка приближение к максимальной длины пути скриптов")]
    public static void CheckPaths()
    {
        string projectPath = Application.dataPath;
        string[] files = Directory.GetFiles(projectPath, "*.cs", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            int pathLength = file.Length;
            
            if (pathLength + TRIGGER_WARMING > MAX_PATH_LENGTH) 
            {
                Debug.Log($"{file}\n" + $"Длина: {pathLength} (приближена к максимальной длине символов)");
            }
        }
    }
}

