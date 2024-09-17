using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchGun : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How strict to be about only triggering punches on forward movement. At 1, only perfect forward movement triggers a punch. At 0, even perfect sideways movement can trigger a punch.")]
    private float punchStrictness;
    
    [SerializeField]
    [Tooltip("How fast the muzzle needs to travel to activate a shot.")]
    private float punchSpeed;
    
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private ProjectileLauncher launcher;
    private Vector3 positionLastFrame;

    private void Update()
    {
        Vector3 moveVec = muzzlePoint.position - positionLastFrame;
        float distTraveled = moveVec.magnitude;
        float actualUnitsPerSecond = distTraveled / Time.deltaTime;
        float dot = Vector3.Dot(moveVec.normalized, muzzlePoint.forward);
        bool punch = dot > punchStrictness && actualUnitsPerSecond > punchSpeed;
        launcher.SetTrigger(punch);

        positionLastFrame = muzzlePoint.position;
    }
}
