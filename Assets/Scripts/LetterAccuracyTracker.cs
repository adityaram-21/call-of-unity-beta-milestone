using UnityEngine;

public class LetterAccuracyTracker : MonoBehaviour
{
    private int correctLetters = 0;
    private int incorrectLetters = 0;
    private GoogleFormAnalytics googleFormAnalytics;

    void Start()
    {
        googleFormAnalytics = FindObjectOfType<GoogleFormAnalytics>();
        correctLetters = 0;
        incorrectLetters = 0;
    }

    public void AddCorrect()
    {
        correctLetters++;
        Debug.Log($"Correct Letters: {correctLetters}");
    }

    public void AddIncorrect()
    {
        incorrectLetters++;
        Debug.Log($"Incorrect Letters: {incorrectLetters}");
    }

    public void LogLetterAccuracy(string levelName)
    {
        int total = correctLetters + incorrectLetters;
        float percent = total > 0 ? ((float)correctLetters / total) * 100f : 0f;

        string data = $"{levelName},{correctLetters},{incorrectLetters},{total},{percent:F2}";
        googleFormAnalytics.LogEvent("LetterAccuracy", data);

        Debug.Log($"[LetterAccuracy] Logged for {levelName}: {data}");

        correctLetters = 0;
        incorrectLetters = 0;
    }
}