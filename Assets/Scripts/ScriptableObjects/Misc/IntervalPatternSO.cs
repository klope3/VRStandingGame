using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

[CreateAssetMenu(menuName = "Scriptable Objects/Misc/IntervalPatternSO")]
public class IntervalPatternSO : SerializedScriptableObject
{
    [OdinSerialize] private Interval[] intervals;

    public Interval[] Intervals { get { return intervals; } }
}

public class Interval
{
    [SerializeField] private float length;
    [SerializeField] private float maxLengthVariance;
    [SerializeField] private string name;

    public float Length { get { return length; } }
    public string Name { get { return name; } }
    public float MaxLengthVariance { get { return maxLengthVariance; } }
}
