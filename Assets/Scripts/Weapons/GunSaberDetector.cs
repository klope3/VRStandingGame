using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class GunSaberDetector : MonoBehaviour
{
    [SerializeField] private GunSaber saber;

    public void OnTriggerEnter(Collider other)
    {
        saber.TrackTransform(other.transform);
    }

    public void OnTriggerExit(Collider other)
    {
        saber.UntrackTransform(other.transform);
    }
}
