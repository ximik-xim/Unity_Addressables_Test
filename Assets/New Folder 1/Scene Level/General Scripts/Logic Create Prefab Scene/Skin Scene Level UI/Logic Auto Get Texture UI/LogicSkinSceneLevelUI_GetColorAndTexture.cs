using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Логика автоматического получения цвета и текстуру для обложки сцены
/// </summary>
public class LogicSkinSceneLevelUI_GetColorAndTexture : MonoBehaviour
{
    [SerializeField]
    private GetDKOPatch _getDkoPatch;

    [SerializeField]
    private AbsSkinSceneLevelUI _sceneUI;

    private StorageTextureAndColorSceneSkinSceneLevelUI _storageTexture;

    [SerializeField]
    private RawImage _texture;
    
    private void Awake()
    {
        if (_getDkoPatch.Init == false)
        {
            _getDkoPatch.OnInit += OnInitGetDkoPatchStorageSceneNumber;
        }
        
        if (_sceneUI.IsInit == false)
        {
            _sceneUI.OnInit += OnInitSceneUI;
        }
        
        CheckInit();
    }
    
    private void OnInitGetDkoPatchStorageSceneNumber()
    {
        if (_getDkoPatch.Init == true)
        {
            _getDkoPatch.OnInit -= OnInitGetDkoPatchStorageSceneNumber;
            CheckInit();
        }
    }
    
    private void OnInitSceneUI()
    {
        if (_sceneUI.IsInit == true)
        {
            _sceneUI.OnInit -= OnInitSceneUI;
            CheckInit();
        }
    }
   
    private void CheckInit()
    {
        if (_getDkoPatch.Init == true && _sceneUI.IsInit == true)  
        {
            _storageTexture = _getDkoPatch.GetDKO<DKODataInfoT<StorageTextureAndColorSceneSkinSceneLevelUI>>().Data;
            
            InitData();
        }
    }

    private void InitData()
    {
        GetTexture();
    }

    private void GetTexture()
    {
        //Потом тут другую логику можно написать
        _texture.texture = _storageTexture.GetTextureUI(_sceneUI.GetName()).Texture;
        _texture.color = _storageTexture.GetTextureUI(_sceneUI.GetName()).Color;
    }
}
