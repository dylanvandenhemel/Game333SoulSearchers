using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTower : MonoBehaviour
{
    private Vector3 fwd;

    private int detectorLimit = 0;

    private void Start()
    {
        fwd = transform.TransformDirection(Vector3.forward);
    }

    private void FixedUpdate()
    {
        int LayerMask = 1;

        LayerMask = ~LayerMask;

        if(Physics.Raycast(transform.position, fwd, Mathf.Infinity, LayerMask))
        {
            if(detectorLimit == 1)
            {
                Trigger();
                detectorLimit = 0;
            }
        }
        else
        {
            if(detectorLimit == 0)
            {
                StopTrigger();
                detectorLimit = 1;
            }
        }
    }

    public void Trigger()
    {
        transform.GetComponent<Activator>().Trigger();
    }

    public void StopTrigger()
    {
        transform.GetComponent<Activator>().StopTrigger();
    }
}
