using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchDisplay : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private MatchManager matchManager;

    private void Update()
    {
        bar.fillAmount = matchManager.MatchTimer / matchManager.MatchLengthSeconds;
    }
}
