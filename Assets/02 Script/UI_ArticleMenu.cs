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
        ArticleManager.Instance.DeleteArticle(UI_Article.ThisArticle.Id);
        PressMenuButton();
    }
    public void PressInsertButton()
    {
        UI_ArticleModify.Instance.ArticleModifyUI.SetActive(true);
        UI_ArticleModify.Instance.Modified_Article = UI_Article.ThisArticle;
        ArticleManager.Instance.InsertArticle(UI_Article.ThisArticle);
        PressMenuButton();
    }
    public void PressCloseButton()
    {
        UI_ArticleModify.Instance.ArticleModifyUI.SetActive(false);

    }

}
