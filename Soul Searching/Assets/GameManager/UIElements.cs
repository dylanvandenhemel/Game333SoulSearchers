using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIElements : MonoBehaviour
{
    public Canvas uIElements;
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
        uIElements.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void LeverUIOff()
    {
        uIElements.transform.GetChild(0).gameObject.SetActive(false);
    }




}
