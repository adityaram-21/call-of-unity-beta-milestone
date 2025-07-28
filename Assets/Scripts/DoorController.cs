using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Collider doorCollider;
    public Animator animator; // Optional if using animation
    private bool isOpen = false;

    public void OpenDoor()
    {
        if (isOpen) return;
        isOpen = true;

        Debug.Log("🚪 Door is now open!");

        // Trigger animation (if used)
        if (animator != null)
        {
            animator.SetTrigger("Open");
        }
        else
        {
            // Basic door open: Move up
            transform.position += new Vector3(0, 3f, 0);
        }

        // Disable collider to allow player through
        if (doorCollider != null)
            doorCollider.enabled = false;
    }
}
