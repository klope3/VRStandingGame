using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalShield : ProjectileImpactable
{
    [SerializeField] private DepletableReserve energyCell;
    [SerializeField] private int depletionPerHit;
    [SerializeField] private GameObject shieldObj;

    private void Awake()
    {
        energyCell.OnAmountChanged += EnergyCell_OnAmountChanged;
    }

    private void EnergyCell_OnAmountChanged()
    {
        if (energyCell.CurAmount > 0 && !shieldObj.activeSelf) shieldObj.SetActive(true);
    }

    public override void ReceiveImpact(Projectile projectile, RaycastHit rayHit)
    {
        energyCell.AddAmount(-1 * depletionPerHit);
        if (energyCell.CurAmount <= 0) shieldObj.SetActive(false);
    }
}
