using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public int targetLayer;
    [Header("探测间隔")]
    public float spacingTime;
    public float distance;
    public List<GameObject> inRangeGos { get; private set; }
    private Collider[] resColliders;

    private LoopCall loopCall;

    private void Awake()
    {
        inRangeGos = new List<GameObject>();
        loopCall = new LoopCall(spacingTime, FindInRange);
    }
    private void Update()
    {
        loopCall.LoopCalling();
    }


    private void FindInRange()
    {
        inRangeGos.Clear();
        resColliders = Physics.OverlapSphere(transform.position, distance, 1 << targetLayer);
        for(int i = 0; i < resColliders.Length; i++)
        {
            if (resColliders[i].GetComponent<Vehicle>() != null && resColliders[i].GetComponent<Vehicle>() != GetComponent<Vehicle>())
            {
                inRangeGos.Add(resColliders[i].gameObject);
            }
        }
    }
}
