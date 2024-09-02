using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Weapons/ProjectileLauncherSO")]
public class ProjectileLauncherSO : ScriptableObject
{
    [SerializeField] private float shotsPerSecond;
    [SerializeField, Range(0, 1)] private float inaccuracy;
    [SerializeField] private FireType fireType;
    [SerializeField] private GameObjectPoolable projectilePf;
    [SerializeField] private int ammoPerShot;

    public float ShotsPerSecond { get { return shotsPerSecond; } }
    public float Inaccuracy { get { return inaccuracy; } }
    public FireType FiringType { get { return fireType; } }
    public GameObjectPoolable ProjectilePf { get { return projectilePf; } }
    public int AmmoPerShot { get { return ammoPerShot; } }

    public enum FireType
    {
        SemiAutomatic,
        Automatic
    }
}
