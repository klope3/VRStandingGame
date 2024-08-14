using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private ProjectileLauncher launcher;

    private void Update()
    {
        bar.fillAmount = launcher.CurAmmo / launcher.MaxAmmo;
    }
}
