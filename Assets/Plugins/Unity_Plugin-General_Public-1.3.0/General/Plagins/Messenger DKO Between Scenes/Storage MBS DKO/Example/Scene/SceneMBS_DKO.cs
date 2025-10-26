
/// <summary>
/// На конкретной сцене должен быть лиш 1 экземпляр.
/// Это хранилеще с DKO у конкретной сцены
/// </summary>
public class SceneMBS_DKO : LogicMessengerDKOBetweenScenes
{
    private void Awake()
    {
        LocalAwake();
    }
}
