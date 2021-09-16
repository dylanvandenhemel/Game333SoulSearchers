using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public Transform startingPosition;
    public Transform targetLocation;
    private bool targetReached = false;
    public float zombieRoamSpeed = 2f;
    private float zombieStartSpeed;

    private Transform player;
    private bool bTracker = false;
    private Vector3 orgin;

    RaycastHit hit;
    public LayerMask Mask;
    public LayerMask Wall;
    private Transform target;

    private void Start()
    {
        zombieStartSpeed = zombieRoamSpeed + 1;
    }

    private void FixedUpdate()
    {
        //keeps center at center of zombie
        orgin = transform.position;

        //Patrol path
        if (!bTracker && !targetReached)
        {
            //resets zombie speed
            zombieRoamSpeed = Mathf.Clamp(zombieRoamSpeed, zombieStartSpeed, 10);
            zombieRoamSpeed -= 1f;

            transform.position = Vector3.MoveTowards(transform.position, targetLocation.position, zombieRoamSpeed * Time.deltaTime);
            transform.LookAt(targetLocation);
            if (transform.position == targetLocation.position)
            {
                targetReached = true;
            }
        }
        else if (!bTracker && targetReached)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPosition.position, zombieRoamSpeed * Time.deltaTime);
            transform.LookAt(startingPosition);
            if (transform.position == startingPosition.position)
            {
                targetReached = false;
            }
        }

        //Zombie sees ghost player
        if(bTracker)
        {
            if(player.GetComponent<Player>().bpossessSkel == true)
            {
                bTracker = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, player.position, zombieRoamSpeed * Time.deltaTime);
            //increases zombie speed in increments
            zombieRoamSpeed = Mathf.Clamp(zombieRoamSpeed, 0, 5.2f);
            zombieRoamSpeed += 0.15f;
        }
        
    }

    public void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player") && other.GetComponent<Player>().bpossessSkel == false)
        {
            player = other.transform;
            
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
                    Debug.DrawRay(orgin, transform.TransformDirection(Vector3.forward) * 10, Color.green, 1);
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

}
