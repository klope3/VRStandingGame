using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPositionProvider : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Range(0, 360)] private float arcSpan;
    [SerializeField] private float minDistFromCenter;
    [SerializeField] private float maxDistFromCenter;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;
    [SerializeField] private int debugPosPerFrame;
    [SerializeField] private bool debugPositions;

    private void Update()
    {
        if (!debugPositions) return;

        for (int i = 0; i < debugPosPerFrame; i++)
        {
            Vector3 pos = GetRandPosition();
            Debug.DrawLine(pos, pos + Vector3.up * 0.1f, Color.red, 0.1f);
        }
    }

    public Vector3 GetRandPosition()
    {
        Vector3 euler = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(euler.x, Random.Range(-0.5f * arcSpan, 0.5f * arcSpan), euler.z);
        target.localPosition = new Vector3(0, 0, Random.Range(minDistFromCenter, maxDistFromCenter));
        return target.position + Vector3.up * Random.Range(minHeight, maxHeight);
    }
}
