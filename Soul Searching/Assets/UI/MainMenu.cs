using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    PlayerControls menuButton;
    private Scene currentScene;

    public GameObject settings;
    private bool bsettingsOn;
    private void OnEnable()
    {
        menuButton = new PlayerControls();
        menuButton.Enable();
        menuButton.PlayerActions.Possess.performed += StartGame;
        menuButton.PlayerActions.Interact.performed += Settings;
        menuButton.PlayerActions.Whistle.performed += QuitGame;
    }
    private void OnDisable()
    {
        menuButton.Disable();
        menuButton.PlayerActions.Possess.performed -= StartGame;
        menuButton.PlayerActions.Interact.performed -= Settings;
        menuButton.PlayerActions.Whistle.performed -= QuitGame;
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    private void StartGame(InputAction.CallbackContext c)
    {
        Debug.Log("Next Scene");
        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }

    private void Settings(InputAction.CallbackContext c)
    {
        if(!bsettingsOn)
        {
            settings.SetActive(true);
            bsettingsOn = true;
        }
        else if(bsettingsOn)
        {
            settings.SetActive(false);
            bsettingsOn = false;

        }
    }


    private void QuitGame(InputAction.CallbackContext c)
    {
        Debug.Log("Quit");
        Application.Quit();
    }


}
