using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiNavigationModule : MonoBehaviour
{
    [SerializeField] 
    [Tooltip("Find the position provider on awake using this tag.")]
    private string positionProviderTag;

    [SerializeField] private AiMovement movement;
    [SerializeField] private float moveTimer; 

    [SerializeField] 
    [Tooltip("When choosing a new navTarget, the max angle away from its current position, relative to the world center.")]
    private float maxAngleDeviance;

    private EnemyPositionProvider positionProvider;
    private float timer;

    private void Awake()
    {
        timer = moveTimer;

        GameObject go = GameObject.FindGameObjectWithTag(positionProviderTag);
        positionProvider = go.GetComponent<EnemyPositionProvider>();
        if (!positionProvider) Debug.LogWarning("No position provider found!");
    }

    private void Update()
    {
        HandleNavigation();
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
}
