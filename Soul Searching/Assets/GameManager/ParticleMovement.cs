using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParticleMovement : MonoBehaviour
{
    private int currentTargetVal;
    private Vector3 currentTarget;
    private Transform[] savedPath;
    private Transform endPathTrigger;
    private bool bTraveling;
    private bool bEndPath;
    [HideInInspector] public List<GameObject> particleList;

    public void Path(Transform[] path, Transform endPath)
    {
        savedPath = path;
        endPathTrigger = endPath;
        currentTarget = path[currentTargetVal].position;
        bTraveling = true;
        
        if(bEndPath)
        {
            currentTarget = endPath.position;
        }       
        
    }

    void Update()
    {
        if (bTraveling)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, 4 * Time.deltaTime);
            if (transform.position == currentTarget)
            {
                if(savedPath.Length - 1 > currentTargetVal)
                {
                    currentTargetVal++;
                }
                else
                {
                    bEndPath = true;
                }
                bTraveling = false;
                Path(savedPath, endPathTrigger);
            }
        }
        
        if (bEndPath && transform.position == endPathTrigger.position)
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
