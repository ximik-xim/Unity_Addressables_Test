
/// <summary>
/// Всеобщее хранилеще с DKO(проходит между всеми сценами)
/// Должен быть один на сцене(аля синглтон)
/// И его будут искать все остальные скрипты для взаимодействия и полечение дальнейших DKO
/// </summary>
public class DontDestroyMBS_DKO : LogicMessengerDKOBetweenScenes
{
    private void Awake()
    {
        LocalAwake();
    }
}
