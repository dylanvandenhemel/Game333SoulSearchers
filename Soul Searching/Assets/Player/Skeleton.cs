using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Skeleton : MonoBehaviour
{
    //player as skeletom
    PlayerControls pActions;
    private CharacterController cController;
    private Vector3 desiredDirection;
    public float speed = 3f;
    public float faceRotationSpeed = 4f;

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

    //Make skeleton move

}
