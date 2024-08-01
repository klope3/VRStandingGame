using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    private bool triggerDown;

    public void SetTrigger(bool b)
    {
        Debug.Log($"Called with {b}");
        bool prevTrigger = triggerDown;
        triggerDown = b;
        string word = b ? "pressed" : "released";
        if (prevTrigger != triggerDown) Debug.Log($"Trigger {word}");
    }
}
