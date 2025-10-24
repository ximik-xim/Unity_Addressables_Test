using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Дефолтная загрузка сцены(если она есть при сборки проекта)
/// </summary>
public class SceneLoaderDefault : AbsSceneLoader
{
    public override event Action OnInit;

    public override bool IsInit => true;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override void LoadScene(int numberScene)
    {
        SceneManager.LoadScene(numberScene);
    }
    
    public override void LoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}
