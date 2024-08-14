using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBrain : MonoBehaviour
{
    [SerializeField, Tooltip("Find the position provider on awake using this tag.")]
    private string positionProviderTag;
    private EnemyPositionProvider positionProvider;
    [SerializeField] private AiMovement movement;
    [SerializeField] private float moveTimer;
    [SerializeField, Tooltip("When choosing a new navTarget, the max angle away from its current position, relative to the world center.")] 
        private float maxAngleDeviance;
    [SerializeField, Tooltip("The length of the interval during which telegraphing and attacking takes place.")] 
        private float attackInterval;
    [SerializeField, Tooltip("Max seconds to randomly add to each attack interval. Helps stop enemies from syncing up.")]
        private float attackIntervalVariance;
    [SerializeField, Tooltip("The AI will start telegraphing their attack for this many seconds before the end of the attackInterval.")]
        private float telegrapDuration;
    [SerializeField, Tooltip("The weapon will be in the 'attacking state' for this long at the end of the attackInterval.")]
        private float attackDuration;
    [SerializeField, Tooltip("Find the GameObject with this tag on Awake to use as the attack target.")]
        private string attackTargetTag;
    [SerializeField, Tooltip("The transform that will be rotated to visually 'look' at the attack target.")]
        private Transform lookingTransform;
    [SerializeField] private GameObject laserPointer;
    [SerializeField] private ProjectileLauncher launcher;
    private float timer;
    private float attackTimer;
    private bool lastFrameTelegraphing; //whether the AI was telegraphing last frame. used for determining when to store the attackTargetTrans position.
    private Transform attackTargetTrans; //when starting to telegraph attack, the AI stores this transform's current position.
    private Vector3 attackTarget; //while telegraphing the attack, the AI stays locked onto this static position, giving the player time to dodge.

    private void Awake()
    {
        ResetAttackTimer();
        timer = moveTimer;
        
        GameObject go = GameObject.FindGameObjectWithTag(positionProviderTag);
        positionProvider = go.GetComponent<EnemyPositionProvider>();
        if (!positionProvider) Debug.LogWarning("No position provider found!");
    
        GameObject targetGo = GameObject.FindGameObjectWithTag(attackTargetTag);
        if (!targetGo) Debug.LogWarning("No attack target object found!");
        attackTargetTrans = targetGo.transform;
    }

    private void Update()
    {
        HandleAim();
        HandleAttack();
        HandleNavigation();
    }

    private void HandleAim()
    {
        bool isTelegraphing = attackTimer < telegrapDuration && attackTimer > attackDuration;

        if (!lastFrameTelegraphing && isTelegraphing) attackTarget = attackTargetTrans.position; //when we go from "not telegraphing" to "telegraphing," store the target's current position

        if (isTelegraphing) lookingTransform.LookAt(attackTarget);
        else lookingTransform.LookAt(attackTargetTrans);

        if (isTelegraphing && !laserPointer.activeSelf) laserPointer.SetActive(true);
        if (!isTelegraphing && laserPointer.activeSelf) laserPointer.SetActive(false);

        lastFrameTelegraphing = isTelegraphing;
    }

    private void HandleAttack()
    {
        attackTimer -= Time.deltaTime;
        bool triggerDown = attackTimer < attackDuration;

        launcher.SetTrigger(triggerDown);

        if (attackTimer < 0) ResetAttackTimer();
    }

    private void HandleNavigation()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            movement.SetNavTarget(positionProvider.GetRandPosition(transform.position, maxAngleDeviance));
            timer = moveTimer;
        }
    }

    private void ResetAttackTimer()
    {
        attackTimer = attackInterval + Random.Range(0, attackIntervalVariance);
    }
}
