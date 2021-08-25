using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //player
    private CharacterController cController;
    private Vector3 desiredDirection;
    public float speed = 5f;
    public float faceRotationSpeed = 5f;


    PlayerControls pActions;
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
}
