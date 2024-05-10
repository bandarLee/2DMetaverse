using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MongoExample03 : MonoBehaviour
{
    void Start()
    {
        string connectionString = "mongodb+srv://iron0944:qwas1234!@cluster0.czs65ij.mongodb.net/";
        MongoClient mongoClient = new MongoClient(connectionString);
        IMongoDatabase sampleDB = mongoClient.GetDatabase("metaverse");
        var collection = sampleDB.GetCollection<Article>("articles");

        //도큐먼트 하나 삽입
        //InsertOne(도큐먼트)
        Article article = new Article()
        {
            Name = "공지수",
            Content = "공지사항",
            ArticleType = ArticleType.Notice,
            Like = 100,
            WriteTime = DateTime.Now

        };
/*        collection.InsertOne(article);
*/        Debug.Log(article.Id);

        //도큐먼트 여러개 삽입
        //InsertMany(List<도큐먼트>)
        List<Article> articles = new List<Article>()
        {
            new Article()
            {
                Name = "공지수",
                Content = "공지사항",
                ArticleType = ArticleType.Notice,
                Like = 100,
                WriteTime = DateTime.Now
            },
            new Article()
            {
                Name = "일지수",
                Content = "공지사항",
                ArticleType = ArticleType.Normal,
                Like = 100,
                WriteTime = DateTime.Now
            },
            new Article()
            {
                Name = "이지수",
                Content = "공지사항",
                ArticleType = ArticleType.Normal,
                Like = 100,
                WriteTime = DateTime.Now
            }

        };
        collection.InsertMany(articles);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
