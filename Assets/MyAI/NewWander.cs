using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWander : Steering
{
    public float circleRadius;
    public float circleDistance;

    [Header("每秒计算次数")]
    public float calculateSpeed;
    private Vector3 targetOffset = new Vector3();

    public override Vector3 CalculateForce()
    {
        return targetOffset * holder.maxSpeed - holder.velocity;
    }

    /// <summary>
    /// 得到此物体到圆上随机一点的单位向量
    /// </summary>
    /// <returns></returns>
    private Vector3 GetRandomPoint()
    {
        float randomRad = Random.Range(0f, 2 * Mathf.PI);
        float resX = Mathf.Cos(randomRad);
        float resY = 0;
        float resZ = Mathf.Sin(randomRad);
        Vector3 res = new Vector3(resX, resY, resZ);
        res *= circleRadius;
        res += transform.forward * circleDistance;
        return res.normalized;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            targetOffset = GetRandomPoint();
            timer = 1 / calculateSpeed;
        }
    }
}
