using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Нужен что бы автоматически определить ключ сцены, при переходе на новую сцену
/// (через SceneManager.activeSceneChanged не получиться автоматезир. Они слишком поздно отраб, по этому сделал проверку при каждом запросе)
/// </summary>
public class AutoDetectedKeyNameScene : MonoBehaviour
{
    [SerializeField]
    private StorageNameSceneAndKeyNameScene _storageNameSceneAndKeyNameScene;

    [SerializeField]
    private KeyNameScene _keyNameScene;
    
    public event Action OnUpdateKeyScene;

    
    /// <summary>
    /// Сериализовал для тестов
    /// </summary>
    [SerializeField]
    private KeyNameScene _lastKeyNameScene;

    
    private void Awake()
    {       
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckKeyCurrentScene();
    }
    
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private KeyNameScene CheckKeyCurrentScene()
    {
        KeyNameScene keyNameScene;
        
        if (_storageNameSceneAndKeyNameScene.IsContainsNameScene(SceneManager.GetActiveScene().name) == true)
        {
            keyNameScene = _storageNameSceneAndKeyNameScene.GetSceneKey(SceneManager.GetActiveScene().name);
        }
        else
        {
            keyNameScene = null;
            Debug.Log($"ВНИМАНИЕ. Ключ для сцены {SceneManager.GetActiveScene().name} не был найден. Потенциальная ошибка");
        }

        if (_lastKeyNameScene != null)
        {
            _lastKeyNameScene = keyNameScene;
            OnUpdateKeyScene?.Invoke();
            
        }
        else if (keyNameScene != null)
        {
            _lastKeyNameScene = keyNameScene;
            OnUpdateKeyScene?.Invoke();
        }
        
        
        return keyNameScene;
    }
    

    public KeyNameScene GetKeyScene()
    {
        return CheckKeyCurrentScene();
    }
    
}
