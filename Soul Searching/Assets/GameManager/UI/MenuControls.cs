using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuControls : MonoBehaviour
{
    
    PlayerControls menuButtons;
    /*
    public GameObject menu;

    public Text[] menuItemList;

    private int currentSelectionVal;

    private int menuMinVal = 0;
    private int menuMaxVal;
    */
    private void OnEnable()
    {
        menuButtons = new PlayerControls();
        menuButtons.Enable();
    }
    private void OnDisable()
    {
        menuButtons.Disable();
        //menuButtons.PlayerActions.Movement.started -= CurrentSelection;
        menuButtons.PlayerActions.Interact.performed -= Return;
    }
    /*
    private void Start()
    {
        menuMaxVal = menuItemList.Length;
        TextColor(0);
    }

    private void Update()
    {
        Settings.masterVolumeSet = masterVol.GetComponent<UnityEngine.UI.Slider>().value;
        Settings.musicVolumeSet = musicVol.GetComponent<UnityEngine.UI.Slider>().value;
        Settings.sFXVolumeSet = sFXVol.GetComponent<UnityEngine.UI.Slider>().value;


        //for sound test
        soundTest.volume = Settings.sFXVolumeSet;
        Settings.sFXVolumeSet *= Settings.masterVolumeSet;
    }
    private void CurrentSelection(InputAction.CallbackContext c)
    {
        if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().y < 0)
        {
            if (currentSelectionVal < menuMaxVal - 1)
            {
                currentSelectionVal++;
            }
        }
        else if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().y > 0)
        {
            if (currentSelectionVal > menuMinVal)
            {
                currentSelectionVal--;
            }
        }

        if (currentSelectionVal == 0)
        {
            if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x > 0)
            {
                masterVol.GetComponent<UnityEngine.UI.Slider>().value += 0.2f;
            }
            else if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x < 0)
            {
                masterVol.GetComponent<UnityEngine.UI.Slider>().value -= 0.2f;
            }
        }
        else if (currentSelectionVal == 1)
        {
            if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x > 0)
            {
                musicVol.GetComponent<UnityEngine.UI.Slider>().value += 0.2f;
            }
            else if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x < 0)
            {
                musicVol.GetComponent<UnityEngine.UI.Slider>().value -= 0.2f;
            }
        }
        else if (currentSelectionVal == 2)
        {
            if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x > 0)
            {
                sFXVol.GetComponent<UnityEngine.UI.Slider>().value += 0.2f;
            }
            else if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x < 0)
            {
                sFXVol.GetComponent<UnityEngine.UI.Slider>().value -= 0.2f;
            }
            sFXVol.GetComponent<AudioSource>().Play();
        }

        TextColor(currentSelectionVal);
    }
    */
    //used to deactivate interaction with the menu off screen
    public void subCurrentMenu()
    {
        //menuButtons.PlayerActions.Movement.started += CurrentSelection;
        menuButtons.PlayerActions.Interact.performed += Return;
    }
    public void unSubCurrentMenu()
    {
        //menuButtons.PlayerActions.Movement.started -= CurrentSelection;
        menuButtons.PlayerActions.Interact.performed -= Return;
    }
    
    public void Return(InputAction.CallbackContext c)
    {
        GetComponent<MenuAnimation>().cameraAnimCall(0, 0);
    }

    /*
    private void TextColor(int menuVal)
    {
        for (int i = 0; i < menuMaxVal; i++)
        {
            if (i == menuVal)
            {
                menuItemList[i].color = Color.green;
            }
            else
            {
                menuItemList[i].color = Color.magenta;
            }
        }
    }
    */
}
