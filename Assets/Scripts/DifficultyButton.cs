using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    public GameManager gameManager; // Make this public
    public int difficulty;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    void Update()
    {
        if (!button.interactable && DataManager.Instance != null && DataManager.Instance.IsDataLoaded)
        {
            button.interactable = true;
        }
    }

    void SetDifficulty()
    {
        // We find the GameManager here, right when it's needed.
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        gameManager.StartGame(difficulty);
    }
}