using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoorReset : MonoBehaviour
{
    [HideInInspector] public bool bWasActive;
    private void OnEnable()
    {
        ResetDelegate.Reset += OnReset;
    }
    private void OnDisable()
    {
        ResetDelegate.Reset -= OnReset;
    }

    private void OnReset()
    {
        if(transform.GetComponent<TriggerObjects>().bDoorActive)
        {
            bWasActive = true;
        }
        while (transform.GetComponent<TriggerObjects>().NumberofSignalsReqDoor <= 2)
        {
            transform.GetComponent<TriggerObjects>().NumberofSignalsReqDoor++;
            transform.GetComponent<TriggerObjects>().bDoorActive = false;
        }
        StartCoroutine(reset());
    }

    IEnumerator reset()
    {
        yield return new WaitForEndOfFrame();
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("doorClose");
        bWasActive = false;
    }
}
