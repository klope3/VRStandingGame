using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAimingModule : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Find the GameObject with this tag on Awake to use as the aim target.")]
    private string aimTargetTag;

    [SerializeField]
    [Tooltip("The transform that will be rotated to visually 'look' at the aim target.")]
    private Transform aimingTransform;

    private bool aimingLocked; //when true, aim at the fixed position "aimTargetPosition".
    private Transform aimTargetTrans; // when aiming is set to locked, the AI stores this transform's current position.
    private Vector3 aimTargetPosition;

    private void Awake()
    {
        GameObject targetGo = GameObject.FindGameObjectWithTag(aimTargetTag);
        if (!targetGo) Debug.LogWarning("No aim target object found!");
        aimTargetTrans = targetGo.transform;
    }

    private void Update()
    {
        if (aimingLocked) aimingTransform.LookAt(aimTargetPosition);
        else aimingTransform.LookAt(aimTargetTrans);
    }

    public void SetAimingLocked(bool b)
    {
        bool prevLocked = aimingLocked;
        aimingLocked = b;
        if (!prevLocked && aimingLocked) aimTargetPosition = aimTargetTrans.position;
    }
}
