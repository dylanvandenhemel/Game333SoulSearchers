using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LostSouls : MonoBehaviour
{ 
    private bool bCollected;

    public GameObject babySkel;

    private Transform player;
    private bool bFollow;

    private NavMeshAgent localMap;

    private void Start()
    {
        //default value is 0.5 so this will randomize near that range
        float newStopDistance = Random.Range(0.1f, 1f);

        //default value is 6 so this will randomize near that range
        float newSpeed = Random.Range(4, 8);

        GetComponent<NavMeshAgent>().stoppingDistance = newStopDistance;
        GetComponent<NavMeshAgent>().speed = newSpeed;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player = other.transform;
            bCollected = true;

            if(!bFollow)
            {
                if (player.GetComponent<Player>().bpossessSkel)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    babySkel.SetActive(true);
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                    babySkel.SetActive(false);
                }
            }

            if (GetComponent<NavMeshAgent>().remainingDistance > 0.5f)
            {
                babySkel.GetComponent<Animator>().SetBool("bSkelWalk", true);
            }
            else
            {
                babySkel.GetComponent<Animator>().SetBool("bSkelWalk", false);
            }
            
            player.GetComponent<Player>().collectablesCount++;
            bFollow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            bFollow = false;
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

    public void ActiveReset()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        localMap.ResetPath();
        StartCoroutine(resetNavMesh());
    }

    //on reset nav mesh agents prevent the object from reseting its location if there is a wall between it
    IEnumerator resetNavMesh()
    {
        yield return new WaitForEndOfFrame();
        GetComponent<NavMeshAgent>().enabled = true;
    }
}
