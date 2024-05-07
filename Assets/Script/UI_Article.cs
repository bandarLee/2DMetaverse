using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Article : MonoBehaviour
{
    public Image ProfileImageUI;
    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI ContentTextUI;
    public TextMeshProUGUI LikeTextUI;
    public TextMeshProUGUI WriteTimeUI;
    public void Init(Article article)
    {
        NameTextUI.text = article.Name;
        ContentTextUI.text = article.Content;
        LikeTextUI.text = $"¡¡æ∆ø‰ {article.Like}";
        WriteTimeUI.text = $"{article.WriteTime}";



    }
}
