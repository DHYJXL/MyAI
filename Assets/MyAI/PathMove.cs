using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMove : Steering
{
    public Transform pathParrent;
    private Transform[] path;
    public int currentIndex { get; private set; }
    private Vector3 nextPoint;

    public float maxArriveDistance = 0.5f;

    protected override void Awake()
    {
        List<Transform> temp = new List<Transform>();
        base.Awake();
        currentIndex = -1;
        nextPoint = new Vector3();
        if (pathParrent != null && pathParrent.childCount > 0)
        {
            path = new Transform[pathParrent.childCount];
            for (int i = 0; i < pathParrent.childCount; i++)
            {
                path[i] = pathParrent.GetChild(i);
            }
        }
        if (path != null && path.Length > 0)
        {
            nextPoint = path[0].position;
        }
    }

    public override Vector3 CalculateForce()
    {
        if ((transform.position - nextPoint).magnitude < maxArriveDistance)
        {
            currentIndex++;
            if (currentIndex + 1 < path.Length)
            {
                nextPoint = path[currentIndex + 1].position;
            }
            else
            {
                isActive = false;
            }
        }
        Vector3 targetSpeed = (nextPoint - transform.position).normalized * holder.maxSpeed;
        if (currentIndex + 2 < path.Length)//不是在前往最后一个路径点
        {
            return targetSpeed - holder.velocity;
        }
        else//在前往最后一个路径点
        {
            float mul = (nextPoint - transform.position).magnitude / (path[currentIndex].position - nextPoint).magnitude;
            if (mul < 1)
            {
                return targetSpeed * mul - holder.velocity;
            }
            return targetSpeed - holder.velocity;
        }
    }
}
