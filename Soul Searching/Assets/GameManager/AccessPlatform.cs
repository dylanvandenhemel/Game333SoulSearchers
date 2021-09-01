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
        else if(other.CompareTag("Map"))
        {
            transform.parent = null;
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            Debug.Log("ggggg");
        }

    }








}
