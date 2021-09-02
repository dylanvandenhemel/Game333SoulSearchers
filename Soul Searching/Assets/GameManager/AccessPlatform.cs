using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Platform"))
        {
            Debug.Log("Platform");
            transform.parent = other.transform;
        }

        if(other.CompareTag("Map"))
        {
            Debug.Log("Touched Map");
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

}
