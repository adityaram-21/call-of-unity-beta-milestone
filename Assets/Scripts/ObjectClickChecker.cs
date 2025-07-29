using UnityEngine;
using TMPro;
using System.Collections;

public class ObjectClickChecker : MonoBehaviour
{
    public GameObject winPopup;
    public GameObject losePopup;
    public GameObject doorToRemove;

    private WordDictionaryManager clueManager;
    private GameManager gameManager;
    private int timeTakenLevel1 = 0;
    private int timeTakenLevel2 = 0;

    void Start()
    {
        clueManager = FindObjectOfType<WordDictionaryManager>();
        gameManager = FindObjectOfType<GameManager>();

        if (clueManager == null || gameManager == null)
        {
            Debug.LogError("ClueManager or GameManager not found in scene!");
            return;
        }

        if (winPopup != null) winPopup.SetActive(false);
        if (losePopup != null) losePopup.SetActive(false);
    }

    void OnMouseDown()
    {
        if (!LetterRack.clueSolved)
        {
            Debug.Log("You must solve the clue first before clicking objects.");
            return;
        }

        if (clueManager == null || string.IsNullOrEmpty(clueManager.targetObjectName))
        {
            Debug.LogWarning("ClueManager or target object not set.");
            return;
        }

        string clickedName = gameObject.name.Trim().ToLower();
        string targetName = clueManager.targetObjectName.Trim().ToLower();

        Debug.Log($"Clicked Object: {clickedName}, Target Object: {targetName}");

        if (clickedName == targetName)
        {
            Debug.Log("Correct object clicked! You win.");
            TimerController timerController = FindObjectOfType<TimerController>();
            if (gameManager != null && gameManager.currentLevel == 1)
            {
                timeTakenLevel1 = (int)(300f - timerController.timeRemaining);
                GoogleSheetLogger.LogEvent("Level 1 Completed", $"{timeTakenLevel1}");
            }
            else
            if (gameManager != null && gameManager.currentLevel == 2)
            {
                timeTakenLevel2 = (int)(300f - timerController.timeRemaining - timeTakenLevel1);
                GoogleSheetLogger.LogEvent("Level 2 Completed", $"{timeTakenLevel2}");
            }
            StartCoroutine(HandleWin());
        }
        else
        {
            Debug.Log("Wrong object clicked. Game Over.");
            gameManager.GameOver("Wrong target. Mission compromised!!");
        }
    }

    IEnumerator HandleWin()
    {
        if (gameManager != null)
        {
            gameManager.WinGame("Yayyyieeee! You have picked the correct object. The doors are now open.");
        }

        yield return new WaitForSeconds(3f);

        if (winPopup != null)
        {
            winPopup.SetActive(false);
        }

        if (doorToRemove != null)
        {
            Destroy(doorToRemove);
            Debug.Log("Door removed. Path is now open.");
            gameManager.LoadNextLevel();
        }
    }
}
