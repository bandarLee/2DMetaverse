using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ArticleType
{
    Normal,
    Notice,

}
public class Article
{
    public ArticleType ArticleType;
    public string Name;
    public string Title;
    public string Content;
    public int Like;
    public DateTime WriteTime;

}
