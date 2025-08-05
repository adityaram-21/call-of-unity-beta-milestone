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
    public GameObject pickingPanelS;
    public GameObject pickingPanelT;
    public GameObject pickingPanelI;
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
    public Vector3 tutorialStartPosition = new Vector3(-12.65f, -3.9f, 0f);
    public GameObject tutorialCanvas;
    private bool movementDone = false;

    public static GameManager Instance;

    public GameObject finalTutorialPopup;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    void Start()
    {
        Time.timeScale = 0f;
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

        GameObject player = GameObject.FindWithTag("Player");

        if (welcomeNextButton != null)
            welcomeNextButton.onClick.AddListener(ShowMovementPanel);

        if (player != null)
            player.transform.position = tutorialStartPosition;

        if (tutorialCanvas != null)
            tutorialCanvas.SetActive(true);

        if (codewordNextButton != null)
            codewordNextButton.onClick.AddListener(ShowMappingPanel);

        if (mappingButton != null)
            mappingButton.onClick.AddListener(ShowPickingPanel);

        startPanel.SetActive(false);
        gamePanel.SetActive(false);
        pausePanel.SetActive(false);

        LoadLevel(0);
    }

    void StartGame()
    {
        Debug.Log("Loading level 1...");
        Vector3 playerStartPosition = new Vector3(-7.5f, 4f, 0f);
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            player.transform.position = playerStartPosition;

        if (doorToRemove != null)
            doorToRemove.SetActive(true);

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
        welcomePanel?.SetActive(false);
        movementPanel?.SetActive(true);
        Debug.Log("Moved to Movement Tutorial Panel");
    }

    void Update()
    {
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

        if (spacebarPanel != null && spacebarPanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            spacebarPanel.SetActive(false);
            codewordPanel?.SetActive(true);
        }

        if (mappingPanel != null && mappingPanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            ShowPickingPanel();
        }
        else if (pickingPanelS != null && pickingPanelS.activeSelf && Input.GetMouseButtonDown(0))
        {
            pickingPanelS.SetActive(false);
            pickingPanelT.SetActive(true);
            Debug.Log("Moved to PickingT Tutorial Panel");
        }
        else if (pickingPanelT != null && pickingPanelT.activeSelf && Input.GetMouseButtonDown(0))
        {
            pickingPanelT.SetActive(false);
            pickingPanelI.SetActive(true);
            Debug.Log("Moved to PickingI Tutorial Panel");
        }
        else if (pickingPanelI != null && pickingPanelI.activeSelf && Input.GetMouseButtonDown(0))
        {
            pickingPanelI.SetActive(false);
            swappingPanel.SetActive(true);
            Debug.Log("Moved to Swapping Tutorial Panel");
        }
    }

    void ShowSpacebarPanel()
    {
        movementDone = true;
        movementPanel?.SetActive(false);
        spacebarPanel?.SetActive(true);
        Debug.Log("Moved to Spacebar Tutorial Panel");
    }

    void ShowMappingPanel()
    {
        codewordPanel?.SetActive(false);
        mappingPanel?.SetActive(true);
        Debug.Log("Moved to Mapping Tutorial Panel");
    }

    void ShowPickingPanel()
    {
        mappingPanel?.SetActive(false);
        pickingPanelS?.SetActive(true);
        Debug.Log("Moved to PickingS Tutorial Panel");
    }

    public void ShowFinalTutorialPopup()
{
    if (finalTutorialPopup != null)
    {
        finalTutorialPopup.SetActive(true);
    }
}

    void LoadLevel2()
    {
        Debug.Log("Loading level 2...");
        Vector3 playerStartPosition = doorToRemove.transform.position;
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            player.transform.position = playerStartPosition;

        doorToRemove?.SetActive(false);

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
                winText.text = currentLevel == 2 ? "Congratulations! You have completed the game!" : message;
            }

            if (currentLevel == 2)
            {
                pauseButton.gameObject.SetActive(false);
                Time.timeScale = 0f;
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
            Time.timeScale = 0f;
        }
    }

    public void LoadLevel(int level)
    {
        RandomLetterSpawner spawner = FindObjectOfType<RandomLetterSpawner>();
        LetterRack letterRack = FindObjectOfType<LetterRack>();
        MinimapCameraController minimapCameraController = FindObjectOfType<MinimapCameraController>();

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
                minimapCameraController.PositionAndSizeCamera(1);
            }
            else if (level == 2)
            {
                spawner.SpawnCluesForLevel2();
                minimapCameraController.PositionAndSizeCamera(2);
            }

            if (letterRack.wordManager != null)
            {
                if (level == 0)
                    letterRack.wordManager.SelectTutorialClue();
                else
                    letterRack.wordManager.SelectRandomClue();
            }

            Debug.Log($"Loading clues for level {level}...");
            letterRack.ClearRack();
            letterRack.SetupRack();

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
        Time.timeScale = 0f;
    }
}
