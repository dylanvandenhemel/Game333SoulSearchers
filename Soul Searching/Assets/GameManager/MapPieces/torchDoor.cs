using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torchDoor : MonoBehaviour
{
    public GameObject fireEffect;
    public int signalTurnOn;
    public bool openDoorTorch;

    private void Start()
    {
        if (transform.parent != null && transform.parent.CompareTag("Door"))
        {
            fireEffect.SetActive(false);
        }
        else
        {
            fireEffect.SetActive(true);
        }
    }
    private void Update()
    {
        if (transform.parent != null && transform.parent.CompareTag("Door"))
        {
            if (transform.parent.GetComponent<TriggerObjects>().NumberofSignalsReqDoor == signalTurnOn && !openDoorTorch)
            {
                fireEffect.SetActive(true);
            }
            else if (transform.parent.GetComponent<TriggerObjects>().NumberofSignalsReqDoor != signalTurnOn && !openDoorTorch)
            {
                fireEffect.SetActive(false);
            }
            //turns on with door
            if (transform.parent.GetComponent<TriggerObjects>().bDoorActive && openDoorTorch)
            {
                fireEffect.SetActive(true);
            }
            else if (!transform.parent.GetComponent<TriggerObjects>().bDoorActive && openDoorTorch)
            {
                fireEffect.SetActive(false);
            }

        }
    }
}
