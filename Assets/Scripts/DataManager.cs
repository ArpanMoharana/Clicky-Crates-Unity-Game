using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string playerName;
    public string bestPlayerName;
    public int bestScore;
    public TargetData[] allTargets;
    
    public bool IsDataLoaded { get; private set; }
    
    private PlayerLog playerLog;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(LoadAllData());
    }

    IEnumerator LoadAllData()
    {
        Debug.Log("DataManager: Starting to load all data...");
        IsDataLoaded = false;
        
        yield return StartCoroutine(LoadTargetData());
        Debug.Log("DataManager: Finished loading TargetData.");

        yield return StartCoroutine(LoadPlayerLog());
        Debug.Log("DataManager: Finished loading PlayerLog.");

        LoadHighScore();
        LoadPlayerName();
        Debug.Log("DataManager: Finished loading PlayerPrefs data.");
        
        IsDataLoaded = true;
        Debug.Log("DataManager: All data loaded successfully. IsDataLoaded is now TRUE.");
    }
    
    IEnumerator LoadTargetData()
    {
        // THIS IS THE MODIFIED LINE: Added "file://" to the path
        string path = "file://" + Path.Combine(Application.streamingAssetsPath, "targets.json");
        UnityWebRequest request = UnityWebRequest.Get(path);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonString = request.downloadHandler.text;
            TargetList loadedData = JsonUtility.FromJson<TargetList>(jsonString);
            allTargets = loadedData.targets;
            Debug.Log($"Successfully loaded {allTargets.Length} targets from JSON.");
        }
        else
        {
            Debug.LogError("Failed to load targets.json: " + request.error);
        }
    }

    IEnumerator LoadPlayerLog()
    {
        string path = Path.Combine(Application.persistentDataPath, "playerlog.json");
        UnityWebRequest loadRequest = UnityWebRequest.Get(path);
        yield return loadRequest.SendWebRequest();
        if (loadRequest.result == UnityWebRequest.Result.Success && !string.IsNullOrEmpty(loadRequest.downloadHandler.text))
        {
            string json = loadRequest.downloadHandler.text;
            playerLog = JsonUtility.FromJson<PlayerLog>(json);
        }
        else
        {
            playerLog = new PlayerLog();
        }
    }
    
    public List<PlayerLogEntry> GetLogEntries()
    {
        if(playerLog == null)
        {
            playerLog = new PlayerLog();
        }
        return playerLog.logEntries;
    }
    
    #region Unchanged Functions
    public void SavePlayerName()
    {
        PlayerPrefs.SetString("LastPlayerName", playerName);
        PlayerPrefs.Save();
    }
    public void AddLogEntry(PlayerLogEntry newEntry)
    {
        playerLog.logEntries.Add(newEntry);
        SavePlayerLog();
    }
    private void SavePlayerLog()
    {
        string json = JsonUtility.ToJson(playerLog, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "playerlog.json"), json);
    }
    public void SaveHighScore()
    {
        PlayerPrefs.SetString("BestPlayerName", bestPlayerName);
        PlayerPrefs.SetInt("BestScore", bestScore);
        PlayerPrefs.Save(); 
    }
    public void LoadPlayerName()
    {
        playerName = PlayerPrefs.GetString("LastPlayerName", "Player");
    }
    public void LoadHighScore()
    {
        bestPlayerName = PlayerPrefs.GetString("BestPlayerName", "Player");
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }
    #endregion
}