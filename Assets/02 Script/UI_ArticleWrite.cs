using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

using UnityEngine.UI;
public class UI_ArticleWrite : MonoBehaviour
{
    public GameObject ArticleList;
    public GameObject ArticleWrite;






    public void PressWriteButton()
    {
        ArticleList.gameObject.SetActive(false);
        ArticleWrite.gameObject.SetActive(true);

    }
    public void PressCloseButton()
    {
        ArticleWrite.gameObject.SetActive(false);
        ArticleList.gameObject.SetActive(true);

    }
    public void PressResisterButton()
    {
        ArticleManager.Instance.WriteArticle();

        PressCloseButton();

    }
}
