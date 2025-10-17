using UnityEngine;

/// <summary>
/// находит тек. ключ сцены, и засовывает его в Task Load Scene
/// (для загрузки сцены через Task loader UI)
/// </summary>
public class LogicSetKeyCurrentSceneInLoadTargetSceneSetKeyAddressable : MonoBehaviour
{
    [SerializeField]
    private GetDKOPatch _dkoPatchAutoDetectedKeyNameScene;
 
    private AutoDetectedKeyNameScene _autoDetectedKeyNameScene;
    
    /// <summary>
    /// Логика загр. след сцены(не нужно ждать её иниц, как минимум до момента пока не уст её ключ)
    /// </summary>
    [SerializeField]
    private LoadTargetSceneSetKeyAddressable _loadTargetSceneSetKey;
    
    private void Awake()
    {
        if (_dkoPatchAutoDetectedKeyNameScene.Init == false)
        {
            _dkoPatchAutoDetectedKeyNameScene.OnInit += OnInitDkoPatchAutoDetectedKeyNameScene;
        }

        CheckInit();
    }
    
    private void OnInitDkoPatchAutoDetectedKeyNameScene()
    {
        if (_dkoPatchAutoDetectedKeyNameScene.Init == true)
        {
            _dkoPatchAutoDetectedKeyNameScene.OnInit -= OnInitDkoPatchAutoDetectedKeyNameScene;
            CheckInit();
        }
    }
   
    private void CheckInit()
    {
        if (_dkoPatchAutoDetectedKeyNameScene.Init == true) 
        {
            _autoDetectedKeyNameScene = _dkoPatchAutoDetectedKeyNameScene.GetDKO<DKODataInfoT<AutoDetectedKeyNameScene>>().Data;

            GetKeyScene();
        }
    }
    private void GetKeyScene()
    {
        var keyNameScene = _autoDetectedKeyNameScene.GetKeyScene();
        
        _loadTargetSceneSetKey.SetKeyScene(keyNameScene.GetKey());
    }
}
