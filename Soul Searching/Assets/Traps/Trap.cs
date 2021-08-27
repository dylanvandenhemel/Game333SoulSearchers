using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Physical"))
        {
            TrapTrigger();
        }
    }

    public void TrapTrigger()
    {
        Debug.Log("Kill");
    }
    
}
