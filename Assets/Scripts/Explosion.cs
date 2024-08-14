using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask affectedLayers;
    //private float testTimer;
    public UnityEvent OnExplode;

    private void Update()
    {
        //testTimer += Time.deltaTime;
        //if (testTimer > 3)
        //{
        //    Explode();
        //    testTimer = 0;
        //}
    }

    public void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, affectedLayers);
        foreach (Collider col in hitColliders)
        {
            HealthHandler health = col.GetComponent<HealthHandler>();
            if (health != null)
            {
                health.AddHealth(-1 * damage);
            }
        }
        OnExplode?.Invoke();
    }
}
