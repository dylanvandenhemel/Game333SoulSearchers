using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuStart : MonoBehaviour
{
    PlayerControls menuButtons;

    public GameObject menu;

    public Text[] menuItemList;

    private int currentSelectionVal;

    private static bool bDoneTutorial;

    private int menuMinVal = 0;
    private int menuMaxVal;

    private void Awake()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        UnityEngine.Cursor.visible = false;
    }
    //private static bool visible;
    private void OnEnable()
    {
        menuButtons = new PlayerControls();
        menuButtons.Enable();
        menuButtons.PlayerActions.Possess.performed += SelectUI;
        menuButtons.PlayerActions.Movement.started += CurrentSelection;
    }
    private void OnDisable()
    {
        menuButtons.Disable();
        menuButtons.PlayerActions.Possess.performed -= SelectUI;
        menuButtons.PlayerActions.Movement.started -= CurrentSelection;
    }

    private void Start()
    {
        menuMaxVal = menuItemList.Length;
        TextColor(0);
    }

    private void CurrentSelection(InputAction.CallbackContext c)
    {
        if(menuButtons.PlayerActions.Movement.ReadValue<Vector2>().y < 0)
        {
            if(currentSelectionVal < menuMaxVal - 1)
            {
                currentSelectionVal++;
            }
        }
        else if(menuButtons.PlayerActions.Movement.ReadValue<Vector2>().y > 0)
        {
            if (currentSelectionVal > menuMinVal)
            {
                currentSelectionVal--;
            }
        }
        TextColor(currentSelectionVal);
    }

    private void TextColor(int menuVal)
    {
        for(int i = 0; i < menuMaxVal; i++)
        {
            if(i == menuVal)
            {
                menuItemList[i].color = Color.green;
            }
            else
            {
                menuItemList[i].color = Color.cyan;
            }
        }
    }

    private void SelectUI(InputAction.CallbackContext c)
    {
        if(currentSelectionVal == 0)
        {
            Debug.LogError("Start");
            if (!bDoneTutorial)
            {
                bDoneTutorial = true;
                SceneManager.LoadScene(1);
            }
            else if (bDoneTutorial)
            {
                SceneManager.LoadScene(Settings.levelsUnlocked);
            }
        }
        else if(currentSelectionVal == 1)
        {
            GetComponent<MenuAnimation>().cameraAnimCall(1);
        }
        else if (currentSelectionVal == 2)
        {
            GetComponent<MenuAnimation>().cameraAnimCall(2);
        }
        else if (currentSelectionVal == 3)
        {
            GetComponent<MenuAnimation>().cameraAnimCall(3);
        }
        else if (currentSelectionVal == 4)
        {
            Application.Quit();
        }
    }

}
