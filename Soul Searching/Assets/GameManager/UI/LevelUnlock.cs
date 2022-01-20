using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//updates with value in settings script
public class LevelUnlock1 : MonoBehaviour
{
    public GameObject[] skullLocks;

    public void Start()
    {
        if(Settings.levelsUnlocked >= 3)
        {
            for(int i = 0; i <= Settings.levelsUnlocked - 3; i++)
            {
                skullLocks[i].gameObject.SetActive(false);
            }
        }
    }




}
