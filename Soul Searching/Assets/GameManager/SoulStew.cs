using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulStew : MonoBehaviour
{
    public LayerMask Mask;
    private Vector3 fwd;

    private void Start()
    {
        fwd = transform.TransformDirection(Vector3.forward);
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, fwd, Mathf.Infinity, Mask))
        {
            Debug.Log("Player in Zone");
        }
        
    }
}
