using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MongoExample04 : MonoBehaviour
{
    void Start()
    {
        string connectionString = "mongodb+srv://iron0944:qwas1234!@cluster0.czs65ij.mongodb.net/";
        MongoClient mongoClient = new MongoClient(connectionString);
        IMongoDatabase sampleDB = mongoClient.GetDatabase("metaverse");
        var collection = sampleDB.GetCollection<Article>("articles");
        //������
        var updateDefinition = Builders<Article>.Update.Set("Name", "������");
        UpdateResult result = collection.UpdateMany(data => data.Name == "������", updateDefinition);
        Debug.Log($"������ ���� ���� :{result.ModifiedCount}");

        Article article = new Article()
        {
            Id = ObjectId.GenerateNewId(),

            Name = "���",
            Content = "�ݰ����ϴ�. ������Դϱ�",
            ArticleType = ArticleType.Notice,
            Like = 100,
            WriteTime = DateTime.Now

        };
        /*var result2 = collection.ReplaceOne(data = > d.Id == id, article);*/
        var deleteResult  = collection.DeleteOne(d => d.Name == "������");
        Debug.Log($"{deleteResult.DeletedCount} article");
    }


}
