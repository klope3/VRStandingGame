using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchGun : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How strict to be about only triggering punches on forward movement. At 1, only perfect forward movement triggers a punch. At 0, even perfect sideways movement can trigger a punch.")]
    private float punchStrictness;
    
    [SerializeField]
    [Tooltip("How fast the muzzle needs to travel to activate a fully-powered shot.")]
    private float fullPunchSpeed;

    [SerializeField]
    [Tooltip("How fast the muzzle needs to travel to activate a low-powered shot. If the speed is above fullPunchSpeed, a fully-powered one will happen instead.")]
    private float lowPunchSpeed;
    
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private ProjectileLauncher fullPowerLauncher;
    [SerializeField] private ProjectileLauncher lowPowerLauncher;
    [SerializeField] private float shotsPerSecond;
    private Vector3 positionLastFrame; //one frame ago
    private Vector3 positionBeforeLastFrame; //two frames ago
    private float triggerTimer; //uses own trigger timer since two separate launchers are being used

    private void Update()
    {
        Vector3 curMoveVec = muzzlePoint.position - positionLastFrame;
        Vector3 prevMoveVec = positionLastFrame - positionBeforeLastFrame;

        float curDistTraveled = curMoveVec.magnitude;
        float curUnitsPerSecond = curDistTraveled / Time.deltaTime;
        float prevDistTraveled = prevMoveVec.magnitude;
        float prevUnitsPerSecond = prevDistTraveled / Time.deltaTime;

        bool isPunchSlowing = prevUnitsPerSecond > curUnitsPerSecond;
        bool fullyPowered = prevUnitsPerSecond > fullPunchSpeed;
        bool lowPowered = prevUnitsPerSecond > lowPunchSpeed;

        float dot = Vector3.Dot(curMoveVec.normalized, muzzlePoint.forward);
        bool isPunchStraightEnough = dot > punchStrictness;
        bool timerReady = triggerTimer >= 1 / shotsPerSecond;

        if (timerReady && isPunchStraightEnough && isPunchSlowing)
        {
            if (fullyPowered)
            {
                fullPowerLauncher.SetTrigger(true);
                triggerTimer = 0;
            } else if (lowPowered)
            {
                lowPowerLauncher.SetTrigger(true);
                triggerTimer = 0;
            } else
            {
                fullPowerLauncher.SetTrigger(false);
                lowPowerLauncher.SetTrigger(false);
            }
        } else
        {
            fullPowerLauncher.SetTrigger(false);
            lowPowerLauncher.SetTrigger(false);
        }

        if (triggerTimer < 1 / shotsPerSecond) triggerTimer += Time.deltaTime;

        positionBeforeLastFrame = positionLastFrame;
        positionLastFrame = muzzlePoint.position;
    }
}
