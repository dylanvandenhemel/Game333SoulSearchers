using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Activator : MonoBehaviour
{
    public GameObject TriggerObject;

    private bool bPressPlate = false;

    private bool bLever = false;
    private bool bLeverActive = false;

    //Lever Input
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
        //Only to allow the on trigger for pressplate
        if(transform.CompareTag("PressPlate"))
        {
            bPressPlate = true;
        }
        else if(transform.CompareTag("Lever"))
        {
            bLever = true;
        }
    }

    public void Trigger()
    {
        TriggerObject.GetComponent<TriggerObjects>().Trigger();
    }
    public void StopTrigger()
    {
        TriggerObject.GetComponent<TriggerObjects>().StopTrigger();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Physical") && bPressPlate)
        {
            Trigger();
        }
        //Must to be possesed to work
        if(other.CompareTag("Player") && other.GetComponent<Player>().bpossessSkel && bLever)
        {
            pActions.PlayerActions.Interact.performed += LeverPull;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Physical") && bPressPlate)
        {
            StopTrigger();
        }

        if (other.CompareTag("Player") && other.GetComponent<Player>().bpossessSkel && bLever)
        {
            pActions.PlayerActions.Interact.performed -= LeverPull;
        }
    }

    private void LeverPull(InputAction.CallbackContext c)
    {
        if(!bLeverActive)
        {
            Trigger();
            bLeverActive = true;
        }
        else
        {
            StopTrigger();
            bLeverActive = false;
        }

    }
}
