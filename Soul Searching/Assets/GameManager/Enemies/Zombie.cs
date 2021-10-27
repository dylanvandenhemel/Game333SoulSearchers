using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public bool bpatrol = false;
    public Transform startingPosition;
    public Transform targetLocation;
    private bool targetReached = false;
    public float zombieRoamSpeed = 2f;
    private float zombieStartSpeed;

    private Transform player;
    private float playerDistance = 10f;
    private bool bTracker = false;
    private Vector3 orgin;
    private Quaternion startRotation;

    RaycastHit hit;
    public LayerMask Mask;
    public LayerMask Wall;
    private Transform target;

    private void OnEnable()
    {
        ResetDelegate.Reset += ActiveReset;
    }

    private void OnDisable()
    {
        ResetDelegate.Reset -= ActiveReset;
    }

    private void Start()
    {
        zombieStartSpeed = zombieRoamSpeed + 1;
        startRotation = transform.rotation;
        if(bpatrol)
        {
            GetComponent<Animator>().SetBool("isPatrol", true);
        }

        //Hides debug cubes
        startingPosition.GetComponent<MeshRenderer>().enabled = false;
        targetLocation.GetComponent<MeshRenderer>().enabled = false;

        //This is to make sure the zombie moves at an even level
        startingPosition.position = new Vector3(startingPosition.position.x, transform.position.y, startingPosition.position.z);
        targetLocation.position = new Vector3(targetLocation.position.x, transform.position.y, targetLocation.position.z);
    }

    private void FixedUpdate()
    {
        //fixes the forces of the zombie to 0;
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //keeps center at center of zombie
        orgin = transform.position;

        //Patrol path
        if (!bTracker && !targetReached && bpatrol)
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
            GetComponent<Animator>().SetBool("isChase", false);
        }
        else if (!bTracker && targetReached && bpatrol)
        {
            //resets zombie speed
            zombieRoamSpeed = Mathf.Clamp(zombieRoamSpeed, zombieStartSpeed, 10);
            zombieRoamSpeed -= 1f;

            transform.position = Vector3.MoveTowards(transform.position, startingPosition.position, zombieRoamSpeed * Time.deltaTime);
            transform.LookAt(startingPosition);
            if (transform.position == startingPosition.position)
            {
                targetReached = false;
            }
            GetComponent<Animator>().SetBool("isChase", false);
        }

        //Zombie sees ghost player
        if(bTracker)
        {
            if (player.GetComponent<Player>().bpossessSkel == true)
            {
                bTracker = false;
                GetComponent<Animator>().SetBool("isChase", false);
            }

            transform.position = Vector3.MoveTowards(transform.position, player.position, zombieRoamSpeed * Time.deltaTime);
            //increases zombie speed in increments
            zombieRoamSpeed = Mathf.Clamp(zombieRoamSpeed, 0, 8f);
            zombieRoamSpeed += 0.1f;
            GetComponent<Animator>().SetBool("isChase", true);
        }
        
    }

    public void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player") && other.GetComponent<Player>().bpossessSkel == false)
        {
            player = other.transform;
            
            if(!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, playerDistance, Wall))
            {
                transform.LookAt(player);
            }

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, Mask))
            {
                playerDistance = hit.distance;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, playerDistance, Wall))
                {
                    bTracker = false;
                }
                else
                {
                    Debug.DrawRay(orgin, transform.TransformDirection(Vector3.forward) * 10, Color.green, 1);
                    bTracker = true;
                    GetComponent<ZombieSound>().ZombieSeesSound();
                }
            
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

    public void KillEnemy()
    {
        //Kill zombie on trap
        GetComponent<Collider>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        bTracker = false;

        GetComponent<ZombieSound>().ZombieDiesSound();
    }

    public void ActiveReset()
    {
        GetComponent<Animator>().SetBool("isChase", false);
        GetComponent<Collider>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.position = startingPosition.position;
        transform.rotation = startRotation;
        bTracker = false;
    }

}
