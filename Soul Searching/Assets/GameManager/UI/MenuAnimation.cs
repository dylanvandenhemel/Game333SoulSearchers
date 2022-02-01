using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject levelMenu;
    //public GameObject levelMenu2;
    public GameObject controlMenu;
    public GameObject settingMenu;



    Vector3 startMenuPosition;
    Quaternion startMenuRotation;

    Quaternion levelMenuRotation;

    Vector3 controlsMenuPosition;

    Vector3 settingMenuPosition;
    Quaternion settingMenuRotation;

    Vector3 level2MenuPosition;

    private int cameraAnimationVal;

    private void Start()
    {
        startMenuRotation = transform.rotation;
        startMenuPosition = transform.position;
        levelMenuRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 90, 0);
        controlsMenuPosition = new Vector3(transform.position.x - 1.8f, transform.position.y, transform.position.z);
        settingMenuPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.8f);
        settingMenuRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y - 90, 0);
        level2MenuPosition = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 1);
    }
    void Update()
    {
        if (cameraAnimationVal == 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, startMenuRotation, 150 * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, startMenuPosition, 3 * Time.deltaTime);
        }
        //prevents camera from getting stuck in level menu 2
        else if (cameraAnimationVal == 1)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, levelMenuRotation, 150 * Time.deltaTime);
        }
        else if (cameraAnimationVal == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, controlsMenuPosition, 3 * Time.deltaTime);
        }
        else if (cameraAnimationVal == 3)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, settingMenuRotation, 150 * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, settingMenuPosition, 3 * Time.deltaTime);
        }
        else if (cameraAnimationVal == 4)
        {
            transform.position = Vector3.MoveTowards(transform.position, level2MenuPosition, 3 * Time.deltaTime);
        }
    }

    public void cameraAnimCall(int valc)
    {
        StartCoroutine(cameraAnimation(valc));
    }

    public IEnumerator cameraAnimation(int animVal)
    {
        if (animVal == 0)
        {
            cameraAnimationVal = 0;
        }
        else if (animVal == 1)
        {
            cameraAnimationVal = 1;
        }
        else if (animVal == 2)
        {
            cameraAnimationVal = 2;
        }
        else if (animVal == 3)
        {
            cameraAnimationVal = 3;
        }
        else if (animVal == 4)
        {
            cameraAnimationVal = 4;
        }

        startMenu.SetActive(false);
        GetComponent<MenuStart>().enabled = false;

        levelMenu.SetActive(false);
        GetComponent<MenuLevel>().unSubCurrentMenu();

        controlMenu.SetActive(false);
        GetComponent<MenuControls>().unSubCurrentMenu();

        settingMenu.SetActive(false);
        GetComponent<MenuSettings>().unSubCurrentMenu();

        //levelMenu2.SetActive(true);
        //GetComponent<MenuLevel2>().subCurrentMenu();


        yield return new WaitForSeconds(0.6f);

        
        if (animVal == 0)
        {
            startMenu.SetActive(true);
            GetComponent<MenuStart>().enabled = true;
        }
        else if (animVal == 1)
        {
            levelMenu.SetActive(true);
            GetComponent<MenuLevel>().subCurrentMenu();
        }
        else if (animVal == 2)
        {
            controlMenu.SetActive(true);
            GetComponent<MenuControls>().subCurrentMenu();
        }
        else if (animVal == 3)
        {
            settingMenu.SetActive(true);
            GetComponent<MenuSettings>().subCurrentMenu();
        }
        else if(animVal == 4)
        {
            //levelMenu2.SetActive(true);
            GetComponent<MenuLevel2>().subCurrentMenu();
        }
        

    }

}
