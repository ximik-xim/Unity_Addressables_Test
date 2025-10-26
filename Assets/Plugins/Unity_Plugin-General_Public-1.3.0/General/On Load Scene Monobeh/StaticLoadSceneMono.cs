using UnityEngine;

//Не готов. Не решена проблема с тем, что при запуске проекта в первый раз не отрабатывает
public  class StaticLoadSceneMono 
{
    public static void InvokeMethod()
    {
        
        Debug.Log("Use Invoke");
        var obj = GameObject.FindObjectsOfType<MonoBehaviour>();

        Debug.Log(obj.Length);
        foreach (var VARIABLE in obj)
        {

            var tar = VARIABLE as ILoadSceneMono;
            if (tar != null)
            {
                tar.LoadSceneMono();
            }
        }
    }
}
