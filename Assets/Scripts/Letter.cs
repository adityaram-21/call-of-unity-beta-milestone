using UnityEngine;

public class Letter : MonoBehaviour
{
    public char letterValue;
    public TextMesh letterText;

    private Vector3 originalPosition;
    public bool isCollected = false;
    private GameManager gameManager;

    [SerializeField] private float collectRange = 10f; // Max click distance

    private void Start()
    {
        originalPosition = transform.position;
        UpdateDisplay();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SetLetter(char c)
    {
        letterValue = c;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (letterText != null)
            letterText.text = letterValue.ToString();
    }

    private void OnMouseDown()
    {
        if (isCollected) return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= collectRange)
        {
            LetterRack rack = FindObjectOfType<LetterRack>();
            if (rack != null && rack.AddCollectedLetter(letterValue, this))
            {
                if (gameManager != null && gameManager.letterAccuracyTracker != null)
                {
                    if (rack.IsCorrectLetter(letterValue))  // Ensure IsCorrectLetter exists in LetterRack
                        gameManager.letterAccuracyTracker.AddCorrect();
                    else
                        gameManager.letterAccuracyTracker.AddIncorrect();
                }
                Collect();
            }
        }
        else
        {
            Debug.Log("Too far to collect this letter.");
        }
    }

    public void Collect()
    {
        isCollected = true;
        gameObject.SetActive(false);
    }

    public void ReturnToWorld()
    {
        isCollected = false;
        transform.position = originalPosition;
        gameObject.SetActive(true);
    }
}
