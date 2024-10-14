using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatchVisualCountdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private MatchManager matchManager;

    public void UpdateCountdown()
    {
        int timerInt = Mathf.CeilToInt(matchManager.CountdownTimer);
        text.text = $"{timerInt}";
    }

    public void EraseCountdown()
    {
        text.text = "";
    }
}
