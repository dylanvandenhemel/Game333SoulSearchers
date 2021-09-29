using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class UIElements : MonoBehaviour
{
    //Children Order is VERY important
    //Pause screen is always at the end

    public Canvas uIElements;

    PlayerControls pauseButton;
    private bool bPaused;

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
        uIElements.transform.GetChild(0).gameObject.SetActive(true);        
    }

    public void PossessUIOff()
    {
        uIElements.transform.GetChild(0).gameObject.SetActive(false);
    }

    //Lever
    public void LeverUIOn()
    {
        uIElements.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void LeverUIOff()
    {
        uIElements.transform.GetChild(1).gameObject.SetActive(false);
    }

    //Keys
    public void BronzeKeyUIOn()
    {
        uIElements.transform.GetChild(2).gameObject.SetActive(true);
    }
    public void BronzeKeyUIOff()
    {
        uIElements.transform.GetChild(2).gameObject.SetActive(false);
    }
    public void SilverKeyUIOn()
    {
        uIElements.transform.GetChild(3).gameObject.SetActive(true);
    }
    public void SilverKeyUIOff()
    {
        uIElements.transform.GetChild(3).gameObject.SetActive(false);
    }
    public void GoldKeyUIOn()
    {
        uIElements.transform.GetChild(4).gameObject.SetActive(true);
    }
    public void GoldKeyUIOff()
    {
        uIElements.transform.GetChild(4).gameObject.SetActive(false);
    }




    //Pause
    public void PauseGame(InputAction.CallbackContext c)
    {        
        if(!bPaused)
        {
            pauseButton.PlayerActions.Whistle.performed += QuitGame;

            GetComponent<Player>().OnPause();
            uIElements.transform.GetChild(uIElements.transform.childCount - 1).gameObject.SetActive(true);
            bPaused = true;

            Time.timeScale = 0;
        }
        else if (bPaused)
        {
            pauseButton.PlayerActions.Whistle.performed -= QuitGame;

            GetComponent<Player>().OnPause();
            uIElements.transform.GetChild(uIElements.transform.childCount - 1).gameObject.SetActive(false);
            bPaused = false;

            Time.timeScale = 1;
        }
    }

    private void QuitGame(InputAction.CallbackContext c)
    {
        Debug.Log("Quit");
        Application.Quit();
    }




}
