using System.Collections.Generic;
using UnityEngine;

public class AnalyticsService : MonoBehaviour
{
    private void Awake()
    {
        AppMetrica.Instance.RequestTrackingAuthorization(status => { });
    }
    
    public void TrackEvent(string eventName)
    {
        AppMetrica.Instance.ReportEvent(eventName);
    }

    public void TrackEventWithParams(string eventName, string paramName, object paramValue)
    {
        AppMetrica.Instance.ReportEvent(eventName, new Dictionary<string, object>
        {
            { paramName, paramValue }
        });
        Debug.Log("Event tracked with params: " + eventName + ", " + paramName + ": " + paramValue);
    }
}