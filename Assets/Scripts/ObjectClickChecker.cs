using UnityEngine;
using TMPro;
using System.Collections;

public class ObjectClickChecker : MonoBehaviour
{
    public GameObject winPopup;
    public GameObject losePopup;
    public GameObject doorToRemove;

    private WordDictionaryManager clueManager;

    void Start()
    {
        clueManager = FindObjectOfType<WordDictionaryManager>();
        if (clueManager == null)
        {
            Debug.LogError("WordDictionaryManager not found in scene!");
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
            StartCoroutine(HandleWin());
        }
        else
        {
            Debug.Log("Wrong object clicked. Game Over.");
            GameOver("Wrong target. Mission compromised!!");
        }
    }

    IEnumerator HandleWin()
    {
        if (winPopup != null)
        {
            winPopup.SetActive(true);
        }

        yield return new WaitForSeconds(3f);

        if (winPopup != null)
        {
            winPopup.SetActive(false);
        }

        if (doorToRemove != null)
        {
            Destroy(doorToRemove);
            Debug.Log("🚪 Door removed. Path is now open.");
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

            losePopup.SetActive(true);
        }

        Time.timeScale = 0f; // freeze game on loss
    }
}
