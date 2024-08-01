using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Using this because OnTriggerUp in GrabbableUnityEvents didn't seem to be working.
//Works by receiving float value from OnTrigger.

public class TriggerEventHelper : MonoBehaviour
{
    private float triggerValue;
    public UnityEvent OnTriggerPressed;
    public UnityEvent OnTriggerReleased;

    public void OnTrigger(float triggerValue)
    {
        float prevVal = this.triggerValue;
        this.triggerValue = triggerValue;
        if (this.triggerValue > 0.5 && prevVal <= 0.5)
        {
            if (OnTriggerPressed != null) OnTriggerPressed.Invoke();
        } else if (this.triggerValue <= 0.5 && prevVal > 0.5)
        {
            if (OnTriggerReleased != null) OnTriggerReleased.Invoke();
        }
    }
}
