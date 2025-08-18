 using System;
using UnityEngine;
using UnityEngine.SceneManagement;

 /// <summary>
 /// Этот скрипт, при переходе на другую сцену будет искать у новой сцены хранилеще с DKO
 /// (по идеи) должен быть один на сцене и Dont Destroy, и когда перейдем на другую сцену, то через него получим хранилеще DKO этой сцены 
 /// </summary>
public class FindMBS_DKO_Scene : MonoBehaviour
{
    private SceneMBS_DKO _sceneMbsDko;
    public SceneMBS_DKO GetSceneMbsDko => _sceneMbsDko;
    
    private bool _init = false;
    public bool Init => _init;
    public event Action OnInit;
    private void Awake()
    {
        SceneManager.sceneUnloaded += FindMbsDkoScene;
    }

    private void FindMbsDkoScene(Scene arg0)
    {
        _init = false;
        
        _sceneMbsDko = FindObjectOfType<SceneMBS_DKO>();
        if (_sceneMbsDko != null)
        {
            if (_sceneMbsDko.IsInit == false)
            {
                _sceneMbsDko.OnInit += InitSceneMbsDko;
                return;
            }
            
        }
        
        _init = true;
        OnInit?.Invoke();
    }

    private void InitSceneMbsDko()
    {
        _sceneMbsDko.OnInit -= InitSceneMbsDko;
        
        _init = true;
        OnInit?.Invoke();
    }
}
