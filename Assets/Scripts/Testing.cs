using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class Testing : MonoBehaviour
{
    [SerializeField] private GameObject hitPlayerVisual;
    [SerializeField] private LayerMask playerLayer;

    private void Update()
    {
        bool hit = Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit hitInfo, int.MaxValue, playerLayer);
        if (hit)
        {
            hitPlayerVisual.SetActive(true);
        } else
        {
            hitPlayerVisual.SetActive(false);
        }
    }

    public void ReceiveKnobEvent(float degrees)
    {
        VRUtils.Instance.Log($"Knob turned to {degrees} degrees");
    }

    public void ReceiveButtonEvent()
    {
        Debug.Log("The button was pressed");
    }
}
