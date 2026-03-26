using UnityEditor.SceneManagement;

public class EditorStaticLoadSceneMono 
{

    // [InitializeOnLoadMethod]
    // private static void InvokeV1()
    // {
    //     InvokeMethod();
    // }

    // [InitializeOnLoadMethod]
    private static void InvokeV2()
    {
        //Таааааак, этот event отрабатывает только в режиме PlayMode
        //А мне блять, прямо наоборот нужно не в плей моде получить данные об переходе на другую сцену
        // SceneManager.sceneUnloaded -= arg0 =>  InvokeMethod();
        //  SceneManager.sceneUnloaded += arg0 =>  InvokeMethod();
        // SceneManager.sceneLoaded += (arg0, mode) =>  InvokeMethod();
        // SceneManager.activeSceneChanged += (arg0, scene) =>  InvokeMethod();

        
        
        //А этот класс то что нужно, куча нужный метод для отслеживания состояния перехода на сцену и т.д
        // Сработает после загрузки сцены( но вот проблема, 1 сцена с которой происходит загрузка при запуске билда, не отрабатывает этот метод)
        // EditorSceneManager.sceneOpened -= (scene, mode) =>  InvokeMethod();
        // EditorSceneManager.sceneOpened += (scene, mode) =>  InvokeMethod();

        //не срабатывает при загрузке билда
        // EditorSceneManager.sceneLoaded -= (arg0, mode) =>  InvokeMethod();
        // EditorSceneManager.sceneLoaded += (arg0, mode) =>  InvokeMethod();
        //не срабатывает при загрузке билда
        // EditorSceneManager.sceneOpening -= (path, mode) =>  InvokeMethod();
        // EditorSceneManager.sceneOpening += (path, mode) =>  InvokeMethod();
        
        //А этот event срабатывает при загрузке 1 сцены(Нужно подумать как это совместит, и надо ли оно)
        //Ведь OnValidate тоже отрабатывает при загрузке сцена
        EditorSceneManager.activeSceneChangedInEditMode -= (arg0, scene) => StaticLoadSceneMono.InvokeMethod();
        EditorSceneManager.activeSceneChangedInEditMode += (arg0, scene) => StaticLoadSceneMono.InvokeMethod();
        
    }
}
