using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ArticleMenu : MonoBehaviour
{
    public GameObject ArticleMenu;
    public UI_Article UI_Article;
    private void Start()
    {
        ArticleMenu.SetActive(false);
    }
    public void PressMenuButton()
    {
        ArticleMenu.SetActive(!ArticleMenu.activeSelf);
    }
    public void PressDeleteButton()
    {
        ArticleManager.Instance.DeleteArticle(UI_Article.articleid);
        PressMenuButton();
    }
}
