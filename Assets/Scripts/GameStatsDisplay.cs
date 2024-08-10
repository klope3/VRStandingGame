using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatsDisplay : MonoBehaviour
{
    [SerializeField] private GameEventTracker eventTracker;
    [SerializeField] private TextMeshProUGUI playerDamageCountText;

    private void Awake()
    {
        eventTracker.OnEventRecorded += EventTracker_OnEventRecorded;
    }

    private void EventTracker_OnEventRecorded()
    {
        playerDamageCountText.text = $"{eventTracker.TimesDamaged}";
    }
}
