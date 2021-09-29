using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Activator : MonoBehaviour
{
    private GameObject player;
    public GameObject TriggerObject;

    private bool bPressPlate = false;
    private bool bPressedPlate;
    private bool bstartPPState;

    private bool bLever = false;
    private bool bActiveLever = false;
    private bool bLeverinRange = false;

    //Lever Input
    PlayerControls pActions;
    private void OnEnable()
    {
        ResetDelegate.Reset += OnReset;
        pActions = new PlayerControls();
        pActions.Enable();
    }
    private void OnDisable()
    {
        ResetDelegate.Reset -= OnReset;
        pActions.Disable();
    }

    private void Start()
    {


        //Only to allow the on trigger for pressplate
        if (transform.CompareTag("PressPlate"))
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

    private void OnTriggerEnter(Collider other)
    {
        //For PressPlate
        if(other.gameObject.layer == LayerMask.NameToLayer("Physical") && bPressPlate)
        {
            if(!bPressedPlate)
            {
                Trigger();
                bPressedPlate = true;
            }
        }

        //For Lever: Must to be possesed to work
        if(other.CompareTag("Player") && other.GetComponent<Player>().bpossessSkel && bLever)
        {
            player = other.gameObject;
            if (!bLeverinRange)
            {
                other.GetComponent<UIElements>().LeverUIOn();
                pActions.PlayerActions.Interact.performed += LeverPull;
                bLeverinRange = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //For PressPlate
        if (other.gameObject.layer == LayerMask.NameToLayer("Physical") && bPressPlate)
        {
            if (bPressedPlate)
            {
                Trigger();
                bPressedPlate = false;
            }
        }

        //For Lever: Must to be possesed to work
        if (other.CompareTag("Player") || !other.GetComponent<Player>().bpossessSkel && bLever)
        {
            other.GetComponent<UIElements>().LeverUIOff();
            bLeverinRange = false;
            pActions.PlayerActions.Interact.performed -= LeverPull;
        }
    }

    private void LeverPull(InputAction.CallbackContext c)
    {
        if(player.GetComponent<Player>().bpossessSkel)
        {
            if (!bActiveLever)
            {
                Trigger();
                bActiveLever = true;
            }
            else
            {
                Trigger();
                bActiveLever = false;
            }
        }
    }

    public void OnReset()
    {
        //Fixes On/Off
        if(bPressedPlate != bstartPPState)
        {
            Trigger();
            bPressedPlate = bstartPPState;
        }

        if (bActiveLever)
        {
            Trigger();
            bActiveLever = false;
        }
    }
}
