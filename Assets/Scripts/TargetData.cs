[System.Serializable]
public class TargetData
{
    public string name;
    public int pointValue;
    public string prefabPath;
}

[System.Serializable]
public class TargetList
{
    public TargetData[] targets;
}