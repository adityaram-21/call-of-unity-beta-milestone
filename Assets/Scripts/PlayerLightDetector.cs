using UnityEngine;
using TMPro;

public class PlayerLightDetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyLight"))  // Light should be tagged properly
        {
            Debug.Log("Player entered the red light. Game Over!");

            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.GameOver("Player got caught by security!");
            }
        }
    }
}
