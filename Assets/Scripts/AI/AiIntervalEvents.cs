using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AiIntervalEvents : MonoBehaviour
{
    [SerializeField]
    private IntervalPattern intervalPattern;

    public UnityEvent OnAttackStart;
    public UnityEvent OnAttackStop;
    public UnityEvent OnTelegraphStart;
    public UnityEvent OnTelegraphStop;

    private void Awake()
    {
        intervalPattern.OnIntervalFinished += IntervalPattern_OnIntervalFinished;
    }

    private void IntervalPattern_OnIntervalFinished(Interval finishedInterval)
    {
        if (finishedInterval.Name == "idle")
        {
            OnTelegraphStart?.Invoke();
        }
        if (finishedInterval.Name == "telegraph")
        {
            OnTelegraphStop?.Invoke();
            OnAttackStart?.Invoke();
        }
        if (finishedInterval.Name == "attack")
        {
            OnAttackStop?.Invoke();
        }
    }
}
