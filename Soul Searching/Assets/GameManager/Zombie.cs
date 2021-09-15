using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    private bool bTracker = false;
    private Vector3 orgin;

    RaycastHit hit;
    public LayerMask Mask;
    public LayerMask Wall;
    private Transform target;

    private void Start()
    {
        orgin = transform.position;
        pointA.GetComponent<MeshRenderer>().enabled = false;
        pointB.GetComponent<MeshRenderer>().enabled = false;
    }


    private void FixedUpdate()
    {
        if (!bTracker)
        {
            //Return Zombie to position
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            
            target = other.transform;
            transform.LookAt(target);
            
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, Mask))
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, Wall))
                {
                    bTracker = false;
                }
                else if (bTracker == true)
                {
                    transform.LookAt(target);
                    Debug.DrawRay(orgin, transform.TransformDirection(Vector3.forward) * 10, Color.green, 1);
                    Debug.Log("Brains");
                }
                bTracker = true;
            
            }
            
            

        }

    }
}
