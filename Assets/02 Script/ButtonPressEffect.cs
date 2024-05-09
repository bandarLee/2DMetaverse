using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ButtonPressEffect : MonoBehaviour
{
    public TextMeshProUGUI[] buttonTexts;
    public TextMeshProUGUI buttonText;

    public GameObject[] underlines;
    public GameObject line;

    private void Start()
    {
        foreach(GameObject underline in underlines)
        {
            underline.SetActive(false);
        }
        foreach (TextMeshProUGUI buttonText in buttonTexts)
        {
            buttonText.color = Color.gray;
        }
    }
    public void ButtonPress() 
    {
        foreach (GameObject underline in underlines)
        {
            underline.SetActive(false);
        }

        foreach (TextMeshProUGUI buttonText in buttonTexts)
        {
            buttonText.color = Color.gray;
        }

        line.SetActive(true);
        buttonText.color = Color.black;
    }


}
