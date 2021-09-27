using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class UIElements : MonoBehaviour
{
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
    public void PossessUIOn()
    {
        uIElements.transform.GetChild(0).gameObject.SetActive(true);        
    }

    public void PossessUIOff()
    {
        uIElements.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void LeverUIOn()
    {
        uIElements.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void LeverUIOff()
    {
        uIElements.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void PauseGame(InputAction.CallbackContext c)
    {        
        if(!bPaused)
        {
            GetComponent<Player>().OnPause();
            Debug.Log("Pause");
            uIElements.transform.GetChild(2).gameObject.SetActive(true);
            bPaused = true;

            Time.timeScale = 0;
        }
        else if (bPaused)
        {
            GetComponent<Player>().OnPause();
            Debug.Log("UNPause");
            uIElements.transform.GetChild(2).gameObject.SetActive(false);
            bPaused = false;

            Time.timeScale = 1;
        }
    }




}
