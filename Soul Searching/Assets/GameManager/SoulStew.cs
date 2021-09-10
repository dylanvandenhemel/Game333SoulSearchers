using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulStew : MonoBehaviour
{

    private Quaternion startRotation;
    private float viewReternSpeed = 100;

    private bool bTracker = false;
    private Vector3 orgin;

    RaycastHit hit;
    public LayerMask Mask;
    public LayerMask Wall;
    private Transform target;

    private void Start()
    {
        startRotation = transform.rotation;
        orgin = transform.position;
    }


    private void FixedUpdate()
    {
        if (!bTracker)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, startRotation, viewReternSpeed * Time.deltaTime);
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
                else if(bTracker == true)
                {
                    transform.LookAt(target);
                    Debug.DrawRay(orgin, transform.TransformDirection(Vector3.forward) * 10, Color.green, 1);
                    StewAttack();
                }
                bTracker = true;
            }


        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bTracker = false;
        }

    }


    private void StewAttack()
    {
        Debug.Log("Attacked");
    }
}

