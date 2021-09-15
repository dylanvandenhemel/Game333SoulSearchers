using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    private Transform ghostKey;
    private float ghostKeyDrop;

    public bool collectedBronzeKey = false;
    public bool collectedSilverKey = false;
    public bool collectedGoldKey = false;

    public bool collectedGhostKey = false;
    public void KeyCollected(Transform currentKey)
    {
        //Disable Key instead of mesh because of prefab
        if (currentKey.CompareTag("KeyBronze"))
        {
            collectedBronzeKey = true;
            currentKey.gameObject.SetActive(false);
        }
        else if (currentKey.CompareTag("KeySilver"))
        {
            collectedSilverKey = true;
            currentKey.gameObject.SetActive(false);
        }
        else if (currentKey.CompareTag("KeyGold"))
        {
            collectedGoldKey = true;
            currentKey.gameObject.SetActive(false);
        }
        else if (currentKey.CompareTag("KeyGhost") && !transform.GetComponent<Player>().bpossessSkel)
        {
            ghostKey = currentKey;
            ghostKeyDrop = ghostKey.position.y;
            collectedGhostKey = true;
        }
    }

    public void UsedGhostKey()
    {
        ghostKey.gameObject.SetActive(false);
    }

    public void Update()
    {
        if(collectedGhostKey)
        {
            if(!transform.GetComponent<Player>().bpossessSkel)
            {
                ghostKey.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            }
            else
            {
                ghostKey.position = new Vector3(ghostKey.position.x, ghostKeyDrop, ghostKey.position.z);
                collectedGhostKey = false;
            }
        }
    }
}
