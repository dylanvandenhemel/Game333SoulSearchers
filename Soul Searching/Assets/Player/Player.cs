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
        cController.Move(desiredDirection * Time.deltaTime * speed);

        if (desiredDirection.x != 0 || desiredDirection.z != 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(desiredDirection.x, 0, desiredDirection.z)), Time.deltaTime * faceRotationSpeed);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Skeleton"))
        {
            currentSkeletonPile = other.transform;
            pActions.PlayerActions.Possess.performed += Possess;   
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Skeleton"))
        {
            Debug.Log("untouch");
            pActions.PlayerActions.Possess.performed -= Possess;
        }
    }

    private void Possess(InputAction.CallbackContext c)
    {
        
        if(transform.childCount == playerChildCount)
        {
            //Sets pile with character until player unpossesses
            currentSkeletonPile.parent = transform;
            currentSkeletonPile.GetComponent<Collider>().enabled = false;
            currentSkeletonPile.GetComponent<MeshRenderer>().enabled = false;

            //Player becomes Skeleton
            

        }
        else
        {
            currentSkeletonPile.GetComponent<Collider>().enabled = true;
            currentSkeletonPile.GetComponent<MeshRenderer>().enabled = true;
            //Debug.Log("No longer Skeleton");
            currentSkeletonPile.parent = null;
            pActions.PlayerActions.Possess.performed -= Possess;
        }
        
    }
}
