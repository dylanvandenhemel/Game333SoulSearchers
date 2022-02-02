using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResetDelegate : MonoBehaviour
{
    public bool bcallReset;
    public static Action Reset = delegate { };
    private const float RESET_WAIT = 1;

    public void Update()
    {
        if(bcallReset)
        {
            StartCoroutine("ResetCoroutine");
            bcallReset = false;
        }
    }

    private IEnumerator ResetCoroutine()
    {
        yield return new WaitForSeconds(RESET_WAIT);
        Reset();
    }
}
