using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResetDelegate : MonoBehaviour
{
    public bool bcallReset;
    public static Action Reset = delegate { };

    public void Update()
    {
        if(bcallReset)
        {
            Reset();
            bcallReset = false;
        }
    }
}
