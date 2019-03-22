using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopCall
{
    private float timer = 0;
    private float callSpacing;
    private Action call;
    public LoopCall(float callSpacing, Action call)
    {
        this.callSpacing = callSpacing;
        this.call = call;
    }

    public void LoopCalling()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (call != null)
            {
                call();
            }
            timer = callSpacing;
        }
    }
}
