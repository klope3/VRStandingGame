using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileImpactable : MonoBehaviour
{
    public abstract void ReceiveImpact(Projectile projectile, RaycastHit rayHit);
}
