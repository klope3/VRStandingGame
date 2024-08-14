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
        GameObject go = enemyPool.GetPooledObject();
        HealthHandler health = go.GetComponent<HealthHandler>();
        health.ResetHealth();
        health.OnDied += Health_OnDied;
        Vector3 position = positionProvider.GetRandPosition();
        go.transform.position = position;
        aliveEnemies++;
        OnSpawnEnemy?.Invoke();
    }

    private void Health_OnDied(HealthHandler health)
    {
        health.OnDied -= Health_OnDied;
        aliveEnemies--;
    }
}
