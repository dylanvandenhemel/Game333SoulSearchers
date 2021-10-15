using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIElements : MonoBehaviour
{
    //Children Order is VERY important
    //Pause screen is always at the end

    public Text start;
    public Text mainMenu;
    public Text quit;

    PlayerControls pauseButton;
    private bool bPaused;
    private int pauseSelection = 3;

    private void OnEnable()
    {
        pauseButton = new PlayerControls();
        pauseButton.Enable();
        pauseButton.PlayerActions.Pause.performed += PauseGame;
    }

    private void OnDisable()
    {
        pauseButton.Disable();
        pauseButton.PlayerActions.Pause.performed -= PauseGame;
    }
    
    //Possession
    public void PossessUIOn()
    {
        transform.GetChild(0).gameObject.SetActive(true);        
    }

    public void PossessUIOff()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    //Lever
    public void LeverUIOn()
    {
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void LeverUIOff()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }

    //Keys
    public void BronzeKeyUIOn()
    {
        transform.GetChild(2).gameObject.SetActive(true);
    }
    public void BronzeKeyUIOff()
    {
        transform.GetChild(2).gameObject.SetActive(false);
    }
    public void SilverKeyUIOn()
    {
        transform.GetChild(3).gameObject.SetActive(true);
    }
    public void SilverKeyUIOff()
    {
        transform.GetChild(3).gameObject.SetActive(false);
    }
    public void GoldKeyUIOn()
    {
        transform.GetChild(4).gameObject.SetActive(true);
    }
    public void GoldKeyUIOff()
    {
        transform.GetChild(4).gameObject.SetActive(false);
    }




    //Pause
    public void PauseGame(InputAction.CallbackContext c)
    {        
        if(!bPaused)
        {
            pauseButton.PlayerActions.Movement.started += CurrentSelection;
            pauseButton.PlayerActions.Possess.performed += SelectUI;

            transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
            bPaused = true;

            Time.timeScale = 0;
        }
    }

    private void SelectUI(InputAction.CallbackContext c)
    {
        //start
        if(pauseSelection == 2)
        {
            Debug.Log("Start");
            if (bPaused)
            {
                pauseButton.PlayerActions.Movement.started -= CurrentSelection;
                pauseButton.PlayerActions.Possess.performed -= SelectUI;

                transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
                bPaused = false;
            }
        }
        //main menu
        else if(pauseSelection == 1)
        {
            Debug.Log("Main Menu");
            SceneManager.LoadScene("MainMenu");
        }
        //Quit
        else if (pauseSelection == 0)
        {
            Debug.Log("quit");
            Application.Quit();
        }

        Time.timeScale = 1;
    }

    private void CurrentSelection(InputAction.CallbackContext c)
    {
        if (pauseButton.PlayerActions.Movement.ReadValue<Vector2>().y >= 1)
        {
            //only 4 buttons at the moment
            if (pauseSelection < 2)
            {
                Debug.Log("Up");
                pauseSelection++;
            }
        }
        else if (pauseButton.PlayerActions.Movement.ReadValue<Vector2>().y <= -1)
        {
            if (pauseSelection > 0)
            {
                Debug.Log("Down");
                pauseSelection--;
            }
        }

        //Updates button selected to color
        if (pauseSelection == 2)
        {
            start.color = Color.blue;
        }
        else
        {
            start.color = Color.black;
        }

        if (pauseSelection == 1)
        {
            mainMenu.color = Color.blue;
        }
        else
        {
            mainMenu.color = Color.black;
        }

        if (pauseSelection == 0)
        {
            quit.color = Color.blue;
        }
        else
        {
            quit.color = Color.black;
        }
    }

}
