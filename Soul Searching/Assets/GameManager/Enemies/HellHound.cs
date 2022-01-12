using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HellHound : MonoBehaviour
{
    private Transform player;
    private bool bHeard;
    private NavMeshAgent localMap;
    private void OnEnable()
    {
        ResetDelegate.Reset += ActiveReset;
    }

    private void OnDisable()
    {
        ResetDelegate.Reset -= ActiveReset;
    }
    private void Start()
    {
        localMap = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (bHeard)
        {
            GetComponent<HellHoundSounds>().DogGrowl();
            localMap.SetDestination(player.position);
            if (!player.GetComponent<Player>().bpossessSkel)
            {
                bHeard = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && other.GetComponent<Player>().bwhistling)
        {
            player = other.transform;
            bHeard = true;
        }
        else if(other.CompareTag("Player") && other.GetComponent<Player>().bpossessSkel)
        {
            GetComponent<HellHoundSounds>().DogGrowl();
            player = other.transform;
            bHeard = true;
        }

    }
    public void KillEnemy()
    {
        GetComponent<Collider>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        localMap.ResetPath();
    }

    public void ActiveReset()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Collider>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        localMap.ResetPath();
        StartCoroutine(resetNavMesh());
    }

    //on reset nav mesh agents prevent the object from reseting its location if there is a wall between it
    IEnumerator resetNavMesh()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<NavMeshAgent>().enabled = true;
    }


}
