
[System.Serializable]
public class TaskInfo 
{
    private string _text;

    public TaskInfo(string text)
    {
        _text = text;
    }

    public string GetTextTask()
    {
        return _text;
    }
}
