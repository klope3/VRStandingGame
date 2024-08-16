using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Useful for representing ammo, batteries, health, etc.
/// </summary>
// Existing mechanics that depend on these things should migrate to using this instead.
public class DepletableReserve : MonoBehaviour
{
    [SerializeField, Tooltip("For dev reference only. For example, 'health', 'pistol ammo', etc.")] private string label;
    [SerializeField] private ReserveType reserveType;
    [SerializeField, Min(1), Tooltip("Only used for 'recharge' reserve type.")] private float rechargePerSecond;
    [SerializeField, Min(1)] private float maxAmount;
    [SerializeField, Min(1)] private float startingAmount;
    private float currentAmount;
    public UnityEvent OnAmountChange;
    public event System.Action OnAmountChanged;

    public float CurAmount
    {
        get
        {
            return currentAmount;
        }
    }

    public float MaxAmount
    {
        get
        {
            return maxAmount;
        }
    }

    public enum ReserveType
    {
        Normal, //does not replenish automatically
        Recharge //replenishes automatically over time
    }

    private void Awake()
    {
        AddAmount(startingAmount);
    }

    private void Update()
    {
        if (reserveType == ReserveType.Recharge)
        {
            float amountToRecharge = rechargePerSecond * Time.deltaTime;
            AddAmount(amountToRecharge);
        }
    }

    public void AddAmount(float amount)
    {
        float prevAmount = currentAmount;
        currentAmount = Mathf.Clamp(currentAmount + amount, 0, maxAmount);

        if (prevAmount != currentAmount)
        {
            OnAmountChange?.Invoke();
            OnAmountChanged?.Invoke();
        }
    }

}
