using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //player
    private Vector3 resetLocation;
    private bool bresetPlayer = false;
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
        resetLocation = transform.position;
        cController = GetComponent<CharacterController>();
        playerChildCount = transform.childCount;
    }

    void Update()
    {
        Movement();
        ResetPlayer();
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

    public void OnTriggerEnter(Collider other)
    {
        //Get Skeleton Parts
        if(!bpossessSkel)
        {
            if (other.CompareTag("SkeletonPile"))
            {
                GetComponent<UIElements>().PossessUIOn();

                currentSkeletonPile = other.transform;
                pActions.PlayerActions.Possess.performed += Possess;
            }
        }

        if(other.CompareTag("DeathBox") && !bpossessSkel)
        {
            //player holds reset manager
            bresetPlayer = true;
            GetComponent<ResetDelegate>().bcallReset = true;
            
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if(!bpossessSkel)
        {
            if (other.CompareTag("SkeletonPile"))
            {
                GetComponent<UIElements>().PossessUIOff();

                pActions.PlayerActions.Possess.performed -= Possess;
            }
        }

    }

    private void Possess(InputAction.CallbackContext c)
    {
        
        if(transform.childCount == playerChildCount)
        {
            //Sets pile with character until player unpossesses
            transform.position = currentSkeletonPile.position;
            transform.rotation = currentSkeletonPile.rotation;
            //currentSkeletonPile.rotation = transform.rotation;
            currentSkeletonPile.parent = transform;

            currentSkeletonPile.GetComponent<Collider>().enabled = false;
            currentSkeletonPile.GetChild(1).GetComponent<MeshRenderer>().enabled = false;

            //Player becomes Skeleton
            bpossessSkel = true;
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            currentSkeletonPile.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
                                                                
                                //Make sure it is the actual skeleton for gameobject child index
            currentSkeletonPile.GetChild(0).gameObject.SetActive(true);

            //Makes player not be able to move through gates
            gameObject.layer = LayerMask.NameToLayer("Physical");

        }
        else
        {
            //currentSkeletonPile.GetChild(1).GetComponent<Collider>().enabled = true;
            currentSkeletonPile.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
                                //Make sure it is the actual skeleton for gameobject child index
            currentSkeletonPile.GetChild(0).gameObject.SetActive(false);

            //Player control back
            bpossessSkel = false;
            currentSkeletonPile.GetComponent<Collider>().enabled = true;
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            currentSkeletonPile.parent = null;
            //resets position and rotation
            currentSkeletonPile.rotation = Quaternion.Euler(0, 180, 0);
            currentSkeletonPile.position = new Vector3(currentSkeletonPile.position.x, currentSkeletonPile.position.y - 0.3f, currentSkeletonPile.position.z);
            pActions.PlayerActions.Possess.performed -= Possess;

            //allows player to pass trough walls again
            gameObject.layer = LayerMask.NameToLayer("Phase");
        }
        
    }

    public void KillSkeleton()
    {
        currentSkeletonPile.GetComponent<Collider>().enabled = true;
        currentSkeletonPile.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
        currentSkeletonPile.GetChild(0).gameObject.SetActive(false);

        //Player control back
        bpossessSkel = false;
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        currentSkeletonPile.parent = null;
        pActions.PlayerActions.Possess.performed -= Possess;

        //allows player to pass trough walls again
        gameObject.layer = LayerMask.NameToLayer("Phase");
    }

    private void ResetPlayer()
    {
        if(bresetPlayer)
        {
            //CController is strict
            cController.enabled = false;
            cController.transform.position = resetLocation;
            if(cController.transform.position == resetLocation)
            {
                cController.enabled = true;
                bresetPlayer = false;
            }
            
        }
    }

}
