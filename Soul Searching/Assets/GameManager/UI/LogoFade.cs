using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoFade : MonoBehaviour
{
    public GameObject mainScript;
    public void Load()
    {
        gameObject.SetActive(false);
        transform.GetComponent<Animator>().SetTrigger("FadeOut");
        transform.parent.GetComponent<Animator>().SetTrigger("fadeIn");
        mainScript.GetComponent<MenuStart>().bLogoFadeDone = true;
    }
}
