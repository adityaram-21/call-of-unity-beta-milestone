using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GoogleFormAnalytics : MonoBehaviour
{
    private string formURL = "https://docs.google.com/forms/d/e/1FAIpQLSe0k8G6ttB_QGWxOkHnApjGFhvLv5tLzldULRY-Lzz_a5s8Dw/formResponse";

    public void LogEvent(string eventName, string eventData)
    {
        StartCoroutine(PostToGoogle(eventName, eventData));
    }

    IEnumerator PostToGoogle(string eventName, string eventData)
    {
        WWWForm form = new WWWForm();

        form.AddField("entry.1931112680", eventName);
        form.AddField("entry.590746167", eventData);

        UnityWebRequest www = UnityWebRequest.Post(formURL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success || www.responseCode == 0)
        {
            Debug.Log("Google Form submission successful.");
        }
        else
        {
            Debug.LogError("Submission failed: " + www.error);
        }
    }
}