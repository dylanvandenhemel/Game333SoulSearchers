using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private Vector2 textPosition;
    private bool bDone;
    void Start()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1100);
        textPosition = GetComponent<RectTransform>().anchoredPosition;
    }

    void Update()
    {
        if(!bDone)
        {
            textPosition = Vector3.MoveTowards(textPosition, new Vector2(0, 1100), 2);
            GetComponent<RectTransform>().anchoredPosition = textPosition;

        }

        if(textPosition == new Vector2(0, 1100))
        {
            bDone = true;
            SceneManager.LoadScene(0);
        }
    }
}
