using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float lifespan;
    [SerializeField, Tooltip("The delay between when the projectile impacts " +
        "and when the GameObject is actually set " +
        "inactive.")] private float deathTime;
    [SerializeField, Min(0)] private float impactForce;
    [SerializeField] private LayerMask impactLayers;
    private Vector3 movementVector;
    private float lifeTimer;
    private float deathTimer;
    private bool hasImpacted;
    public UnityEvent OnImpact;
    public UnityEvent OnLaunch;
    public delegate void ProjectileImpact(Projectile projectile, RaycastHit rayHit);
    public event ProjectileImpact OnProjectileImpact;

    private void Update()
    {
        if (hasImpacted)
        {
            deathTimer += Time.deltaTime;
            if (deathTimer > deathTime)
            {
                gameObject.SetActive(false);
            }
            return;
        }

        lifeTimer += Time.deltaTime;
        if (lifeTimer > lifespan)
        {
            gameObject.SetActive(false);
            return;
        }

        Vector3 prevPos = transform.position;
        Vector3 nextPos = transform.position + movementVector * Time.deltaTime * speed;
        Ray ray = new Ray(prevPos, nextPos - prevPos);
        RaycastHit rayHit;
        bool hit = Physics.Raycast(ray, out rayHit, (nextPos - prevPos).magnitude, impactLayers);

        if (hit)
        {
            DoImpact(rayHit);
        }
        else
        {
            transform.position = nextPos;
        }
    }

    public void Launch(Vector3 movementVector)
    {
        this.movementVector = movementVector;
        hasImpacted = false;
        lifeTimer = 0;
        deathTimer = 0;
        OnLaunch?.Invoke();
    }

    private void DoImpact(RaycastHit rayHit = new RaycastHit(), bool moveToPoint = true)
    {
        if (moveToPoint)
        {
            transform.position = rayHit.point;
        }
        Collider hitCollider = rayHit.collider;
        if (hitCollider != null)
        {
            HealthHandler health = hitCollider.GetComponent<HealthHandler>();
            if (health != null)
            {
                health.AddHealth(-1 * damage);
            }
            Rigidbody body = hitCollider.GetComponent<Rigidbody>();
            if (body != null)
            {
                body.AddForceAtPosition(movementVector * impactForce, rayHit.point, ForceMode.Impulse);
            }
            ProjectileImpactable impactable = hitCollider.GetComponent<ProjectileImpactable>();
            if (impactable != null)
            {
                impactable.ReceiveImpact(this, rayHit);
            }
        }
        OnProjectileImpact?.Invoke(this, rayHit);
        OnImpact?.Invoke();
        hasImpacted = true;
    }

    /// <summary>
    /// Force impact behavior without requiring a collider to be hit.
    /// Because there was no actual hit, a dummy RaycastHit will be substituted.
    /// </summary>
    public void SimulateImpact()
    {
        DoImpact(new RaycastHit(), false);
    }
}
