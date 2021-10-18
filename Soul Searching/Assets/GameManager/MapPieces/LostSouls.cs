using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LostSouls : MonoBehaviour
{ 
    private bool bCollected;

    private Transform player;
    private bool bFollow;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !bFollow)
        {
            player = other.transform;
            bCollected = true;
            bFollow = true;

            player.GetComponent<Player>().collectablesCount++;
        }
    }

    private void Update()
    {
        if(bCollected)
        {
            if(!player.GetComponent<ResetObject>().bUponReset)
            {
                GetComponent<NavMeshAgent>().SetDestination(player.position);
            }
            else
            {
                player.GetComponent<Player>().collectablesCount--;
                GetComponent<NavMeshAgent>().ResetPath();
                bCollected = false;
                bFollow = false;
            }
        }
    }
}
