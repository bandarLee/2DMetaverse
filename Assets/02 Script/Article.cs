using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ArticleType
{
    Normal,
    Notice,

}
[Serializable]
public class Article
{
    public ArticleType ArticleType;
    public string Name;
    public string Title;
    public string Content;
    public int Like;
    public DateTime WriteTime;

}
[Serializable]

public class ArticleData
{
    public List<Article> Data;
    public ArticleData(List<Article> data)
    {
        Data = data;
    }
}
