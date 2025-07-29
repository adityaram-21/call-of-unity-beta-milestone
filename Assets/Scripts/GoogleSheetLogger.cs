using UnityEngine;
using System.Runtime.InteropServices;

public class GoogleSheetLogger : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SendEventToGoogleSheet(string eventName, string eventData);

    public static void LogEvent(string eventName, string eventData)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            SendEventToGoogleSheet(eventName, eventData);
        }
        else
        {
            Debug.Log($"Event: {eventName}, Data: {eventData}");
        }
    }
}
