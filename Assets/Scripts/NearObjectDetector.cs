using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class NearObjectDetector : MonoBehaviour
{
    private Collider col;
    public UnityEvent OnObjectDetected;

    private void Awake()
    {
        col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Detected {other.gameObject.name}");
        OnObjectDetected?.Invoke();
    }
}
