using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSequence : MonoBehaviour
{
    public List<GameObject> tutorialSteps; // Drag & drop them in order in the Inspector
    public float delayBetweenSteps = 3f; // used for auto-advance (optional)

    private int currentStepIndex = 0;

    void Start()
{
    Debug.Log("Tutorial sequence starting...");
    HideAll();
    ShowStep(currentStepIndex);
}


    void Update()
    {
        // Advance on spacebar
        if (Input.GetKeyDown(KeyCode.Space))
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
        Debug.Log("Showing tutorial step: " + tutorialSteps[index].name);
        tutorialSteps[index].SetActive(true);
    }
}


    public void AdvanceStep()
    {
        // Hide current step
        if (currentStepIndex < tutorialSteps.Count)
            tutorialSteps[currentStepIndex].SetActive(false);

        currentStepIndex++;

        // Show next step if available
        if (currentStepIndex < tutorialSteps.Count)
        {
            ShowStep(currentStepIndex);
        }
        else
        {
            Debug.Log("Tutorial finished.");
            gameObject.SetActive(false); // Hide entire panel if done
        }
    }
}
