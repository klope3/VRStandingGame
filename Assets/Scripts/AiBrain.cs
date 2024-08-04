using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBrain : MonoBehaviour
{
    [SerializeField] private EnemyPositionProvider positionProvider;
    [SerializeField] private AiMovement movement;
    [SerializeField] private float moveTimer;
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
            movement.SetNavTarget(positionProvider.GetRandPosition());
            timer = moveTimer;
        }
    }
}
