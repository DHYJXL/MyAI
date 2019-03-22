using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Radar))]
public class KeepDistance : Steering
{
    private List<GameObject> neighborhoods = new List<GameObject>();
    [Header("系数")]
    public float x = 3f;

    public override Vector3 CalculateForce()
    {
        Vector3 res = Vector3.zero;
        neighborhoods = GetComponent<Radar>().inRangeGos;
        if (neighborhoods == null || neighborhoods.Count == 0)
        {
            return Vector3.zero;
        }
            
        for (int i = 0; i < neighborhoods.Count; i++)
        {
            Vector3 targetVelocity = (transform.position - neighborhoods[i].transform.position).normalized * holder.maxSpeed
                * GetComponent<Radar>().distance * x / (transform.position - neighborhoods[i].transform.position).magnitude
                ;
            res += targetVelocity - holder.velocity;
        }
        res /= neighborhoods.Count;
        return res;
    }


}
