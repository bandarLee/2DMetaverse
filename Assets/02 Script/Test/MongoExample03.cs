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

        //��ť��Ʈ �ϳ� ����
        //InsertOne(��ť��Ʈ)
        Article article = new Article()
        {
            Name = "������",
            Content = "��������",
            ArticleType = ArticleType.Notice,
            Like = 100,
            WriteTime = DateTime.Now

        };
/*        collection.InsertOne(article);
*/        Debug.Log(article.Id);

        //��ť��Ʈ ������ ����
        //InsertMany(List<��ť��Ʈ>)
        List<Article> articles = new List<Article>()
        {
            new Article()
            {
                Name = "������",
                Content = "��������",
                ArticleType = ArticleType.Notice,
                Like = 100,
                WriteTime = DateTime.Now
            },
            new Article()
            {
                Name = "������",
                Content = "��������",
                ArticleType = ArticleType.Normal,
                Like = 100,
                WriteTime = DateTime.Now
            },
            new Article()
            {
                Name = "������",
                Content = "��������",
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
