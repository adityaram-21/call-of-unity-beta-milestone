using UnityEngine;
using TMPro;

public class PlayerLightDetector : MonoBehaviour
{
    public GameObject gameOverPopup;  // Assign in Inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyLight"))  // Light should be tagged properly
        {
            Debug.Log("🚨 Player entered the red light. Game Over!");

            if (gameOverPopup != null)
            {
                gameOverPopup.SetActive(true);
                Time.timeScale = 0f;  // Freeze the game
            }
        }
    }
}
