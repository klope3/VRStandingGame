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
    private float timer;

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
    }
}
