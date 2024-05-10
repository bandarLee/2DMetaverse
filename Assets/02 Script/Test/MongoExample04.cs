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
        //수정문
        var updateDefinition = Builders<Article>.Update.Set("Name", "민희진");
        UpdateResult result = collection.UpdateMany(data => data.Name == "공지수", updateDefinition);
        Debug.Log($"수정된 문서 개수 :{result.ModifiedCount}");

        Article article = new Article()
        {
            Id = ObjectId.GenerateNewId(),

            Name = "운석열",
            Content = "반갑습니다. 대통령입니까",
            ArticleType = ArticleType.Notice,
            Like = 100,
            WriteTime = DateTime.Now

        };
        /*var result2 = collection.ReplaceOne(data = > d.Id == id, article);*/
        var deleteResult  = collection.DeleteOne(d => d.Name == "민희진");
        Debug.Log($"{deleteResult.DeletedCount} article");
    }


}
