using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectPoolable : MonoBehaviour
{
    [SerializeField] 
    [Tooltip("Set the GameObject active when this is pulled from the pool via GetPooledObject.")] 
    private bool setActiveOnPull = true;

    private bool isAvailable;
    public UnityEvent OnReturnToPool;
    public UnityEvent OnPulledFromPool;

    public bool IsAvailable
    {
        get
        {
            return isAvailable;
        }
    }

    public void SetIsAvailable(bool b)
    {
        bool prevIsAvailable = isAvailable;
        isAvailable = b;

        if (!prevIsAvailable && isAvailable)
        {
            OnReturnToPool?.Invoke();
        }
        if (prevIsAvailable && !isAvailable)
        {
            if (setActiveOnPull) gameObject.SetActive(true);
            OnPulledFromPool?.Invoke();
        }
    }
}
