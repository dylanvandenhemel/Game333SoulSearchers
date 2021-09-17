using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Activator : MonoBehaviour
{
    public GameObject TriggerObject;

    private bool bPressPlate = false;

    private bool bLever = false;
    private bool bActiveLever = false;
    private bool bLeverinRange = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Physical") && bPressPlate)
        {
            Trigger();
        }
        //Must to be possesed to work
        if(other.CompareTag("Player") && other.GetComponent<Player>().bpossessSkel && bLever)
        {
            //double enter bug fix
            if (!bLeverinRange)
            {
                pActions.PlayerActions.Interact.performed += LeverPull;
                bLeverinRange = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Physical") && bPressPlate)
        {
            Trigger();
        }

        if (other.CompareTag("Player") && other.GetComponent<Player>().bpossessSkel && bLever)
        {
            bLeverinRange = false;
            pActions.PlayerActions.Interact.performed -= LeverPull;
        }
    }

    private void LeverPull(InputAction.CallbackContext c)
    {
        if(!bActiveLever)
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
