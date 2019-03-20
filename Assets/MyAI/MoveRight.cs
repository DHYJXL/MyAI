using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : Steering
{
    public override Vector3 CalculateForce()
    {
        return holder.maxSpeed * Vector3.right - holder.velocity;
    }
}
