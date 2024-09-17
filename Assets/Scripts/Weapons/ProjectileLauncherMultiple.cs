using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncherMultiple : MonoBehaviour
{
    [SerializeField] private ProjectileLauncher[] launchers;

    public void SetTriggerAll(bool b)
    {
        for (int i = 0; i < launchers.Length; i++)
        {
            launchers[i].SetTrigger(b);
        }
    }
}
