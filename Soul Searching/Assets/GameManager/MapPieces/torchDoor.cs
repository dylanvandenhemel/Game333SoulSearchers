using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torchDoor : MonoBehaviour
{
    public GameObject fireEffect;
    private void Update()
    {
        if(transform.parent != null)
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
