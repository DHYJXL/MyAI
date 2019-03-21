using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Steering : MonoBehaviour
{
    [Header("权重")]
    public int weight;
    protected Vehicle holder;//持有者
    protected float timer;
    public bool isActive = true;

    //public Vector3 force { get; protected set; }//这个操作计算得到的力

    protected void Awake()
    {
        holder = GetComponent<Vehicle>();
    }

 //   // Update is called once per frame
 //   protected void Update ()
 //   {
 //       timer -= Time.deltaTime;
 //       if (timer < 0)
 //       {
 //           force = CalculateForce();//计算
 //           timer = 1 / calculateSpeed;
 //       }
	//}

    public abstract Vector3 CalculateForce();//得到力

}
