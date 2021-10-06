using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HellHound : MonoBehaviour
{
    private Transform player;
    private bool bHeard;
    private NavMeshAgent localMap;

    private void Start()
    {
        localMap = GetComponent<NavMeshAgent>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && other.GetComponent<Player>().bwhistling)
        {
            player = other.transform;
            Debug.Log("Bark");
            bHeard = true;
        }
    }

    private void Update()
    {
        if(bHeard)
        {
            localMap.SetDestination(player.position);
            bHeard = false;
        }
    }








}
