using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttackModule : MonoBehaviour
{
    [SerializeField] 
    [Tooltip("Find the GameObject with this tag on Awake to use as the attack target.")]
    private string attackTargetTag;
    
    [SerializeField] private GameObject laserPointer;
    [SerializeField] private ProjectileLauncher launcher;

    [SerializeField] 
    [Tooltip("The transform that will be rotated to visually 'look' at the attack target.")]
    private Transform lookingTransform;

    [SerializeField]
    private IntervalPattern intervalPattern;

    [HideInInspector]
    public bool isAttacking;
    private Transform attackTargetTrans; // when starting to telegraph attack, the AI stores this transform's current position.
    private Vector3 attackTarget; // while telegraphing the attack, the AI stays locked onto this static position, giving the player time to dodge.

    private void Awake()
    {
        GameObject targetGo = GameObject.FindGameObjectWithTag(attackTargetTag);
        if (!targetGo) Debug.LogWarning("No attack target object found!");
        attackTargetTrans = targetGo.transform;

        intervalPattern.OnIntervalFinished += IntervalPattern_OnIntervalFinished;
    }

    private void IntervalPattern_OnIntervalFinished(Interval finishedInterval)
    {
        if (finishedInterval.Name == "idle")
        {
            laserPointer.SetActive(true);
            attackTarget = attackTargetTrans.position; //when we go from "not attacking" to "attacking," store the target's current position
        }
        if (finishedInterval.Name == "telegraph")
        {
            laserPointer.SetActive(false);
            launcher.SetTrigger(true);
            isAttacking = true;
        }
        if (finishedInterval.Name == "attack")
        {
            isAttacking = false;
            launcher.SetTrigger(false);
        }
    }

    private void Update()
    {
        if (isAttacking) lookingTransform.LookAt(attackTarget);
        else lookingTransform.LookAt(attackTargetTrans);
    }
}
