using UnityEngine;

public class TutorialTriggerObject : MonoBehaviour
{
    public GameObject popupToShow;        // Assign a UI popup panel (disabled by default)
    public DoorController doorToOpen;     // Assign the DoorController script on your door

    private bool hasBeenTriggered = false;

    void OnMouseDown()
    {
        if (hasBeenTriggered) return;
        hasBeenTriggered = true;

        // Show popup
        if (popupToShow != null)
        {
            popupToShow.SetActive(true);
            StartCoroutine(HidePopupAfterDelay());
        }

        // Open the door
        if (doorToOpen != null)
        {
            doorToOpen.OpenDoor();
        }
    }

    private System.Collections.IEnumerator HidePopupAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        popupToShow.SetActive(false);
    }
}
