using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class ArticleManager : MonoBehaviour
{
    public static ArticleManager Instance;

    private List<Article> _articles = new List<Article>();
    public List<Article> Articles => _articles;

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
        _articles.Add(new Article()
        {
            Name = "김홍일",
            Content = "안녕하세요.",
            ArticleType = ArticleType.Normal,
            Like = 20,
            WriteTime = DateTime.Now
        });
        _articles.Add(new Article()
        {
            Name = "민예진",
            Content = "하이",
            ArticleType = ArticleType.Normal,
            Like = 7,
            WriteTime = DateTime.Now
        });
        _articles.Add(new Article()
        {
            Name = "조희수",
            Content = "해삐:)",
            ArticleType = ArticleType.Normal,
            Like = 908,
            WriteTime = DateTime.Now

        });
        _articles.Add(new Article()
        {
            Name = "고승연",
            Content = "안녕하세~",
            ArticleType = ArticleType.Normal,
            Like = 20,
            WriteTime = DateTime.Now
        });
        _articles.Add(new Article()
        {
            Name = "이태환",
            Content = "나는 전설이다.",
            ArticleType = ArticleType.Normal,
            Like = 320,
            WriteTime = DateTime.Now
        });
        _articles.Add(new Article()
        {
            Name = "이성민",
            Content = "나는 짱이다.",
            ArticleType = ArticleType.Normal,
            Like = 30,
            WriteTime = DateTime.Now
        });
        _articles.Add(new Article()
        {
            Name = "96년생정성훈",
            Content = "하이루방가방가",
            ArticleType = ArticleType.Normal,
            Like = 20,
            WriteTime = DateTime.Now
        });

    }

    void Start()
    {
        ArticleData articleData = new ArticleData(_articles);
        string jsonData = JsonUtility.ToJson(articleData);

        string path = Application.persistentDataPath;
    /*    Debug.Log(path);
        FileStream fs = new FileStream($"{path}/data.txt", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        sw.Write(jsonData);
        sw.Close();*/

        string txt = File.ReadAllText($"{ path}/data.txt");
        _articles = JsonUtility.FromJson<ArticleData>(txt).Data;
    }

    void Update()
    {
        
    }
}
