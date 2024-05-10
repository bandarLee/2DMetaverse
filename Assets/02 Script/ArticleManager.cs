using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Xml.Linq;
using System;
using static UnityEngine.ParticleSystem;
using UnityEngine.UI;

using TMPro;
using System.Collections.ObjectModel;

public class ArticleManager : MonoBehaviour
{
    public static ArticleManager Instance;

    private List<Article> _articles = new List<Article>();
    public List<Article> Articles => _articles;

    private IMongoCollection<Article> _articlesCollection;
    public UI_ArticleList uI_ArticleList;

    public TMP_InputField ContentInput;
    public Toggle NoticeToggle;

    private string author = "이태환";
    private string title = "제목없음";
    private string content;
    private int like = 0;
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
        _articlesCollection = database.GetCollection<Article>("articles");
    }

    void Start()
    {
        LoadAllArticles();
    }

    public void LoadAllArticles()
    {
        var filterNotice = Builders<BsonDocument>.Filter.Gte("ArticleType", 0);
        //writetime 을 기준으로 정렬
        var sort = new BsonDocument();
        sort["WriteTime"] = -1;
        //1은 오름차순
        //-1 은 내림차순(높은값에서 낮은값)
        //0은 정렬안함
        _articles = _articlesCollection.Find(new BsonDocument()).Sort(sort).ToList();
/*        _articles = ConvertDocumentsToArticles(documents);
*/        uI_ArticleList.Refresh();


    }
    public void LoadNoticeArticles()
    {
/*        var filterNotice = Builders<BsonDocument>.Filter.Eq("ArticleType", 1);
*/
        _articles = _articlesCollection.Find(data => (int)data.ArticleType == (int)ArticleType.Notice).ToList();
/*        _articles = ConvertDocumentsToArticles(articles);
*/        uI_ArticleList.Refresh();
        

    }
    public void WriteArticle()
    {
        ArticleType selectedType = NoticeToggle.isOn ? ArticleType.Notice : ArticleType.Normal;
        DateTime now = DateTime.Now.ToUniversalTime();
        Article newArticle = new Article()
        {
            Id = ObjectId.GenerateNewId(),
            ArticleType = selectedType,
            Name = author,
            Title = title,
            Content = ContentInput.text,
            Like = like,
            WriteTime = now
        };
        _articlesCollection.InsertOne(newArticle);
        LoadAllArticles();
        ContentInput.text = null;
    }
    public void DeleteArticle(ObjectId Id)
    {


        var deleteResult = _articlesCollection.DeleteOne(d => d.Id == Id);
        LoadAllArticles();

    }
    /*public void LoadFromDocument(BsonDocument doc,Article newArticle)
    {

        newArticle.ArticleType = (ArticleType)doc["ArticleType"].AsInt32;

        newArticle.Name = doc.GetValue("Name", string.Empty).AsString;
        newArticle.Title = doc.GetValue("Title", string.Empty).AsString;
        newArticle.Content = doc.GetValue("Content", string.Empty).AsString;
        newArticle.Like = doc.GetValue("Like", 0).AsInt32;
        newArticle.WriteTime = DateTime.Parse(doc["WriteTime"].AsString);


    }*/
}
