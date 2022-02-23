using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParticleMovement : MonoBehaviour
{
    private float endDistance = 0.8f;
    private NavMeshAgent localMap;
    [HideInInspector] public Transform destination;
    [HideInInspector] public List<GameObject> particleList;

    private void Start()
    {
        localMap = GetComponent<NavMeshAgent>();
        localMap.SetDestination(destination.position);
    }
    void Update()
    {
        if (localMap.remainingDistance <= endDistance)
        {
            foreach (GameObject particle in particleList)
            {
                if (particle == gameObject)
                    particleList.Remove(particle);
            }
            Destroy(gameObject);

        }
    }
}
