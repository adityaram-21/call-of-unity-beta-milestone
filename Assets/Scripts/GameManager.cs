// using UnityEngine;
// using TMPro;
// using UnityEngine.UI;

// public class GameManager : MonoBehaviour
// {
//     [Header("UI Panels")]
//     public GameObject startPanel;
//     public GameObject pausePanel;
//     public GameObject gamePanel;

//     [Header("UI Buttons")]
//     public Button level1Button;
//     public Button level2Button;
//     public Button pauseButton;
//     public Button resumeButton;
//     public Button restartButton;

//     [Header("Popups")]
//     public GameObject losePopup;
//     public GameObject winPopup;

//     [Header("Game State")]
//     public int currentLevel = 1;

//     [Header("Objects")]
//     public GameObject doorToRemove;
//     public GameObject letterParent;

//     void Start()
//     {
//         Time.timeScale = 0f; // Game is paused at launch
//         startPanel.SetActive(true);
//         pausePanel.SetActive(false);
//         pauseButton.gameObject.SetActive(false);

//         level1Button.onClick.AddListener(StartGame);
//         level2Button.onClick.AddListener(LoadLevel2);
//         pauseButton.onClick.AddListener(PauseGame);
//         resumeButton.onClick.AddListener(ResumeGame);
//         restartButton.onClick.AddListener(RestartGame);
//     }

//     void StartGame()
//     {
//         Debug.Log("Loading level 1...");

//         Vector3 playerStartPosition = new Vector3(-7.5f, 4f, 0f);
//         GameObject player = GameObject.FindWithTag("Player");
//         if (player != null)
//         {
//             player.transform.position = playerStartPosition;
//         }

//         if (doorToRemove != null)
//         {
//             doorToRemove.SetActive(true);
//         }

//         startPanel.SetActive(false);
//         gamePanel.SetActive(true);
//         currentLevel = 1;
//         pauseButton.gameObject.SetActive(true);
//         restartButton.gameObject.SetActive(true);

//         Time.timeScale = 1f;
//         LoadLevel(1);
//     }

//     void LoadLevel2()
//     {
//         Debug.Log("Loading level 2...");

//         // Remove the door and move the player to the start position of level 2
//         Vector3 playerStartPosition = doorToRemove.transform.position;
//         GameObject player = GameObject.FindWithTag("Player");
//         if (player != null)
//         {
//             player.transform.position = playerStartPosition;
//         }

//         if (doorToRemove != null)
//         {
//             doorToRemove.SetActive(false);
//         }

//         Debug.Log("Door removed and player moved to level 2 start position.");

//         startPanel.SetActive(false);
//         gamePanel.SetActive(true);
//         pauseButton.gameObject.SetActive(true);
//         restartButton.gameObject.SetActive(true);

//         Time.timeScale = 1f;
//         LoadLevel(2);
//     }

//     void PauseGame()
//     {
//         pausePanel.SetActive(true);
//         Time.timeScale = 0f;
//     }

//     void ResumeGame()
//     {
//         pausePanel.SetActive(false);
//         Time.timeScale = 1f;
//     }

//     public void WinGame(string message)
//     {
//         Debug.Log(message);

//         if (winPopup != null)
//         {
//             TMP_Text winText = winPopup.GetComponentInChildren<TMP_Text>();
//             if (winText != null)
//             {
//                 winText.text = message;
//             }
//             if (currentLevel == 2)
//             {
//                 winText.text = "Congratulations! You have completed the game!";
//                 pauseButton.gameObject.SetActive(false);
//                 winPopup.SetActive(true);
//                 Time.timeScale = 0f; // freeze game on win
//             }
//             winPopup.SetActive(true);
//         }
//     }

//     public void GameOver(string message)
//     {
//         Debug.Log(message);

//         if (losePopup != null)
//         {
//             TMP_Text loseText = losePopup.GetComponentInChildren<TMP_Text>();
//             if (loseText != null)
//             {
//                 loseText.text = message;
//             }

//             gamePanel.SetActive(false);
//             pauseButton.gameObject.SetActive(false);
//             restartButton.gameObject.SetActive(false);
//             losePopup.SetActive(true);
//             Time.timeScale = 0f; // freeze game on loss
//         }
//     }

//     public void LoadLevel(int level)
//     {
//         RandomLetterSpawner spawner = FindObjectOfType<RandomLetterSpawner>();
//         LetterRack letterRack = FindObjectOfType<LetterRack>();
        
//         // Destroy existing letters in the scene
//         if (letterParent != null)
//         {
//             foreach (Transform child in letterParent.transform)
//             {
//                 Destroy(child.gameObject);
//             }
//         }
        
//         if (spawner != null && letterRack != null)
//         {
//             currentLevel = level;
//             if (gamePanel != null && !gamePanel.activeSelf)
//             {
//                 gamePanel.SetActive(true);
//             }

//             TimerController timerController = FindObjectOfType<TimerController>();
//             if (timerController != null)
//             {
//                 timerController.timerRunning = true;
//                 timerController.timeRemaining = level == 1 ? 180f : 300f;
//             }

//             if (level == 1)
//             {
//                 spawner.SpawnCluesForLevel1();
//             }
//             else if (level == 2)
//             {
//                 spawner.SpawnCluesForLevel2();
//             }

//             Debug.Log($"Loading clues for level {level}...");
//             letterRack.ClearRack(); // Clear the letter rack for the new level
//             letterRack.SetupRack(); // Setup the rack with new clues

//             PlayerController playerController = FindObjectOfType<PlayerController>();
//             if (playerController != null)
//             {
//                 playerController.ToggleFlashlight();
//             }
//         }
//     }

//     public void RestartGame()
//     {
//         Debug.Log("Restarting game...");
//         currentLevel = 1;
//         PlayerController playerController = FindObjectOfType<PlayerController>();
//         if (playerController != null)
//         {
//             playerController.ToggleFlashlight();
//             playerController.SetMaxBatteryLife();
//         }

//         gamePanel.SetActive(false);
//         pausePanel.SetActive(false);
//         losePopup.SetActive(false);
//         winPopup.SetActive(false);
//         startPanel.SetActive(true);

//         pauseButton.gameObject.SetActive(false);
//         restartButton.gameObject.SetActive(false);
//         Time.timeScale = 0f; // Pause the game
//     }
// }




using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject startPanel;
    public GameObject pausePanel;
    public GameObject gamePanel;
    public GameObject welcomePanel;
    public GameObject movementPanel;    
    public GameObject spacebarPanel;
    public GameObject codewordPanel;
    public GameObject mappingPanel;
    public GameObject pickingPanel;
    public GameObject swappingPanel;




    [Header("UI Buttons")]
    public Button level1Button;
    public Button level2Button;
    public Button tutorialButton;
    public Button pauseButton;
    public Button resumeButton;
    public Button restartButton;
    public Button welcomeNextButton;
    public Button codewordNextButton;
    public Button mappingButton;


    [Header("Popups")]
    public GameObject losePopup;
    public GameObject winPopup;

    [Header("Game State")]
    public int currentLevel = 1;

    [Header("Objects")]
    public GameObject doorToRemove;
    public GameObject letterParent;

    [Header("Tutorial")]
    public Vector3 tutorialStartPosition = new Vector3(-12.65f, -3.9f, 0f); // Adjust to your room's actual position
    public GameObject tutorialCanvas;
    private bool movementDone = false;

    
    

    void Start()
    {
        Time.timeScale = 0f; // Game is paused at launch
        startPanel.SetActive(true);
        pausePanel.SetActive(false);
        pauseButton.gameObject.SetActive(false);

        tutorialButton.onClick.AddListener(StartTutorial);
        level1Button.onClick.AddListener(StartGame);
        level2Button.onClick.AddListener(LoadLevel2);
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
    }

    void StartTutorial()
    {
        Debug.Log("Tutorial Button Clicked!");
        currentLevel = 0;
        Time.timeScale = 1f;

        // Move player to tutorial start position
        GameObject player = GameObject.FindWithTag("Player");

        if (welcomeNextButton != null)
        {
            welcomeNextButton.onClick.AddListener(ShowMovementPanel);
        }

        if (player != null)
        {
            player.transform.position = tutorialStartPosition;
        }

        // Show tutorial UI
        if (tutorialCanvas != null)
        {
            tutorialCanvas.SetActive(true);
        }

        if (codewordNextButton != null)
        {
            codewordNextButton.onClick.AddListener(ShowMappingPanel);
        }

        if (mappingButton != null)
        {
            mappingButton.onClick.AddListener(ShowPickingPanel);
        }

        

        // Hide the other UIs
        startPanel.SetActive(false);
        gamePanel.SetActive(false);
        pausePanel.SetActive(false);

        // Optionally call LoadLevel(0) if needed for clue logic
        LoadLevel(0);
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

    void ShowMovementPanel()
    {
        if (welcomePanel != null)
            welcomePanel.SetActive(false);

        if (movementPanel != null)
            movementPanel.SetActive(true);

        Debug.Log("Moved to Movement Tutorial Panel");
    }

    void Update()
    {
        // Movement → Spacebar
        if (!movementDone && movementPanel != null && movementPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
                Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                ShowSpacebarPanel();
            }
        }

        // Spacebar → Codeword
        if (spacebarPanel != null && spacebarPanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            spacebarPanel.SetActive(false);
            if (codewordPanel != null) codewordPanel.SetActive(true);
        }

        // Codeword → Mapping via button
        // Already handled by codewordNextButton.onClick → ShowMappingPanel()

        // Mapping → Picking on mouse click
        if (mappingPanel != null && mappingPanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            ShowPickingPanel();  // will hide mapping and show picking
        }

        // Picking → Swapping on mouse click
        else if (pickingPanel != null && pickingPanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            ShowSwappingPanel();  // will hide picking and show swapping
        }
    }


    void ShowSpacebarPanel()
    {
        movementDone = true;

        if (movementPanel != null)
            movementPanel.SetActive(false);

        if (spacebarPanel != null)
            spacebarPanel.SetActive(true);

        Debug.Log("Moved to Spacebar Tutorial Panel");
    }

    void ShowMappingPanel()
    {
        if (codewordPanel != null)
            codewordPanel.SetActive(false);

        if (mappingPanel != null)
            mappingPanel.SetActive(true);

        Debug.Log("Moved to Mapping Tutorial Panel");
    }

    void ShowPickingPanel()
    {
        if (mappingPanel != null) mappingPanel.SetActive(false);
        if (pickingPanel != null) pickingPanel.SetActive(true);
        Debug.Log("Moved to Picking Tutorial Panel");
    }

    void ShowSwappingPanel()
    {
        if (pickingPanel != null) pickingPanel.SetActive(false);
        if (swappingPanel != null) swappingPanel.SetActive(true);
        Debug.Log("Moved to Swapping Tutorial Panel");
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

            if (level == 0)
            {
                spawner.SpawnCluesForTutorial();
            }
            else if (level == 1)
            {
                spawner.SpawnCluesForLevel1();
            }
            else if (level == 2)
            {
                spawner.SpawnCluesForLevel2();
            }

            if (letterRack.wordManager != null)
{
    if (level == 0)
        letterRack.wordManager.SelectTutorialClue(); // Only if you have a fixed tutorial clue
    else
        letterRack.wordManager.SelectRandomClue();
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
        currentLevel = 1;
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.ToggleFlashlight();
            playerController.SetMaxBatteryLife();
        }

        gamePanel.SetActive(false);
        pausePanel.SetActive(false);
        losePopup.SetActive(false);
        winPopup.SetActive(false);
        startPanel.SetActive(true);

        pauseButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        Time.timeScale = 0f; // Pause the game
    }
}