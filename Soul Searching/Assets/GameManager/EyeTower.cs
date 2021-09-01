using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTower : MonoBehaviour
{
    private RaycastHit hit;

    private void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, 6 /*layerMask*/))
        {
            Debug.Log("Touch");
        }
    }

    public void Trigger()
    {
        Debug.Log("Seen");
    }
}
