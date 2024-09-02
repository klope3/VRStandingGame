using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    [SerializeField] private GameObjectPoolable[] prefabsToPool;
    [SerializeField] private int startingCount;
    private Dictionary<GameObjectPoolable, List<GameObjectPoolable>> pooledObjects; //key = prefab used to instantiate, value = list of objects instantiated from that prefab
    public delegate void CreateEvent(GameObjectPoolable created);
    public event CreateEvent OnObjectCreated;

    private void Awake()
    {
        pooledObjects = new Dictionary<GameObjectPoolable, List<GameObjectPoolable>>();
        for (int i = 0; i < prefabsToPool.Length; i++)
        {
            List<GameObjectPoolable> pooled = new List<GameObjectPoolable>();
            for (int j = 0; j < startingCount; j++)
            {
                pooled.Add(CreatePooledObject(prefabsToPool[i]));
            }
            pooledObjects.Add(prefabsToPool[i], pooled);
        }
    }

    public GameObjectPoolable GetPooledObject(GameObjectPoolable requestedPrefab)
    {
        if (pooledObjects.Count == 0 || !pooledObjects.ContainsKey(requestedPrefab))
        {
            throw new System.Exception($"The requested prefab '{requestedPrefab.name} was not pooled by this object pool!");
        }

        List<GameObjectPoolable> matchingObjects = pooledObjects[requestedPrefab];
        GameObjectPoolable firstAvailable = matchingObjects.FirstOrDefault(obj => obj.IsAvailable);
        if (firstAvailable == null)
        {
            GameObjectPoolable created = CreatePooledObject(requestedPrefab);
            created.SetIsAvailable(false);
            return created;
        }

        firstAvailable.SetIsAvailable(false);
        return firstAvailable;
    }

    private GameObjectPoolable CreatePooledObject(GameObjectPoolable prefab)
    {
        GameObjectPoolable created = Instantiate(prefab, transform);
        created.SetIsAvailable(true);
        created.gameObject.SetActive(false);
        OnObjectCreated?.Invoke(created);
        return created;
    }
}
