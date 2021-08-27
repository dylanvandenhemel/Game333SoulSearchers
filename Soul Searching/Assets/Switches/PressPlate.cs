using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressPlate : MonoBehaviour
{
    public GameObject TriggerObject;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Physical"))
        {
            TriggerObject.GetComponent<Trap>().TrapTrigger();
        }
    }
}
