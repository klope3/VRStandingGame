using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthHandler : MonoBehaviour
{
    [SerializeField] private int startingHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private bool invincible;
    [SerializeField] private bool destroyOnDeath = true;
    [SerializeField] private bool inactiveOnDeath;
    [SerializeField] private bool showLogs;
    private int curHealth;
    public UnityEvent OnDamage;
    public UnityEvent OnHeal;
    public UnityEvent OnDie;
    public event System.Action OnDied;

    public int CurHealth
    {
        get
        {
            return curHealth;
        }
    }

    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
    }

    private void Awake()
    {
        curHealth = startingHealth;
    }

    public void AddHealth(int amount)
    {
        if (amount == 0 || (amount < 0 && invincible))
            return;

        curHealth += amount;
        curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
        if (showLogs)
            Debug.Log($"Added {amount} to {gameObject.name}'s health");

        if (amount > 0)
            OnHeal?.Invoke();
        if (amount < 0)
            OnDamage?.Invoke();
        if (curHealth == 0)
        {
            OnDie?.Invoke();
            OnDied?.Invoke();
            if (destroyOnDeath)
                Destroy(gameObject);
            else if (inactiveOnDeath)
                gameObject.SetActive(false);
        }
    }
}
