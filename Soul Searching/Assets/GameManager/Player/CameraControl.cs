using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    PlayerControls pActions;
    private Vector3 cameraPan;
    public CinemachineVirtualCamera thisCamera;

    private Vector3 startVCOffset;

    private Vector3 currentVCam;

    private bool bReturn;

    private void OnEnable()
    {
        pActions = new PlayerControls();
        pActions.Enable();

        //pActions.PlayerActions.CameraMovement.performed += CameraOperation;
        pActions.PlayerActions.CameraMovement.canceled += CameraReturn;
    }

    private void OnDisable()
    {
        pActions.Disable();
        //pActions.PlayerActions.CameraMovement.performed -= CameraOperation;
        pActions.PlayerActions.CameraMovement.canceled -= CameraReturn;
    }

    private void Start()
    {
        startVCOffset = thisCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
    }

    private void Update()
    {
        cameraPan.x = pActions.PlayerActions.CameraMovement.ReadValue<Vector2>().x;
        cameraPan.y = pActions.PlayerActions.CameraMovement.ReadValue<Vector2>().y;

        currentVCam = thisCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        
        if(!bReturn)
        {
            if (cameraPan.x >= 0.5f)
            {
                //currentVCam.x += Mathf.Clamp(currentVCam.x, 0, startVCOffset.x + 2) * 4 * Time.deltaTime;
            }
            if (cameraPan.x <= -0.5f)
            {
                //currentVCam.x -= 4 * Time.deltaTime;
            }
            if (cameraPan.y >= 0.5f)
            {
                //currentVCam.z += 4 * Time.deltaTime;
            }
            if (cameraPan.y <= -0.5f)
            {
               //currentVCam.z -= 4 * Time.deltaTime;
            }
            thisCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = currentVCam;
        }
        else
        {
            //thisCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.MoveTowards
        }
       

        
    }

    private void CameraReturn(InputAction.CallbackContext c)
    {
        //Debug.LogError("stop");
        //bReturn = true;
    }
}
