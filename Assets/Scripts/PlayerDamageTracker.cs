using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageTracker : MonoBehaviour
{
    [SerializeField] private HealthHandler playerHealth;
    [SerializeField] private GameEventTracker eventTracker;

    private void Awake()
    {
        playerHealth.OnDamaged += PlayerHealth_OnDamaged;
    }

    private void PlayerHealth_OnDamaged(int amount)
    {
        eventTracker.RecordPlayerDamage(amount, 1);
    }
}
