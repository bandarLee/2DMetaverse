using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UI_ArticleList : MonoBehaviour
{
    public List<UI_Article> UIArticles;
    public void Start()
    {
        Refresh();
    }
    public void Refresh()
    {
        List<Article> articles = ArticleManager.Instance.Articles;

        int articleCount = articles.Count;

        foreach ( UI_Article article in UIArticles)
        {

            article.gameObject.SetActive(false);
        }
        for(int i = 0; i < articleCount; i++)
        {
            UIArticles[i].gameObject.SetActive(true);
            UIArticles[i].Init(articles[i]);

        }
    }

}
