using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float thrustForce;
    [SerializeField] private float navTargetTolerance; //upon getting within this distance of navTarget, will decide that it has "reached" the target
    private Vector3 navTarget;
    private bool reachedNavTarget; //set to false when given a new navTarget, and set to true when within the tolerance distance

    private void Awake()
    {
        navTarget = transform.position;
    }

    private void Update()
    {
        if (reachedNavTarget) return;

        Vector3 vecToTarget = navTarget - transform.position;
        if (vecToTarget.magnitude <= navTargetTolerance)
        {
            reachedNavTarget = true;
            return;
        }

        rb.AddForce(vecToTarget.normalized * thrustForce * Time.deltaTime, ForceMode.Force);
    }

    public void SetNavTarget(Vector3 target)
    {
        navTarget = target;
        reachedNavTarget = false;
    }
}
