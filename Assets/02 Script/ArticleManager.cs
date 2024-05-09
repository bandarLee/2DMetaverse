using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Xml.Linq;
using System;
using static UnityEngine.ParticleSystem;

public class ArticleManager : MonoBehaviour
{
    public static ArticleManager Instance;

    private List<Article> _articles = new List<Article>();
    public List<Article> Articles => _articles;

    private IMongoCollection<BsonDocument> articlesCollection;
    public UI_ArticleList uI_ArticleList;
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

        string connectionString = "mongodb+srv://iron0944:qwas1234!@cluster0.czs65ij.mongodb.net/";
        MongoClient mongoClient = new MongoClient(connectionString);
        IMongoDatabase database = mongoClient.GetDatabase("metaverse");
        articlesCollection = database.GetCollection<BsonDocument>("articles");
    }

    void Start()
    {
        LoadAllArticles();
    }

    public void LoadAllArticles()
    {
        var filterNotice = Builders<BsonDocument>.Filter.Gte("ArticleType", 0);

        var documents = articlesCollection.Find(filterNotice).ToList();
        _articles = ConvertDocumentsToArticles(documents);
        uI_ArticleList.Refresh();


    }
    public void LoadNoticeArticles()
    {
        var filterNotice = Builders<BsonDocument>.Filter.Eq("ArticleType", 1);

        var documents = articlesCollection.Find(filterNotice).ToList();
        _articles = ConvertDocumentsToArticles(documents);
        uI_ArticleList.Refresh();
        

    }
    private List<Article> ConvertDocumentsToArticles(List<BsonDocument> documents)
    {
        _articles.Clear();
        foreach (var doc in documents)
        {
            Article newArticle = new Article(); 
            LoadFromDocument(doc,newArticle);
            _articles.Add(newArticle);
        }
        return _articles;
    }
    public void LoadFromDocument(BsonDocument doc,Article newArticle)
    {

        newArticle.ArticleType = (ArticleType)doc["ArticleType"].AsInt32;

        newArticle.Name = doc.GetValue("Name", string.Empty).AsString;
        newArticle.Title = doc.GetValue("Title", string.Empty).AsString;
        newArticle.Content = doc.GetValue("Content", string.Empty).AsString;
        newArticle.Like = doc.GetValue("Like", 0).AsInt32;
        newArticle.WriteTime = DateTime.Parse(doc["WriteTime"].AsString);


    }
}
