using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] public ProjectileLauncherSO launcherData;
    [SerializeField] private DepletableReserve ammoReserve;
    [SerializeField] private Transform muzzleLocation;
    [SerializeField] private GameObjectPool projectilePool;
    [SerializeField, Tooltip("Whether to try and find a pool on Awake. " +
        "Useful for weapons instantiated at runtime, since these won't have " +
        "a reference to a pool yet.")]
    private bool findPool;
    [SerializeField, Tooltip("The tag to use for finding a pool on Awake. " +
        "Does nothing if findPool is false.")]
    private string findPoolTag;
    [SerializeField] private bool showAimLine;
    private float triggerTimer;
    private bool wasTriggerPulled; //last frame
    private bool isTriggerPulled; //this frame
    public UnityEvent OnFire;
    public UnityEvent OnAmmoChange;

    private void Awake()
    {
        if (findPool)
        {
            string findError = $"The weapon attached to {gameObject.name} couldn't find a GameObjectPool with the tag {findPoolTag}!";
            GameObject objWithTag = GameObject.FindGameObjectWithTag(findPoolTag);
            if (objWithTag == null)
            {
                Debug.LogWarning(findError);
            }
            projectilePool = objWithTag.GetComponent<GameObjectPool>();
            if (projectilePool == null)
            {
                Debug.LogWarning(findError);
            }
        }
    }

    private void Update()
    {
        TryFire();
        wasTriggerPulled = isTriggerPulled;
    }

    private void OnDrawGizmos()
    {
        if (showAimLine && muzzleLocation != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(muzzleLocation.position, muzzleLocation.position + muzzleLocation.forward * 1000);
        }
    }

    public void SetTrigger(bool b)
    {
        isTriggerPulled = b;
    }

    private void TryFire()
    {
        bool triggerTimerReady = triggerTimer >= 1 / launcherData.ShotsPerSecond;
        if (!triggerTimerReady)
        {
            triggerTimer += Time.deltaTime;
        }

        bool canAutoFire = isTriggerPulled && triggerTimerReady;
        bool canSemiAutoFire = !wasTriggerPulled && isTriggerPulled && triggerTimerReady;
        bool isFiringAllowedByTrigger = (canAutoFire && launcherData.FiringType == ProjectileLauncherSO.FireType.Automatic) || (canSemiAutoFire && launcherData.FiringType == ProjectileLauncherSO.FireType.SemiAutomatic);
        bool enoughAmmo = ammoReserve.CurAmount >= launcherData.AmmoPerShot;

        if (isFiringAllowedByTrigger && enoughAmmo)
        {
            Fire();
        }
    }

    public void Fire()
    {
        GameObjectPoolable pooledProj = projectilePool.GetPooledObject(launcherData.ProjectilePf);
        pooledProj.transform.position = muzzleLocation.position;
        Projectile proj = pooledProj.GetComponent<Projectile>();
        Vector3 projVector = GetInaccurateVector();
        proj.Launch(projVector);
        proj.transform.forward = projVector;
        triggerTimer = 0;
        ammoReserve.AddAmount(-1 * launcherData.AmmoPerShot);

        OnFire?.Invoke();
    }

    private Vector3 GetInaccurateVector()
    {
        Vector3 randOffset = Random.insideUnitCircle * launcherData.Inaccuracy * 0.001f;
        return (muzzleLocation.right * randOffset.x + muzzleLocation.up * randOffset.y + muzzleLocation.forward * 0.001f).normalized;
    }
}
