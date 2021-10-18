using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulStew : MonoBehaviour
{
    //weird bug where if player stands still and unpossesses they will not die until they move

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
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3, Mask))
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3, Wall))
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
        target.GetComponent<Player>().bresetPlayer = true;
        target.GetComponent<ResetDelegate>().bcallReset = true;
    }
}

