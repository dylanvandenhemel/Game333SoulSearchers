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

        pActions.PlayerActions.CameraMovement.canceled += CameraReturn;
    }

    private void OnDisable()
    {
        pActions.Disable();
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

        if (!bReturn)
        {
            if (cameraPan.x >= 0.2f)
            {
                currentVCam.x += 6 * Time.deltaTime;
            }
            if (cameraPan.x <= -0.2f)
            {
                currentVCam.x -= 6 * Time.deltaTime;
            }
            if (cameraPan.y >= 0.2f)
            {
                currentVCam.z += 6 * Time.deltaTime;
            }
            if (cameraPan.y <= -0.2f)
            {
                currentVCam.z -= 6 * Time.deltaTime;
            }
            thisCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x = Mathf.Clamp(currentVCam.x, startVCOffset.x - 2, startVCOffset.x + 2);
            thisCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = Mathf.Clamp(currentVCam.z, startVCOffset.z - 2, startVCOffset.z + 2);
        }
        else
        {
            //Moves camera back to neutral position
            thisCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.MoveTowards(currentVCam, startVCOffset, 7 * Time.deltaTime);
            if (thisCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset == startVCOffset)
            {
                bReturn = false;
            }
        }


    }

    //this is called when the buttons are let go
    private void CameraReturn(InputAction.CallbackContext c)
    {
        bReturn = true;
    }
}
