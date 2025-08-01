using UnityEngine;
using TMPro;
public class TimerController : MonoBehaviour
{
    public float timeRemaining = 300f; // 5 minutes
    public bool timerRunning = false;
    public TextMeshProUGUI timerText;
    
    void Start()
    {
        UpdateTimerUI(timeRemaining);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI(timeRemaining);
            }
            else
            {
                timerRunning = false;
                timeRemaining = 0;
                TimerEnded();
            }
        }
    }

     void UpdateTimerUI(float time)
    {
        time = Mathf.Max(time, 0);
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerEnded()
    {
        // Handle timer end logic here
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.GameOver("Time's up! Mission failed.");
        }
        else
        {
            Debug.LogWarning("GameManager not found in scene!");
        }
    }
}
