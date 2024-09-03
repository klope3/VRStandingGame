using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ProjectileLauncherDataSwitcher : MonoBehaviour
{
    [SerializeField] 
    [Tooltip("The data objects that will be cycled through. On Awake, the first item will be used.")]
    private ProjectileLauncherSO[] dataObjects;
    [SerializeField] private ProjectileLauncher launcher;
    private int index;

    private void Awake()
    {
        if (dataObjects.Length == 0) return;

        launcher.launcherData = dataObjects[0];
    }

    [Button, DisableInEditorMode]
    public void SwitchToNext()
    {
        index++;
        if (index > dataObjects.Length - 1) index = 0;
        launcher.launcherData = dataObjects[index];
    }
}
