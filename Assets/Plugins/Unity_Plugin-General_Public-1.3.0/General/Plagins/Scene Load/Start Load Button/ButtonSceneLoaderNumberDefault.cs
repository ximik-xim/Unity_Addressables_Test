using UnityEngine;
using UnityEngine.UI;

public class ButtonSceneLoaderNumberDefault : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField]
    private int _numberScene;

    [SerializeField]
    private AbsSceneLoader _sceneLoader;
    
    private void Awake()
    {
        if (_sceneLoader.IsInit == false)
        {
            _sceneLoader.OnInit += OnInitSceneLoader;
            return;
        }

        InitSceneLoader();
    }

    private void OnInitSceneLoader()
    {
        if (_sceneLoader.IsInit == true) 
        {
            _sceneLoader.OnInit -= OnInitSceneLoader;
            InitSceneLoader();
        }
        
    }
    
    private void InitSceneLoader()
    {
        InitData();
    }

    private void InitData()
    {
        _button.onClick.AddListener(ButtonClick);
    }
    
    public void SetNameScene(int sceneNumber)
    {
        _numberScene = sceneNumber;
    }

    private void ButtonClick()
    {
        _sceneLoader.LoadScene(_numberScene);
    }
    
    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ButtonClick);
    }
}
