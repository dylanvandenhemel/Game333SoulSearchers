using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //player
    PlayerControls pActions;
    private CharacterController cController;
    private Vector3 desiredDirection;
    public float speed = 5f;
    public float faceRotationSpeed = 5f;

    //Skeleton Possession
    private Transform currentSkeletonPile;
    private int playerChildCount;
    public bool bpossessSkel = false;
    public float skelSpeed = 3f;
    public float skelfaceRotationSpeed = 4f;

    private void OnEnable()
    {
        pActions = new PlayerControls();
        pActions.Enable();
    }

    private void OnDisable()
    {
        pActions.Disable();
    }

    private void Start()
    {
        cController = GetComponent<CharacterController>();
        playerChildCount = transform.childCount;
    }

    void Update()
    {
        Movement();
    }
    
    private void Movement()
    {
        desiredDirection.x = pActions.PlayerActions.Movement.ReadValue<Vector2>().x;
        desiredDirection.z = pActions.PlayerActions.Movement.ReadValue<Vector2>().y;
        if(!bpossessSkel)
        {
            cController.Move(desiredDirection * Time.deltaTime * speed);

            if (desiredDirection.x != 0 || desiredDirection.z != 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(desiredDirection.x, 0, desiredDirection.z)), Time.deltaTime * faceRotationSpeed);
            }
        }
        else if(bpossessSkel)
        {
            cController.Move(desiredDirection * Time.deltaTime * skelSpeed);

            if (desiredDirection.x != 0 || desiredDirection.z != 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(desiredDirection.x, 0, desiredDirection.z)), Time.deltaTime * skelfaceRotationSpeed);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Get Skeleton Parts
        if(!bpossessSkel)
        {
            if (other.CompareTag("SkeletonPile"))
            {
                currentSkeletonPile = other.transform;
                pActions.PlayerActions.Possess.performed += Possess;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if(!bpossessSkel)
        {
            if (other.CompareTag("SkeletonPile"))
            {
                pActions.PlayerActions.Possess.performed -= Possess;
            }
        }

    }

    private void Possess(InputAction.CallbackContext c)
    {
        
        if(transform.childCount == playerChildCount)
        {
            //Sets pile with character until player unpossesses
            transform.position = currentSkeletonPile.GetChild(0).position;
            currentSkeletonPile.parent = transform;
            currentSkeletonPile.GetComponent<Collider>().enabled = false;
            currentSkeletonPile.GetComponent<MeshRenderer>().enabled = false;

            //Player becomes Skeleton
            bpossessSkel = true;
            transform.GetComponent<MeshRenderer>().enabled = false;
                                                                //This is just to get the player collider out of the way
            //Make sure it is the actual skeleton for gameobject child index
            currentSkeletonPile.GetChild(0).gameObject.SetActive(true);

            //Makes player not be able to move through gates
            gameObject.layer = LayerMask.NameToLayer("Physical");

        }
        else
        {
            currentSkeletonPile.GetComponent<Collider>().enabled = true;
            currentSkeletonPile.GetComponent<MeshRenderer>().enabled = true;
                                //Make sure it is the actual skeleton for gameobject child index
            currentSkeletonPile.GetChild(0).gameObject.SetActive(false);

            //Player control back
            transform.GetComponent<SphereCollider>().center = Vector3.zero;
            bpossessSkel = false;
            transform.GetComponent<Collider>().enabled = true;
            transform.GetComponent<MeshRenderer>().enabled = true;
            transform.GetComponent<Player>().enabled = true;
            currentSkeletonPile.parent = null;
            pActions.PlayerActions.Possess.performed -= Possess;

            //allows player to pass trough walls again
            gameObject.layer = LayerMask.NameToLayer("Phase");
        }
        
    }

}
