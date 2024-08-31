using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    [SerializeField] private GameObjectPoolable prefabToPool;
    [SerializeField] private int startingCount;
    private List<GameObjectPoolable> pooledObjects;
    public delegate void CreateEvent(GameObjectPoolable created);
    public event CreateEvent OnObjectCreated;

    private void Awake()
    {
        pooledObjects = new List<GameObjectPoolable>();
        for (int i = 0; i < startingCount; i++)
        {
            CreatePooledObject();
        }
    }

    public GameObjectPoolable GetPooledObject()
    {
        GameObjectPoolable firstAvailable = pooledObjects.Find(obj => obj.IsAvailable);
        if (firstAvailable == null) return CreatePooledObject();

        firstAvailable.SetIsAvailable(false);
        return firstAvailable;
    }

    private GameObjectPoolable CreatePooledObject()
    {
        GameObjectPoolable created = Instantiate(prefabToPool, transform);
        created.SetIsAvailable(true);
        created.gameObject.SetActive(false);
        pooledObjects.Add(created);
        OnObjectCreated?.Invoke(created);
        return created;
    }
}
