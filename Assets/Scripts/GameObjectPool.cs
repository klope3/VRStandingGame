using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefabToPool;
    [SerializeField] private int startingCount;
    private List<GameObject> pooledObjects;

    private void Awake()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < startingCount; i++)
        {
            CreatePooledObject();
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            GameObject obj = pooledObjects[i];
            if (!obj.activeSelf)
            {
                return obj;
            } 
        }

        return CreatePooledObject();
    }

    private GameObject CreatePooledObject()
    {
        GameObject go = Instantiate(prefabToPool, transform);
        go.SetActive(false);
        pooledObjects.Add(go);
        return go;
    }
}
