using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveFunc : MonoBehaviour
{
    public Vector3 moveTarget { get; protected set; }
    public float weight;
    public float calculateTimer = 50;
    protected float interval { get { return 1f / calculateTimer; } }

    protected void Awake()
    {
        moveTarget = Vector3.zero;
    }
    protected void Start()
    {
        InvokeRepeating("GetMoveTarget", 0, interval);
    }
    protected abstract void GetMoveTarget();
}
