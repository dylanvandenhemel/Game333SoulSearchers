using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuSettings : MonoBehaviour
{
    PlayerControls menuButtons;

    public GameObject menu;

    public Text[] menuItemList;

    private int currentSelectionVal;

    public GameObject masterVol;
    public GameObject musicVol;
    public GameObject sFXVol;

    //keep value in the 0 - 4 range
    private int menuMinVal = 0;
    private int menuMaxVal = 2;

    private void OnEnable()
    {
        menuButtons = new PlayerControls();
        menuButtons.Enable();
        menuButtons.PlayerActions.Movement.started += CurrentSelection;
    }
    private void OnDisable()
    {
        menuButtons.Disable();
        menuButtons.PlayerActions.Movement.started -= CurrentSelection;
    }

    private void Start()
    {
        TextColor(0);
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

        if(currentSelectionVal == 0)
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
        else if(currentSelectionVal == 1)
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
        else if(currentSelectionVal == 2)
        {
            if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x > 0)
            {
                sFXVol.GetComponent<UnityEngine.UI.Slider>().value += 0.2f;
                sFXVol.GetComponent<AudioSource>().Play();
            }
            else if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x < 0)
            {
                sFXVol.GetComponent<UnityEngine.UI.Slider>().value -= 0.2f;
                sFXVol.GetComponent<AudioSource>().Play();
            }
            sFXVol.GetComponent<AudioSource>().volume = sFXVol.GetComponent<UnityEngine.UI.Slider>().value;
        }

        TextColor(currentSelectionVal);
    }

    private void TextColor(int menuVal)
    {
        for (int i = 0; i < menuMaxVal; i++)
        {
            if (i == menuVal)
            {
                menuItemList[i].color = Color.magenta;
            }
            else
            {
                menuItemList[i].color = Color.cyan;
            }
        }
    }
}
