using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : MonoBehaviour
{
    public bool bIsLastLevel;

    private Scene currentScene;
    public Transform fade;
    private void Start()
    {
        //last child
        fade.GetChild(fade.childCount - 1).GetComponent<Animator>().SetTrigger("fadeIn");
        currentScene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fade.GetChild(fade.childCount - 1).GetComponent<Animator>().SetTrigger("fadeOut");
            other.GetComponent<CharacterController>().enabled = false;
            StartCoroutine(fadeWait());
        }
    }

    IEnumerator fadeWait()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Next Scene");
        if (bIsLastLevel)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
    }
}
