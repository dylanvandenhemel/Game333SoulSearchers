using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LostSouls : MonoBehaviour
{
    private bool bCollected;

    private Transform player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player = other.transform;
            bCollected = true;
        }
    }

    private void Update()
    {
        if(bCollected)
        {
            GetComponent<NavMeshAgent>().SetDestination(player.position);
        }
    }
}
