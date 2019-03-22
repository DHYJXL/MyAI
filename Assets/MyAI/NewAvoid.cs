using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAvoid : Steering
{
    [Header("躲避物体的Layer")]
    public int targetLayer;
    [Header("视角")]
    public float sightAngle = 30f;

    public float radius = 2f;

    private float x = 0.3f;

    public override Vector3 CalculateForce()
    {
        RaycastHit[] hitInfos = Physics.SphereCastAll(transform.position, radius, Vector3.forward, 0, 1 << targetLayer);
        List<Vector3> resForce = new List<Vector3>();
        if (hitInfos == null || hitInfos.Length == 0)
        {
            return Vector3.zero;
        }
        for (int i = 0; i < hitInfos.Length; i++)
        {

            Vector3 obstacleOffset = hitInfos[i].collider.transform.position - transform.position;
            float rad = Mathf.Acos(Vector3.Dot(obstacleOffset.normalized, transform.forward.normalized));
            float angle = Mathf.Rad2Deg * rad;

            if (angle < sightAngle)
            {
                Debug.Log(angle + hitInfos[i].collider.name);
                resForce.Add(((transform.position - hitInfos[i].collider.transform.position).normalized * holder.maxSpeed - holder.velocity) * radius * x / obstacleOffset.magnitude);
            }
        }
        if (resForce == null || resForce.Count == 0)
        {
            return Vector3.zero;
        }
        Vector3 res = new Vector3();
        for (int i = 0; i < resForce.Count; i++)
        {
            res += resForce[i];
        }

        return res / resForce.Count;
    }

}
      