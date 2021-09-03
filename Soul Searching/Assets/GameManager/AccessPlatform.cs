using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessPlatform : MonoBehaviour
{
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

}
