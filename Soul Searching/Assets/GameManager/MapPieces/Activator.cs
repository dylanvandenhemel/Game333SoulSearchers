using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class Activator : MonoBehaviour
{
    private GameObject player;
    public List<GameObject> TriggerObject;

    private bool bPressPlate = false;
    private bool bPressedPlate;
    private bool bstartPPState;
    private int yesBones;

    private bool bLever = false;
    private bool bActiveLever = false;
    private bool bLeverinRange = false;
    private bool delayTimer;

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
        for (int i = 0; i < TriggerObject.Count; i++)
        {
            TriggerObject[i].GetComponent<TriggerObjects>().Trigger();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //For PressPlate
        if(other.gameObject.layer == LayerMask.NameToLayer("Physical") && bPressPlate)
        {
            if(!bPressedPlate)
            {
                GetComponent<AudioSource>().volume = Settings.masterVolumeSet * Settings.sFXVolumeSet;
                GetComponent<AudioSource>().Play();
                Trigger();
                bPressedPlate = true;
            }
            yesBones++;
        }

        //For Lever: Must to be possesed to work
        if(other.CompareTag("Player") && other.GetComponent<Player>().bpossessSkel && bLever)
        {
            player = other.gameObject;
            if (!bLeverinRange)
            {
                other.GetComponent<Player>().pauseMenu.gameObject.GetComponent<UIElements>().LeverUIOn();
                pActions.PlayerActions.Interact.performed += LeverPull;
                bLeverinRange = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //For PressPlate
        if (other.gameObject.layer == LayerMask.NameToLayer("Physical"))
        {
            yesBones--;
            if (bPressedPlate && yesBones <= 0)
            {
                yesBones = 0;
                transform.GetChild(transform.childCount - 1).GetComponent<AudioSource>().volume = Settings.masterVolumeSet * Settings.sFXVolumeSet;
                transform.GetChild(transform.childCount - 1).GetComponent<AudioSource>().Play();
                Trigger();
                bPressedPlate = false;
            }
        }

        //For Lever: Must to be possesed to work
        if (other.CompareTag("Player") && bLever)
        {
            other.GetComponent<Player>().pauseMenu.transform.GetComponent<UIElements>().LeverUIOff();
            bLeverinRange = false;
            pActions.PlayerActions.Interact.performed -= LeverPull;
        }
    }

    private void LeverPull(InputAction.CallbackContext c)
    {
        if(!delayTimer)
        {
            if (player.GetComponent<Player>().bpossessSkel)
            {
                if (!bActiveLever)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y - 90, transform.rotation.z);
                    Trigger();
                    GetComponent<AudioSource>().volume = Settings.masterVolumeSet * Settings.sFXVolumeSet;
                    GetComponent<AudioSource>().Play();
                    transform.GetChild(transform.childCount - 1).GetComponent<VisualEffect>().Play();
                    bActiveLever = true;
                }
                else
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + 90, transform.rotation.z);
                    Trigger();
                    transform.GetChild(transform.childCount - 2).GetComponent<AudioSource>().volume = Settings.masterVolumeSet * Settings.sFXVolumeSet;
                    transform.GetChild(transform.childCount - 2).GetComponent<AudioSource>().Play();
                    bActiveLever = false;
                }
            }
            delayTimer = true;
            StartCoroutine(leverWait());
        }
    }

    IEnumerator leverWait()
    {
        yield return new WaitForSeconds(1);
        delayTimer = false;
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
