using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class MenuStart : MonoBehaviour
{
    PlayerControls menuButtons;

    public GameObject menu;

    public TextMeshProUGUI[] menuItemList;

    private int currentSelectionVal;

    private static bool bDoneTutorial;

    private int menuMinVal = 0;
    private int menuMaxVal;

    [HideInInspector]public bool bLogoFadeDone;

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
        menuButtons.PlayerActions.DebugUnlock.started += UnlockLevels;
    }
    private void OnDisable()
    {
        menuButtons.Disable();
        menuButtons.PlayerActions.Possess.performed -= SelectUI;
        menuButtons.PlayerActions.Movement.started -= CurrentSelection;
        menuButtons.PlayerActions.DebugUnlock.started -= UnlockLevels;
    }

    private void UnlockLevels(InputAction.CallbackContext c)
    {
        Debug.Log("All levels Unlocked");
        Settings.levelsUnlocked = 100;
        Settings.levelMenuUnlocked = 100;
    }

    private void Start()
    {
        menuMaxVal = menuItemList.Length;
        TextColor(0);
        if(Settings.levelsUnlocked > 1)
        {
            //Start Game Text
            menuItemList[0].text = "Continue";
        }
    }

    private void CurrentSelection(InputAction.CallbackContext c)
    {
        if(menuButtons.PlayerActions.Movement.ReadValue<Vector2>().y < 0 && bLogoFadeDone)
        {
            if(currentSelectionVal < menuMaxVal - 1)
            {
                currentSelectionVal++;
            }
        }
        else if(menuButtons.PlayerActions.Movement.ReadValue<Vector2>().y > 0 && bLogoFadeDone)
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
        if(currentSelectionVal == 0 && bLogoFadeDone)
        {
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
            GetComponent<MenuAnimation>().cameraAnimCall(1, 0);
        }
        else if (currentSelectionVal == 2)
        {
            GetComponent<MenuAnimation>().cameraAnimCall(2, 0);
        }
        else if (currentSelectionVal == 3)
        {
            GetComponent<MenuAnimation>().cameraAnimCall(3, 0);
        }
        else if (currentSelectionVal == 4)
        {
            Application.Quit();
        }
    }

}
