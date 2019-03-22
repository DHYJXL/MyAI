using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayAvoid : Steering
{
    [Header("射线数量（要偶数）")]
    public int rayCount = 12;
    [Header("视角")]
    public float sightAngle = 90f;
    public int avoidLayer;
    public float wallRayLength = 4f;
    [Header("系数")]
    public float x = 0.35f;


    public override Vector3 CalculateForce()
    {
        Vector3 res = Vector3.zero;
        int hitTimer = 0;
        float angleOffset = sightAngle / (rayCount - 1);

        for (int i = 0; i < rayCount; i++)
        {
            float angle;
            if (i < rayCount / 2)
            {
                angle = angleOffset / 2 + i * angleOffset;
            }
            else
            {
                angle = 360 - (angleOffset / 2 + (i - rayCount / 2) * angleOffset);
            }
            Vector3 target = Quaternion.AngleAxis(angle, Vector3.up) * transform.forward;
            target = target.normalized;
            Ray ray = new Ray(transform.position, target);
            RaycastHit raycastHit;
            bool isHit = Physics.Raycast(ray, out raycastHit, wallRayLength, 1 << avoidLayer);
            if (isHit)
            {
                Vector3 targetVelocity = (transform.position - raycastHit.point).normalized * holder.maxSpeed * wallRayLength * x / (transform.position - raycastHit.point).magnitude;

                res += targetVelocity - holder.velocity;
                hitTimer++;
            }
        }
        if (hitTimer > 0)
        {
            res /= hitTimer;
        }
        return res;
    }
}
