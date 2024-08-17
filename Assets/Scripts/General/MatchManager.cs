using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class MatchManager : MonoBehaviour
{
    [SerializeField] private GameEventTracker eventTracker;
    [SerializeField] private AiManager aiManager;
    [SerializeField] private EnemyPositionProvider positionProvider;
    [SerializeField] private Transform[] enemyPools;
    [SerializeField] private float matchMinutes;
    private readonly float countdownSeconds = 3;
    private float countdownTimer;
    private bool isCountingDown;
    private float matchTimer;
    private bool isMatchActive;
    private int nextMatchMaxEnemies;
    private float nextMatchArcSpan;
    private float nextMatchSpawnInterval;
    public UnityEvent OnCountdownStart;
    public UnityEvent OnCountdownSecond; //each time a second elapses in the countdown
    public UnityEvent OnMatchStart;
    public UnityEvent OnMatchStop;

    public float MatchTimer
    {
        get
        {
            return matchTimer;
        }
    }

    public float MatchLengthSeconds
    {
        get
        {
            return matchMinutes * 60;
        }
    }

    private void Update()
    {
        HandleCountdown();
        HandleMatchTime();
    }

    private void HandleMatchTime()
    {
        if (isMatchActive)
        {
            matchTimer += Time.deltaTime;;
            if (matchTimer >= MatchLengthSeconds)
            {
                StopMatch();
            }
        }
    }

    private void HandleCountdown()
    {
        if (isCountingDown)
        {
            if (countdownTimer <= 0)
            {
                isCountingDown = false;
                StartMatch();
            }
            float prevTimer = countdownTimer;
            countdownTimer -= Time.deltaTime;
            if (countdownTimer > 0 &&  Mathf.Ceil(prevTimer) != Mathf.Ceil(countdownTimer))
            {
                OnCountdownSecond?.Invoke();
            }
        }
    }

    public void StartMatchCountdown()
    {
        if (isMatchActive || isCountingDown) return;

        matchTimer = 0;
        countdownTimer = countdownSeconds;
        isCountingDown = true;
        eventTracker.ResetStats();
        OnCountdownSecond?.Invoke();
    }

    private void StartMatch()
    {
        isMatchActive = true;
        matchTimer = 0;
        aiManager.enabled = true;
        ApplyDifficulty();
        OnMatchStart?.Invoke();
    }

    private void ApplyDifficulty()
    {
        positionProvider.SetArcSpan(nextMatchArcSpan);
        aiManager.SetMaxEnemies(nextMatchMaxEnemies);
        aiManager.SetSpawnInterval(nextMatchSpawnInterval);
    }

    public void SetMatchPaused(bool b)
    {

    }

    public void StopMatch()
    {
        if (!isMatchActive && !isCountingDown) return;

        if (isCountingDown)
        {
            isCountingDown = false;
            return;
        }

        List<GameObject> matchObjects = GetMatchObjects();
        foreach (GameObject go in matchObjects)
        {
            go.SetActive(false);
        }

        isCountingDown = false;
        isMatchActive = false;
        aiManager.enabled = false;
        aiManager.ResetAll();
        OnMatchStop?.Invoke();
    }

    //Get all the objects relevant to stopping/pausing the match (e.g. active projectiles, enemies)
    private List<GameObject> GetMatchObjects()
    {
        List<GameObject> objects = new List<GameObject>();
        foreach (Transform poolTrans in enemyPools)
        {
            foreach (Transform enemyTrans in poolTrans)
            {
                objects.Add(enemyTrans.gameObject);
            }
        }
        return objects;
    }

    public void OnDifficultyKnobChange(float angle)
    {
        float angleToUse = angle > 180 ? angle - 360 + 90 : angle + 90;
        int difficulty = Mathf.RoundToInt(angleToUse / 180 * 10);
        SetNextMatchDifficulty(difficulty);
    }

    [Button]
    private void SetNextMatchDifficulty(int difficulty)
    {
        nextMatchArcSpan = 30 * difficulty + 30;
        nextMatchMaxEnemies = Mathf.RoundToInt(1.7f * difficulty + 2);
        nextMatchSpawnInterval = -0.35f * difficulty + 5;
    }
}
