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
}