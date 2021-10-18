using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenAttack : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(transform.CompareTag("HellHound"))
        {
            if (other.CompareTag("WardenAttack"))
            {
                GetComponentInParent<HellHound>().KillEnemy();
            }
        }
        else if(other.CompareTag("WardenAttack"))
        {
            Debug.Log("DIE");
            GetComponentInParent<Zombie>().KillEnemy();
        }
        
    }






}
