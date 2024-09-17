using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Provides valid enemy positions for enemy spawning and navigation.
//Helps prevent spawning in or moving to undesired locations.
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
        return CalcRandPosition(-0.5f * arcSpan, 0.5f * arcSpan, minDistFromCenter, maxDistFromCenter, minHeight, maxHeight);
    }

    public Vector3 GetRandPosition(Vector3 refPosition, float maxAngleDeviance)
    {
        //Vector3 flattenedRefPos = new Vector3(refPosition.x, transform.position.y, refPosition.z);
        //transform.LookAt(flattenedRefPos);
        //Vector3 euler = transform.localEulerAngles;
        //float startingY = euler.y > 180 ? euler.y - 360 : euler.y;
        //float deviatedMin = startingY - maxAngleDeviance;
        //float deviatedMax = startingY + maxAngleDeviance;
        //float arcMin = -0.5f * arcSpan;
        //float arcMax = 0.5f * arcSpan;
        //float clampMin = deviatedMin > arcMin ? deviatedMin : arcMin;
        //float clampMax = deviatedMax < arcMax ? deviatedMax : arcMax;
        CalcClampedDeviatedAngle(refPosition, maxAngleDeviance, out float clampMin, out float clampMax);
        return CalcRandPosition(clampMin, clampMax, this.minDistFromCenter, this.maxDistFromCenter, this.minHeight, this.maxHeight);
    }

    public Vector3 GetRandPosition(Vector3 refPosition, float maxAngleDeviance, float minDistFromCenter, float maxDistFromCenter, float minHeight, float maxHeight)
    {
        //Vector3 flattenedRefPos = new Vector3(refPosition.x, transform.position.y, refPosition.z);
        //transform.LookAt(flattenedRefPos);
        //Vector3 euler = transform.localEulerAngles;
        //float startingY = euler.y > 180 ? euler.y - 360 : euler.y;
        //float deviatedMin = startingY - maxAngleDeviance;
        //float deviatedMax = startingY + maxAngleDeviance;
        //float arcMin = -0.5f * arcSpan;
        //float arcMax = 0.5f * arcSpan;
        //float clampMin = deviatedMin > arcMin ? deviatedMin : arcMin;
        //float clampMax = deviatedMax < arcMax ? deviatedMax : arcMax;
        CalcClampedDeviatedAngle(refPosition, maxAngleDeviance, out float clampMin, out float clampMax);
        return CalcRandPosition(clampMin, clampMax, minDistFromCenter, maxDistFromCenter, minHeight, maxHeight);
    }

    private void CalcClampedDeviatedAngle(Vector3 refPosition, float maxAngleDeviance, out float clampMin, out float clampMax)
    {
        Vector3 flattenedRefPos = new Vector3(refPosition.x, transform.position.y, refPosition.z);
        transform.LookAt(flattenedRefPos);
        Vector3 euler = transform.localEulerAngles;
        float startingY = euler.y > 180 ? euler.y - 360 : euler.y;
        float deviatedMin = startingY - maxAngleDeviance;
        float deviatedMax = startingY + maxAngleDeviance;
        float arcMin = -0.5f * arcSpan;
        float arcMax = 0.5f * arcSpan;
        clampMin = deviatedMin > arcMin ? deviatedMin : arcMin;
        clampMax = deviatedMax < arcMax ? deviatedMax : arcMax;
    }

    private Vector3 CalcRandPosition(float randMin, float randMax, float minDistFromCenter, float maxDistFromCenter, float minHeight, float maxHeight)
    {
        Vector3 euler = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(euler.x, Random.Range(randMin, randMax), euler.z);
        target.localPosition = new Vector3(0, 0, Random.Range(minDistFromCenter, maxDistFromCenter));
        return target.position + Vector3.up * Random.Range(minHeight, maxHeight);
    }

    public void SetArcSpan(float span)
    {
        arcSpan = span;
    }

    public void SetMaxDistance(float distance)
    {
        maxDistFromCenter = distance;
    }
}
