using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Manages AI spawning and behavior.
//Useful for difficulty settings.
public class AiManager : MonoBehaviour
{
    [SerializeField, Tooltip("Enemies will only spawn when there are less than this many currently alive.")] 
        private int maxEnemies; 
    [SerializeField, Tooltip("The number of seconds between enemy spawns.")] 
        private float enemySpawnTimer;
    [SerializeField] private GameObjectPoolable tempEnemyPf; //this will be removed once we're actually using more than one enemy type
    [SerializeField] private GameObjectPool enemyPool;
    [SerializeField] private EnemyPositionProvider positionProvider;
    private float spawnTimer;
    private int aliveEnemies;
    public UnityEvent OnSpawnEnemy;

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= enemySpawnTimer)
        {
            spawnTimer = 0;
            if (CanSpawnEnemyNow())
            {
                SpawnEnemy();
            } 
        }
    }

    private bool CanSpawnEnemyNow()
    {
        return aliveEnemies < maxEnemies;
    }

    private void SpawnEnemy()
    {
        GameObjectPoolable go = enemyPool.GetPooledObject(tempEnemyPf);
        HealthHandler health = go.GetComponent<HealthHandler>();
        health.ResetHealth();
        health.OnDied += Health_OnDied;

        AiNavigationModule nav = go.GetComponent<AiNavigationModule>();
        Vector3 position = nav.GetNextNavPosition();
        go.transform.position = position;

        AiMovement movement = go.GetComponent<AiMovement>();
        movement.SetNavTarget(position);

        aliveEnemies++;
        OnSpawnEnemy?.Invoke();
    }

    public void ResetAll()
    {
        aliveEnemies = 0;
        spawnTimer = 0;
    }

    public void SetMaxEnemies(int max)
    {
        maxEnemies = max;
    }

    public void SetSpawnInterval(float seconds)
    {
        enemySpawnTimer = seconds;
    }

    private void Health_OnDied(HealthHandler health)
    {
        health.OnDied -= Health_OnDied;
        aliveEnemies--;
    }
}
