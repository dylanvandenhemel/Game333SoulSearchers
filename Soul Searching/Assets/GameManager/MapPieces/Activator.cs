using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class Activator : MonoBehaviour
{
    private GameObject player;
    public bool bParticalOn = true;
    public Transform[] particalPath;
    public Transform particalPathEndGoal;
    public List<GameObject> TriggerObject;
    public GameObject powerParticle;
    [HideInInspector] public List<GameObject> particleList;

    private bool bPressPlate = false;
    private bool bPressedPlate;
    private bool bstartPPState;
    private int yesBones;
    private bool bHasBones;
    private bool bFixPress;

    private bool bSinglePressPlate;

    private bool bLever = false;
    private bool bActiveLever = false;
    private bool bLeverinRange = false;
    private bool delayTimer;
    public bool bDoesNotReset;

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
        else if(transform.CompareTag("SinglePressPlate"))
        {
            bSinglePressPlate = true;
        }
        else if(transform.CompareTag("Lever"))
        {
            bLever = true;
        }
        particleList = new List<GameObject>();
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
        if((other.gameObject.layer == LayerMask.NameToLayer("Physical") || other.gameObject.layer == LayerMask.NameToLayer("Bones(Exclusive)")) && bPressPlate)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Bones(Exclusive)"))
            {
                yesBones++;
            }
            if (!bPressedPlate)
            {
                //Partical effect
                float height = transform.CompareTag("Lever") ? 1.5f : 1;
                for (int i = 0; i < TriggerObject.Count; i++)
                {
                    if (particleList.Count == 0 && bParticalOn)
                    {
                        GameObject particle = Instantiate(powerParticle, new Vector3(transform.position.x, transform.position.y + height, transform.position.z), transform.rotation);
                        //particle.GetComponent<ParticleMovement>().destination = TriggerObject[i].transform;
                        particle.GetComponent<ParticleMovement>().Path(particalPath, particalPathEndGoal);
                        particle.GetComponent<ParticleMovement>().particleList = particleList;
                        particleList.Add(particle);
                    }
                }

                bPressedPlate = true;
                transform.GetChild(0).gameObject.SetActive(false);
                GetComponent<AudioSource>().volume = Settings.masterVolumeSet * Settings.sFXVolumeSet;
                GetComponent<AudioSource>().Play();
                Trigger();
            }
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Physical") && bSinglePressPlate)
        {
            if (!bPressedPlate)
            {
                bPressedPlate = true;
                transform.GetChild(0).gameObject.SetActive(false);
                GetComponent<AudioSource>().volume = Settings.masterVolumeSet * Settings.sFXVolumeSet;
                GetComponent<AudioSource>().Play();
                Trigger();
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        //For Lever: Must to be possesed to work
        if (other.CompareTag("Player") && other.GetComponent<Player>().bpossessSkel && bLever)
        {
            player = other.gameObject;
            if (!bLeverinRange)
            {
                other.GetComponent<Player>().pauseMenu.gameObject.GetComponent<UIElements>().LeverUIOn();
                pActions.PlayerActions.Interact.performed += LeverPull;
                bLeverinRange = true;
            }
        }
        if (other.CompareTag("Player") && !other.GetComponent<Player>().bpossessSkel && bLever)
        {
            other.GetComponent<Player>().pauseMenu.transform.GetComponent<UIElements>().LeverUIOff();
            bLeverinRange = false;
            pActions.PlayerActions.Interact.performed -= LeverPull;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //For PressPlate
        if ((other.gameObject.layer == LayerMask.NameToLayer("Physical") || other.gameObject.layer == LayerMask.NameToLayer("Bones(Exclusive)")) && (bPressedPlate && bPressPlate))
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Bones(Exclusive)") && yesBones > 0)
            {
                yesBones--;
                //when removing a single signal from a double door
                for (int j = 0; j < TriggerObject.Count; j++)
                {
                    if (TriggerObject[j].GetComponent<TriggerObjects>().NumberofSignalsReqDoor == 1)
                    {
                        if (!TriggerObject[j].GetComponent<TriggerObjects>().bDoorActive)
                        {
                            //plus 2 to fix amount when exiting
                            TriggerObject[j].GetComponent<TriggerObjects>().NumberofSignalsReqDoor += 2;
                        }
                    }
                }
                //
            }
            if (bPressedPlate && yesBones == 0)
            {
                bPressedPlate = false;
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(transform.childCount - 1).GetComponent<AudioSource>().volume = Settings.masterVolumeSet * Settings.sFXVolumeSet;
                transform.GetChild(transform.childCount - 1).GetComponent<AudioSource>().Play();
                Trigger();
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
                    //Partical effect
                    float height = transform.CompareTag("Lever") ? 1.5f : 1;
                    for (int i = 0; i < TriggerObject.Count; i++)
                    {
                        if (particleList.Count == 0 && bParticalOn)
                        {
                            GameObject particle = Instantiate(powerParticle, new Vector3(transform.position.x, transform.position.y + height, transform.position.z), transform.rotation);
                            //particle.GetComponent<ParticleMovement>().destination = TriggerObject[i].transform;
                            particle.GetComponent<ParticleMovement>().Path(particalPath, particalPathEndGoal);
                            particle.GetComponent<ParticleMovement>().particleList = particleList;
                            particleList.Add(particle);
                        }
                    }

                    transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y - 90, transform.rotation.z);
                    Trigger();
                    GetComponent<AudioSource>().volume = Settings.masterVolumeSet * Settings.sFXVolumeSet;
                    GetComponent<AudioSource>().Play();
                    transform.GetChild(transform.childCount - 1).GetComponent<VisualEffect>().Play();
                    bActiveLever = true;
                }
                else
                {
                    //when removing a single signal from a double door
                    for (int j = 0; j < TriggerObject.Count; j++)
                    {
                            if (TriggerObject[j].GetComponent<TriggerObjects>().NumberofSignalsReqDoor == 1)
                            {
                                if (!TriggerObject[j].GetComponent<TriggerObjects>().bDoorActive)
                                {
                                    //plus 2 to fix amount when exiting
                                    TriggerObject[j].GetComponent<TriggerObjects>().NumberofSignalsReqDoor += 2;
                                }
                            }
                    }
                    Trigger();

                    transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + 90, transform.rotation.z);
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
        yield return new WaitForSeconds(0.4f);
        delayTimer = false;
    }

    public void OnReset()
    {
        if(!bDoesNotReset)
        {
            //Fixes On/Off
            if (bPressedPlate != bstartPPState)
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
}
