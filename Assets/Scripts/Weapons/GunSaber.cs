using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunSaber : MonoBehaviour
{
    [SerializeField, Tooltip("The launcher trigger will be set to true when the saber has traveled at least this far since last frame. Essentially a minimum speed setting.")] 
        private float minFireDist;
    //[SerializeField] private ProjectileLauncher launcher;
    [SerializeField] private GunSaberDetector detector;
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private int damage;
    private Vector3 lastPosition; //the muzzlePoint's position last frame, for measuring speed
    public UnityEvent OnFire;

    private void Awake()
    {
        lastPosition = muzzlePoint.position;
    }

    private void Update()
    {
        float distTraveled = (muzzlePoint.position - lastPosition).magnitude;
        //launcher.SetTrigger(distTraveled >= minFireDist);
        lastPosition = muzzlePoint.position;
        if (distTraveled < minFireDist)
        {
            return;
        }

        detector.Activate();
    }

    public void ShootTarget(Collider other)
    {
        HealthHandler health = other.GetComponent<HealthHandler>();
        health.AddHealth(-1 * damage);
        OnFire?.Invoke();
    }
}
