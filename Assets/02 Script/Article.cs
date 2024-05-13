using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public enum ArticleType
{
    Normal,
    Notice,

}
[Serializable]
public class Article
{
    [BsonId]
    public ObjectId Id; // ID, _id, id ( 데이터를 넣은 시간 + 기기의 ID + 프로세스 ID + )
    public ArticleType ArticleType;
    public string Name;
    public string Title;
    public string Content;
    public int Like;
    public DateTime WriteTime;
    public string ProfileImage;

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
