using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject pausePanel;
    public Button startButton;
    public Button pauseButton;
    public Button resumeButton;
    public GameObject losePopup;
    public GameObject winPopup;
    public int currentLevel = 1;

    void Start()
    {
        Time.timeScale = 0f; // Game is paused at launch
        startPanel.SetActive(true);
        pausePanel.SetActive(false);
        pauseButton.gameObject.SetActive(false);

        startButton.onClick.AddListener(StartGame);
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
    }

    void StartGame()
    {
        startPanel.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }

    void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void WinGame(string message)
    {
        Debug.Log(message);

        if (winPopup != null)
        {
            TMP_Text winText = winPopup.GetComponentInChildren<TMP_Text>();
            if (winText != null)
            {
                winText.text = message;
            }
            if (currentLevel == 2)
            {
                winText.text = "Congratulations! You have completed the game!";
                pauseButton.gameObject.SetActive(false);
                winPopup.SetActive(true);
                Time.timeScale = 0f; // freeze game on win
            }
            winPopup.SetActive(true);
        }
    }

    public void GameOver(string message)
    {
        Debug.Log(message);

        if (losePopup != null)
        {
            TMP_Text loseText = losePopup.GetComponentInChildren<TMP_Text>();
            if (loseText != null)
            {
                loseText.text = message;
            }

            pauseButton.gameObject.SetActive(false);
            losePopup.SetActive(true);
            Time.timeScale = 0f; // freeze game on loss
        }
    }

    public void LoadNextLevel()
    {
        Debug.Log("Loading next level...");
        RandomLetterSpawner spawner = FindObjectOfType<RandomLetterSpawner>();
        LetterRack letterRack = FindObjectOfType<LetterRack>();
        if (spawner != null && letterRack != null)
        {
            currentLevel++;
            spawner.SpawnCluesForLevel2(); // Call method to spawn clues for level
            letterRack.ClearRack(); // Clear the letter rack for the new level
            letterRack.SetupRack(); // Setup the rack with new clues
        }
    }
}