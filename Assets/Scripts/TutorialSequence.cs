using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSequence : MonoBehaviour
{
    public List<GameObject> tutorialSteps; // Assign WelcomePanel, MovementPanel, etc., in order
    public float delayBetweenSteps = 3f;

    private int currentStepIndex = 0;

    void Start()
    {
        Debug.Log("Tutorial sequence starting...");
        HideAll();
        ShowStep(currentStepIndex); // Only Welcome panel shows
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentStepIndex == 2) // For Spacebar step
        {
            AdvanceStep();
        }

        if (currentStepIndex == 1 && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            AdvanceStep();
        }
    }

    void HideAll()
    {
        foreach (var step in tutorialSteps)
        {
            step.SetActive(false);
        }
    }

    void ShowStep(int index)
    {
        if (index >= 0 && index < tutorialSteps.Count)
        {
            tutorialSteps[index].SetActive(true);
        }
    }

    public void AdvanceStep()
    {
        if (currentStepIndex < tutorialSteps.Count)
            tutorialSteps[currentStepIndex].SetActive(false);

        currentStepIndex++;

        if (currentStepIndex < tutorialSteps.Count)
        {
            ShowStep(currentStepIndex);
        }
        else
        {
            gameObject.SetActive(false); // Hide entire tutorial canvas
        }
    }
}
