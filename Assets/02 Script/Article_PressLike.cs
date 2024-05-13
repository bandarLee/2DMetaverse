using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Article_PressLike : MonoBehaviour
{
    public UI_Article UI_Article;

    public GameObject Heart;
    public GameObject HeartOutline;

    public bool PressHeart = false;
    int a = 1;
    public void PressLike()
    {
        if (!PressHeart)
        {
            HeartOutline.SetActive(false);
            Heart.SetActive(true);
            PressHeart = true;
            a = 1;
            ArticleManager.Instance.ModifyHeart(UI_Article.ThisArticle,a);
        }
        else
        {
            Heart.SetActive(false);
            HeartOutline.SetActive(true);
            PressHeart = false;
            a = -1;
            ArticleManager.Instance.ModifyHeart(UI_Article.ThisArticle, a);


        }
    }
}
