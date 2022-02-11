using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMovement : MonoBehaviour
{
    public float speed = .1f;
    [HideInInspector] public Transform destination;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, destination.position) >= speed)
            transform.position = Vector3.MoveTowards(transform.position, destination.position, speed);
        else
            Destroy(gameObject);
    }
}