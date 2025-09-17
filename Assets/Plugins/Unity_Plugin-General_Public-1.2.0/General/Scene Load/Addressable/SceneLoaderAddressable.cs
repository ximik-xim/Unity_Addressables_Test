using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

//#if 
public class SceneLoaderAddressable : AbsSceneLoader
{
    public override event Action OnInit;

    public override bool IsInit => true;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override void LoadScene(int numberScene)
    {
        Debug.LogError("Addressables не поддерживает загрузку по номеру сцены, только по её ключу");
    }
    
    public override void LoadScene(string keyNameScene)
    {
        Addressables.LoadSceneAsync(keyNameScene);
    }
}
//#endif