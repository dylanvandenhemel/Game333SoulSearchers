using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    private bool bPressPlate;
    public GameObject TriggerObject;

    private void Start()
    {
        //Only to allow the on trigger for pressplate
        if(transform.CompareTag("PressPlate"))
        {
            bPressPlate = true;
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
    }
}
