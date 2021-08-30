using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    private Transform ghostKey;

    public bool collectedBronzeKey = false;
    public bool collectedSilverKey = false;
    public bool collectedGoldKey = false;

    public bool collectedGhostKey = false;
    public void KeyCollected(Transform currentKey)
    {
        if (currentKey.CompareTag("KeyBronze"))
        {
            collectedBronzeKey = true;
            currentKey.GetComponent<MeshRenderer>().enabled = false;
            currentKey.GetComponent<Collider>().enabled = false;
        }
        else if (currentKey.CompareTag("KeySilver"))
        {
            collectedSilverKey = true;
            currentKey.GetComponent<MeshRenderer>().enabled = false;
            currentKey.GetComponent<Collider>().enabled = false;
        }
        else if (currentKey.CompareTag("KeyGold"))
        {
            collectedGoldKey = true;
            currentKey.GetComponent<MeshRenderer>().enabled = false;
            currentKey.GetComponent<Collider>().enabled = false;
        }
        else if (currentKey.CompareTag("KeyGhost") && !transform.GetComponent<Player>().bpossessSkel)
        {
            ghostKey = currentKey;
            collectedGhostKey = true;
            
        }
    }

    public void Update()
    {
        if(collectedGhostKey && !transform.GetComponent<Player>().bpossessSkel)
        {
            ghostKey.position = transform.position;
        }
    }
}
