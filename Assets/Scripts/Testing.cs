using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class Testing : MonoBehaviour
{
    public void ReceiveKnobEvent(float degrees)
    {
        VRUtils.Instance.Log($"Knob turned to {degrees} degrees");
    }

    public void ReceiveButtonEvent()
    {
        Debug.Log("The button was pressed");
    }
}
