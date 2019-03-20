using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid : MoveFunc
{
    public float wallRayLength = 4f;
    public int rayCount = 20;

    protected override void GetMoveTarget()
    {
        Vector3 res = Vector3.zero;
        int hitTimer = 0;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = (360 / rayCount) * i;
            Vector3 target = Quaternion.AngleAxis(angle, Vector3.up) * transform.forward;
            target = target.normalized;
            Ray ray = new Ray(transform.position, target);
            RaycastHit raycastHit;
            bool isHit = Physics.Raycast(ray, out raycastHit, wallRayLength, 1 << 4);
            if (isHit)
            {
                Vector3 hitVector = transform.position - raycastHit.point;
                hitVector = hitVector.normalized / (hitVector.magnitude / 0.07f);
                res += hitVector;
                hitTimer++;
                Debug.Log("/" + res);
            }
        }
        if (hitTimer > 0)
        {
            res /= hitTimer;
        }
        this.moveTarget = res;
    }

}
