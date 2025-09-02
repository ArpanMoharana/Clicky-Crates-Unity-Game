using System.Collections.Generic;

// This defines a single entry in our log
[System.Serializable]
public class PlayerLogEntry
{
    public string sessionStartTime;
    public string playerName; // NEW: Added a variable for the player's name
    public string difficulty;
    public float playDuration;
    public int finalScore;
}

// This is a wrapper class to hold a list of all our log entries
[System.Serializable]
public class PlayerLog
{
    public List<PlayerLogEntry> logEntries;

    public PlayerLog()
    {
        logEntries = new List<PlayerLogEntry>();
    }
}