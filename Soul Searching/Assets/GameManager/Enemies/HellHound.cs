using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HellHound : MonoBehaviour
{
    private Transform player;
    private bool bHeard, bWaiting = false, bPulse = false;
    private float distance, timer = 0;
    private NavMeshAgent localMap;
    public GameObject enemyDeath;
    public GameObject whistleObject;
    //These values are derived from experimentation, should be refined
    private const float WHISTLE_DISTANCE = 11.2f/1.2f;
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
        //Just so there's no errors
        player = transform;
        localMap = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (bWaiting)
            timer += Time.deltaTime;
        if (bWaiting && timer >= distance/WHISTLE_DISTANCE)
        {
            if (!bPulse)
                StartCoroutine("Pulse");
            timer = 0;
            bHeard = true;
            bWaiting = false;
        }
        if (bHeard)
        {
            GetComponent<HellHoundSounds>().DogGrowl();
            localMap.SetDestination(player.position);
            if (!player.GetComponent<Player>().bpossessSkel)
            {
                bHeard = false;
            }
        }
        if(GetComponent<NavMeshAgent>().velocity.magnitude > 1.5f)
        {
            GetComponentInChildren<Animator>().SetBool("isChase", true);
        }
        else
        {
            GetComponentInChildren<Animator>().SetBool("isChase", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && other.GetComponent<Player>().bwhistling)
        {
            //If you want to change the dog so he immediatly rushes upon hearing whistle instead of when touched by effects, do this
            //Toggle bHeard here, store the player's position, and then dynamically calculate distance every frame (because the hound is now moving)
            //Then it would still have the dog effect at the correct time
            player = other.transform;
            distance = Vector3.Distance(transform.position, player.position);
            bWaiting = true;
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
        Instantiate(enemyDeath, transform.position, transform.rotation);
        localMap.ResetPath();
    }

    public void ActiveReset()
    {
        GetComponent<Collider>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        localMap.ResetPath();
        StartCoroutine(resetNavMesh());
    }

    //on reset nav mesh agents prevent the object from reseting its location if there is a wall between it
    IEnumerator resetNavMesh()
    {
        yield return new WaitForEndOfFrame();
        GetComponent<NavMeshAgent>().enabled = true;
    }

    private IEnumerator Pulse()
    {
        Instantiate(whistleObject, transform);
        bPulse = true;
        yield return new WaitForSeconds(1.2f);
        bPulse = false;
    }
}
