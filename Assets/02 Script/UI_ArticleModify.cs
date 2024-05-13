using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ArticleModify : MonoBehaviour
{
    public static UI_ArticleModify Instance;
    public Article Modified_Article;
    public GameObject ArticleModifyUI;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PressCloseButton()
    {
        ArticleModifyUI.SetActive(false);

    }
    public void PressModifyCompleteButton()
    {
        ArticleManager.Instance.CompleteModifyArticle(Modified_Article);

    }

    void Update()
    {
        
    }
}
