using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText; 
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    public TMP_InputField nameInputField;
    public bool isGameActive;
    
    private int score;
    private float spawnRate = 1f;
    private float sessionStartTime;
    private string selectedDifficulty;
    private bool uiInitialized = false; // A flag to make sure we only initialize the UI once

    void Start()
    {
        // We will now initialize the UI in Update to be safe
    }
    
    void Update()
    {
        // If the UI hasn't been set up yet AND the data is now loaded...
        if (!uiInitialized && DataManager.Instance != null && DataManager.Instance.IsDataLoaded)
        {
            // ...set it up.
            UpdateHighScoreDisplay();
            nameInputField.text = DataManager.Instance.playerName;
            
            // Set the flag so this doesn't run again
            uiInitialized = true;
        }
    }

    // --- The rest of the script is unchanged ---
    
    #region Unchanged Functions
    public void StartGame(int difficulty)
    {
        DataManager.Instance.playerName = nameInputField.text;
        DataManager.Instance.SavePlayerName();
        isGameActive = true;
        score = 0;
        spawnRate = 1f / difficulty;
        sessionStartTime = Time.time;
        if (difficulty == 1) selectedDifficulty = "Easy";
        else if (difficulty == 2) selectedDifficulty = "Medium";
        else selectedDifficulty = "Hard";
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
    }
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            if (DataManager.Instance.allTargets == null || DataManager.Instance.allTargets.Length == 0)
            {
                Debug.LogWarning("SpawnTarget Loop: DataManager has 0 targets. Skipping spawn.");
                continue;
            }
            int index = UnityEngine.Random.Range(0, DataManager.Instance.allTargets.Length);
            TargetData selectedTargetData = DataManager.Instance.allTargets[index];
            string path = "Prefabs/" + selectedTargetData.prefabPath;
            GameObject prefabToSpawn = Resources.Load<GameObject>(path);
            if (prefabToSpawn != null)
            {
                GameObject newTarget = Instantiate(prefabToSpawn);
                Target targetScript = newTarget.GetComponent<Target>();
                if (targetScript != null)
                {
                    targetScript.pointValue = selectedTargetData.pointValue;
                }
            }
            else
            {
                Debug.LogError("Prefab not found at path: " + path);
            }
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score.ToString();
    }
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        RecordSessionLog();
        CheckHighScore();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void RecordSessionLog()
    {
        PlayerLogEntry entry = new PlayerLogEntry();
        entry.sessionStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        entry.playerName = DataManager.Instance.playerName;
        entry.difficulty = selectedDifficulty;
        entry.playDuration = Time.time - sessionStartTime;
        entry.finalScore = score;
        DataManager.Instance.AddLogEntry(entry);
    }
    void CheckHighScore()
    {
        if (score > DataManager.Instance.bestScore)
        {
            DataManager.Instance.bestScore = score;
            DataManager.Instance.bestPlayerName = DataManager.Instance.playerName; 
            DataManager.Instance.SaveHighScore();
            UpdateHighScoreDisplay();
        }
    }
    void UpdateHighScoreDisplay()
    {
        highScoreText.text = $"High Score: {DataManager.Instance.bestPlayerName} : {DataManager.Instance.bestScore}";
    }
    #endregion
}