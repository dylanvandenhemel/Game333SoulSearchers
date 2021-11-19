using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torchDoor : MonoBehaviour
{
    public GameObject fireEffect;
    private void Update()
    {
        if(transform.parent != null && transform.parent.CompareTag("Door"))
        {
            if(transform.parent.GetComponent<TriggerObjects>().bDoorActive)
            {
                fireEffect.SetActive(true);
            }
            else
            {
                fireEffect.SetActive(false);
            }
        }
    }
}
