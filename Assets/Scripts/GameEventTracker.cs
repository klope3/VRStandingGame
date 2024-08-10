using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventTracker : MonoBehaviour
{
    [SerializeField] private HealthHandler playerHealth; 
    private List<GameEventInt> playerDamages; //all the individual damage amounts the player has taken
    private float matchTime; //time since match start. Restarted from 0 at start of each match.
    public event System.Action OnEventRecorded;

    public int TimesDamaged
    {
        get
        {
            return playerDamages.Count;
        }
    }

    private void Awake()
    {
        playerDamages = new List<GameEventInt>();

        playerHealth.OnDamaged += PlayerHealth_OnDamaged;
    }

    private void Update()
    {
        matchTime += Time.deltaTime;
    }

    private void PlayerHealth_OnDamaged(int amount)
    {
        RecordPlayerDamage(amount, matchTime);
    }

    public void RecordPlayerDamage(int damage, float time)
    {
        playerDamages.Add(new GameEventInt(damage, time));
        OnEventRecorded?.Invoke();
    }

    private class GameEvent
    {
        public float time; //the time since game start when the event happened
    }

    private class GameEventInt : GameEvent
    {
        public int value;

        public GameEventInt(int value, float time)
        {
            this.value = value;
            this.time = time;
        }
    }
}
