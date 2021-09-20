using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessPlatform : MonoBehaviour
{
    private void OnEnable()
    {
        ResetDelegate.Reset += ResetPlatform;
    }
    private void OnDisable()
    {
        ResetDelegate.Reset -= ResetPlatform;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Platform"))
        {
            transform.parent = other.transform;
        }

        if(other.CompareTag("Map"))
        {
            transform.parent = null;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            transform.parent = null;
        }

    }


    public void ResetPlatform()
    {
        transform.parent = null;
    }

}
