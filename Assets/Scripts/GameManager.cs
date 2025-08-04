using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject startPanel;
    public GameObject pausePanel;
    public GameObject gamePanel;

    [Header("UI Buttons")]
    public Button level1Button;
    public Button level2Button;
    public Button pauseButton;
    public Button resumeButton;
    public Button restartButton;

    [Header("Popups")]
    public GameObject losePopup;
    public GameObject winPopup;

    [Header("Game State")]
    public int currentLevel = 1;

    [Header("Objects")]
    public GameObject doorToRemove;
    public GameObject letterParent;

    void Start()
    {
        Time.timeScale = 0f; // Game is paused at launch
        startPanel.SetActive(true);
        pausePanel.SetActive(false);
        pauseButton.gameObject.SetActive(false);

        level1Button.onClick.AddListener(StartGame);
        level2Button.onClick.AddListener(LoadLevel2);
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
    }

    void StartGame()
    {
        Debug.Log("Loading level 1...");

        Vector3 playerStartPosition = new Vector3(-7.5f, 4f, 0f);
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = playerStartPosition;
        }

        if (doorToRemove != null)
        {
            doorToRemove.SetActive(true);
        }

        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        currentLevel = 1;
        pauseButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

        Time.timeScale = 1f;
        LoadLevel(1);
    }

    void LoadLevel2()
    {
        Debug.Log("Loading level 2...");

        // Remove the door and move the player to the start position of level 2
        Vector3 playerStartPosition = doorToRemove.transform.position;
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = playerStartPosition;
        }

        if (doorToRemove != null)
        {
            doorToRemove.SetActive(false);
        }

        Debug.Log("Door removed and player moved to level 2 start position.");

        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        pauseButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

        Time.timeScale = 1f;
        LoadLevel(2);
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

            gamePanel.SetActive(false);
            pauseButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);
            losePopup.SetActive(true);
            Time.timeScale = 0f; // freeze game on loss
        }
    }

    public void LoadLevel(int level)
    {
        RandomLetterSpawner spawner = FindObjectOfType<RandomLetterSpawner>();
        LetterRack letterRack = FindObjectOfType<LetterRack>();
        
        // Destroy existing letters in the scene
        if (letterParent != null)
        {
            foreach (Transform child in letterParent.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        if (spawner != null && letterRack != null)
        {
            currentLevel = level;
            if (gamePanel != null && !gamePanel.activeSelf)
            {
                gamePanel.SetActive(true);
            }

            TimerController timerController = FindObjectOfType<TimerController>();
            if (timerController != null)
            {
                timerController.timerRunning = true;
                timerController.timeRemaining = level == 1 ? 180f : 300f;
            }

            if (level == 1)
            {
                spawner.SpawnCluesForLevel1();
            }
            else if (level == 2)
            {
                spawner.SpawnCluesForLevel2();
            }

            Debug.Log($"Loading clues for level {level}...");
            letterRack.ClearRack(); // Clear the letter rack for the new level
            letterRack.SetupRack(); // Setup the rack with new clues

            PlayerController playerController = FindObjectOfType<PlayerController>();
            if (playerController != null)
            {
                playerController.ToggleFlashlight();
            }
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restarting game...");
        
        Time.timeScale = 0f;
        // Completely reset everything, including all UI/script state
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        // just in case, for backup
        //currentLevel = 1;
        // PlayerController playerController = FindObjectOfType<PlayerController>();
        // if (playerController != null)
        //  {
        //     playerController.ToggleFlashlight();
        //    playerController.SetMaxBatteryLife();
        //  }

       // gamePanel.SetActive(false);
       // pausePanel.SetActive(false);
       // losePopup.SetActive(false);
       // winPopup.SetActive(false);
       // startPanel.SetActive(true);

       // pauseButton.gameObject.SetActive(false);
       // restartButton.gameObject.SetActive(false);
       // Time.timeScale = 0f; // Pause the game
    }
    
    
}