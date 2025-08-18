using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(int numberScene)
    {
        SceneManager.LoadScene(numberScene);
    }
    
    public void LoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}
