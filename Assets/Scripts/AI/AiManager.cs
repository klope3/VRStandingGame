using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

//Manages AI spawning and behavior.
//Useful for difficulty settings.
public class AiManager : SerializedMonoBehaviour
{
    [SerializeField] 
    [Tooltip("Enemies will only spawn when there are less than this many currently alive.")] 
    private int maxEnemies; 

    [SerializeField] 
    [Tooltip("The number of seconds between enemy spawns.")] 
    private float enemySpawnTimer;

    [OdinSerialize]
    [Tooltip("All enemies and their relative chances of spawning.")]
    private WeightedCollection<GameObjectPoolable> weights;

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
        GameObjectPoolable goPf = weights.ChooseWeightedRandom();
        GameObjectPoolable go = enemyPool.GetPooledObject(goPf);
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

    [Button]
    private void DebugLogWeights()
    {
        weights.DebugWeights();
    }
}
