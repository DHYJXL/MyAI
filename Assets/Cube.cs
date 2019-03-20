using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    //public float wallRayLength = 3f;
    //public float wallWeight = 1f;
    public float maxSpeed = 5f;
    public float minSpeed = 0.5f;

    public float rotateSpeed = 3f;

    private Vector3 moveTarget;

    // Use this for initialization
    private void Awake()
    {
        moveTarget = transform.forward * 3;
        //Debug.Log(transform.forward);
    }
    void Start ()
    {
      
        //InvokeRepeating("CheckWall", 0, 0.1f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetMoveTargetFromFunc();

        LookAt(moveTarget + transform.position);

        //transform.Translate(moveTarget * Time.deltaTime * 2, Space.World);
        //transform.rotation = Quaternion.LookRotation(moveTarget);

        if (moveTarget.magnitude > maxSpeed)
        {
            moveTarget = moveTarget.normalized * maxSpeed;
        }
        else if (moveTarget.magnitude < minSpeed)
        {
            moveTarget = moveTarget.normalized * minSpeed;
        }
        transform.Translate(new Vector3(0, 0, moveTarget.magnitude * Time.deltaTime));
    }

    private void GetMoveTargetFromFunc()
    {
        Vector3 offset = Vector3.zero;
        MoveFunc[] moveFuncs = GetComponents<MoveFunc>();
        float totalWeight = 0;
        if (moveFuncs.Length > 0)
        {
            for (int i = 0; i < moveFuncs.Length; i++)
            {
                if (moveFuncs[i].moveTarget != Vector3.zero)
                {
                    totalWeight += moveFuncs[i].weight;
                    offset += (moveFuncs[i].moveTarget * moveFuncs[i].weight);
                }
            }
            offset /= totalWeight;
        }
        moveTarget += offset;
    }

    public void LookAt(Vector3 target)
    {
        Vector3 targetPos = target;
        targetPos.y = 0;
        float angle = GetAngle(transform.forward, targetPos - transform.position, Vector3.up);

        transform.Rotate(Vector3.up, angle * rotateSpeed * Time.deltaTime);
    }

    //private void OnDrawGizmos()
    //{
    //    CheckWall();
    //}

    //private void CheckWall()
    //{

    //    Vector3 res = Vector3.zero;
    //    int hitTimer = 0;

    //    for (int i = 0; i < 12; i++)
    //    {
    //        float angle = 30f * i;
    //        Vector3 target = Quaternion.AngleAxis(angle, Vector3.up) * transform.forward;
    //        target = target.normalized;
    //        Ray ray = new Ray(transform.position, target);
    //        RaycastHit raycastHit;
    //        bool isHit = Physics.Raycast(ray, out raycastHit, wallRayLength, 1 << 4);
    //        if (isHit)
    //        {
    //            Vector3 hitVector = transform.position - raycastHit.point;
    //            hitVector = hitVector.normalized / hitVector.magnitude;
    //            res += hitVector;
    //            hitTimer++;
    //        }
    //    }
    //    if (hitTimer > 0)
    //    {
    //        res /= hitTimer;
    //    }
    //    Debug.Log(hitTimer);
    //    moveTarget += res * wallWeight;
    //}

    /// <summary>
    /// 返回垂直指定轴平面两向量的角度（包含符号）
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public float GetAngle(Vector3 v1, Vector3 v2, Vector3 axis)
    {
        return Mathf.Atan2(
            Vector3.Dot(axis, Vector3.Cross(v1, v2)),
            Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
    }
}
