using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBrain : MonoBehaviour
{
    [SerializeField] private EnemyPositionProvider positionProvider;
    [SerializeField] private AiMovement movement;
    [SerializeField] private float moveTimer;
    [SerializeField, Tooltip("When choosing a new navTarget, the max angle away from its current position, relative to the world center.")] 
        private float maxAngleDeviance;
    [SerializeField, Tooltip("Frequency of the trig function used to determine attack/non-attack times.")] private float attackFrequency;
    [SerializeField, Tooltip("Vertical offset of the trig function used to determine attack/non-attack times.")] private float attackOffset;
    [SerializeField] private ProjectileLauncher launcher;
    private float timer;
    private float attackTime; //used as the X value for evaluating the attack trig function.

    private void Awake()
    {
        timer = moveTimer;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            movement.SetNavTarget(positionProvider.GetRandPosition(transform.position, maxAngleDeviance));
            timer = moveTimer;
        }

        attackTime += Time.deltaTime;
        launcher.SetTrigger(IsAttacking());
    }

    // The "sometimes attacking, sometimes not" behavior can be defined with a simple trig function.
    // When it returns greater than 0, we're attacking.
    private bool IsAttacking()
    {
        return Mathf.Sin(attackFrequency * attackTime) + attackOffset > 0;
    }
}
