using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchGun : MonoBehaviour
{
    [SerializeField, Tooltip("How strict to be about only triggering punches on forward movement. At 1, only perfect forward movement triggers a punch. At 0, even perfect sideways movement can trigger a punch.")]
        private float punchStrictness;
    [SerializeField, Tooltip("How far the muzzle needs to travel between frames to activate a shot.")] 
        private float punchTravel;
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private ProjectileLauncher launcher;
    private Vector3 positionLastFrame;

    private void Update()
    {
        Vector3 moveVec = muzzlePoint.position - positionLastFrame;
        float dot = Vector3.Dot(moveVec.normalized, muzzlePoint.forward);
        bool punch = dot > punchStrictness && moveVec.magnitude > punchTravel;
        launcher.SetTrigger(punch);

        positionLastFrame = muzzlePoint.position;
    }
}
