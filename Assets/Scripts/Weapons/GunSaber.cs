using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GunSaber : MonoBehaviour
{
    [SerializeField, Tooltip("How fast the saber needs to be moving in order to fire.")] 
        private float unitsPerSecond;
    //[SerializeField] private ProjectileLauncher launcher;
    [SerializeField] private Collider detector;
    [SerializeField, Tooltip("How to be about needing to aim directly at an enemy to hit it. 0-1, where 1 is most strict.")] 
        private float detectorVectorTolerance;
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private int damage;
    [SerializeField] private float shotsPerSecond;
    private float shotTimer;
    private Vector3 lastPosition; //the muzzlePoint's position last frame, for measuring speed
    private List<Transform> trackedTransforms; //keeps track of all the transforms currently inside the detector
    public UnityEvent OnFire;

    private void Awake()
    {
        lastPosition = muzzlePoint.position;
        trackedTransforms = new List<Transform>();
    }

    private void Update()
    {
        if (shotTimer > 0) shotTimer -= Time.deltaTime;

        float distTraveled = (muzzlePoint.position - lastPosition).magnitude;
        float actualUnitsPerSecond = distTraveled / Time.deltaTime;
        lastPosition = muzzlePoint.position;
        if (actualUnitsPerSecond >= unitsPerSecond)
        {
            TryShoot();
        }
    }

    private void TryShoot()
    {
        Transform target = ChooseTarget();
        if (target == null || shotTimer > 0) return;

        HealthHandler health = target.GetComponent<HealthHandler>();
        health.AddHealth(-1 * damage);
        trackedTransforms.Remove(target);
        shotTimer = 1 / shotsPerSecond;

        OnFire?.Invoke();
    }

    private Transform ChooseTarget()
    {
        //Out of all the transforms currently inside the detector that are currently aimed at "precisely enough" (if any),
        //pick the closest one as the target.
        Transform target = trackedTransforms.Where(otherTrans =>
        {
            Vector3 vecToTrans = otherTrans.position - transform.position;
            return Vector3.Dot(transform.forward, vecToTrans) <= detectorVectorTolerance;
        })
        .DefaultIfEmpty(null)
        .OrderBy(otherTrans =>
        {
            if (otherTrans == null) return 0;
            return Vector3.Distance(otherTrans.position, transform.position);
        })
        .First();

        return target;
    }

    public void TrackTransform(Transform trans)
    {
        trackedTransforms.Add(trans);
    }

    public void UntrackTransform(Transform trans)
    {
        trackedTransforms.Remove(trans);
    }
}
