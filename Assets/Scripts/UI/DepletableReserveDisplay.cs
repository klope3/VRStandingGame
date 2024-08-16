using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This should replace the current AmmoDisplay
public class DepletableReserveDisplay : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private DepletableReserve reserve;

    private void Update()
    {
        bar.fillAmount = reserve.CurAmount / reserve.MaxAmount;
    }
}
