using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class GunSaberDetector : MonoBehaviour
{
    [SerializeField] private GunSaber saber;
    [SerializeField, Tooltip("When activated, how many frames to stay active for.")] 
        private int activeFrames;
    private int activeFramesLeft;

    public void Activate()
    {
        activeFramesLeft = activeFrames;
    }

    private void Update()
    {
        if (activeFramesLeft > 0)
        {
            activeFramesLeft--;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (activeFramesLeft <= 0) return;

        saber.ShootTarget(other);
    }
}
