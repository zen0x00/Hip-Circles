using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComboText : MonoBehaviour
{
    [SerializeField]private GameObject comboPanel;
    [SerializeField]private TextMeshProUGUI comboText;
    [SerializeField]private GameObject Boost1;
    [SerializeField]private GameObject Boost2;
    [SerializeField]private GameObject Boost3;
    [SerializeField]private GameObject Boost4;
    [SerializeField]private GameObject Boost5;
    

    int loopCount=0;
    public void increaseCount()
    {
        loopCount++;
        comboPanel.SetActive(true);
        if (loopCount == 1)
        {
            comboText.text="Good";
            Boost1.SetActive(true);
        }
        if (loopCount == 2)
        {
            comboText.text="Excellent";
            Boost2.SetActive(true);
        }
        if (loopCount == 3)
        {
            comboText.text="You Are Nailing It";
            Boost3.SetActive(true);
        }
        if (loopCount == 4)
        {
            comboText.text="Marvellous";
            Boost4.SetActive(true);
        }
        if (loopCount >= 5)
        {
            comboText.text="Unstopable";
            Boost5.SetActive(true);
        }
    }
    public void resetCount()
    {
        loopCount=0;
        comboPanel.SetActive(false);
        Boost1.SetActive(false);
        Boost2.SetActive(false);
        Boost3.SetActive(false);
        Boost4.SetActive(false);
        Boost5.SetActive(false);
    }
    
}
