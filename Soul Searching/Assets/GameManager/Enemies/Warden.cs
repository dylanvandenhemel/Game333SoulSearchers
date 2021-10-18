using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Warden : MonoBehaviour
{
    private GameObject target;
    private NavMeshAgent localMap;

    private void Start()
    {
        localMap = GetComponent<NavMeshAgent>();
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Zombie") || other.CompareTag("HellHound"))
        {
            target = other.gameObject;
            Debug.Log("in range " + target.name);
            localMap.SetDestination(target.transform.position);

        }

        if (GetComponent<ResetObject>().bUponReset)
        {
            localMap.ResetPath();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Zombie") || other.CompareTag("HellHound"))
        {
            localMap.ResetPath();
        }
    }

}
