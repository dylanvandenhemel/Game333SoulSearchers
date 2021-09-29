using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : MonoBehaviour
{
    //Loading Screen someday----> then next scene


    private Scene currentScene;
    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Next Scene");
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
    }
}
