using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MoveFunc
{
    public float circleDistance = 1f;
    public float circleRadius = 1f;

    protected override void GetMoveTarget()
    {
        this.moveTarget = GetRandomPoint() * 0.07f;
    }



    private Vector3 GetRandomPoint()
    {
        float randomRad = Random.Range(0f, 2 * Mathf.PI);
        float resX = Mathf.Cos(randomRad);
        float resY = 0;
        float resZ = Mathf.Sin(randomRad);
        Vector3 res = new Vector3(resX, resY, resZ);
        res *= circleRadius;
        res += transform.forward * circleDistance;
        return res;
    }
}
