using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [Header("每秒计算次数")]
    public float calculateSpeed;
    public float mass = 1;//物体质量
    public float maxForce;//物体受力最大值
    public float maxSpeed;//物体速度最大值
    public float damping;//转向速度
    private Steering[] steerings;//操控行为列表

    private Vector3 steeringForce;//受力
    public Vector3 velocity;//速度
    private Vector3 acceleration;//加速度

    private float timer = 0;

    private void Awake()
    {
        steerings = GetComponents<Steering>();
    }

    // Update is called once per frame
    void Update ()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            steeringForce = Vector3.zero;
            int totalWeight = 0;
            for (int i = 0; i < steerings.Length; i++)
            {
                if (steerings[i].isActive)
                {
                    steeringForce += steerings[i].CalculateForce() * steerings[i].weight;
                    totalWeight += steerings[i].weight;
                }
            }
            if (totalWeight > 0)
            {
                steeringForce /= totalWeight;
            }
            if (steeringForce.magnitude > maxForce)
            {
                steeringForce = steeringForce.normalized * maxForce;
            }
            acceleration = steeringForce / mass;
            timer = 1 / calculateSpeed;
        }

       
    }

    private void FixedUpdate()
    {
        velocity += acceleration * Time.fixedDeltaTime;
        Vector3 moveDistance = velocity * Time.fixedDeltaTime;

        transform.position += moveDistance;
        if (velocity.magnitude > 0.001f)
        {
            Vector3 newForward = Vector3.Slerp(transform.forward, velocity, damping * Time.fixedDeltaTime);
            transform.forward = newForward;
        }
    }
}
