using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntervalPattern : MonoBehaviour
{
    [SerializeField] private IntervalPatternSO patternData;
    [SerializeField] private bool looping;
    [SerializeField]
    [Tooltip("If true, call ResetPattern() in OnEnable().")]
    private bool resetOnEnable = true;

    private float timer;
    private float timerRandAdd;
    private int intervalIndex;
    private bool running;
    public delegate void IntervalChange(Interval finishedInterval);
    public event IntervalChange OnIntervalFinished;
    public System.Action OnStart;
    public System.Action OnEnd;
    public UnityEvent OnReset;

    private void Awake()
    {
        if (patternData.Intervals.Length == 0) return;
        SetRunning(true);
    }

    private void OnEnable()
    {
        if (resetOnEnable) ResetPattern();
    }

    private void Update()
    {
        if (!running) return;

        if (timer == 0 && intervalIndex == 0)
        {
            OnStart?.Invoke();
        }
        timer += Time.deltaTime;
        Interval interval = patternData.Intervals[intervalIndex];
        if (timer < interval.Length + timerRandAdd) return;

        OnIntervalFinished?.Invoke(interval);
        intervalIndex++;
        timer = 0;
        bool reachedEnd = intervalIndex > patternData.Intervals.Length - 1;
        if (reachedEnd) intervalIndex = 0;
        timerRandAdd = Random.Range(0, patternData.Intervals[intervalIndex].MaxLengthVariance);

        if (reachedEnd)
        {
            OnEnd?.Invoke();
            if (!looping)
            {
                running = false;
            }
        } 
    }

    public void SetRunning(bool b)
    {
        running = b;
    }

    public void ResetPattern()
    {
        intervalIndex = 0;
        timer = 0;
        OnReset?.Invoke();
    }
}
