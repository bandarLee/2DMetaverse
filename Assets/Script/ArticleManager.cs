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
            Name = "��ȫ��",
            Content = "�ȳ��ϼ���.",
            ArticleType = ArticleType.Normal,
            Like = 20,
            WriteTime = DateTime.Now
        });
        _articles.Add(new Article()
        {
            Name = "�ο���",
            Content = "����",
            ArticleType = ArticleType.Normal,
            Like = 7,
            WriteTime = DateTime.Now
        });
        _articles.Add(new Article()
        {
            Name = "�����",
            Content = "�ػ�:)",
            ArticleType = ArticleType.Normal,
            Like = 908,
            WriteTime = DateTime.Now

        });
        _articles.Add(new Article()
        {
            Name = "��¿�",
            Content = "�ȳ��ϼ�~",
            ArticleType = ArticleType.Normal,
            Like = 20,
            WriteTime = DateTime.Now
        });
        _articles.Add(new Article()
        {
            Name = "����ȯ",
            Content = "���� �����̴�.",
            ArticleType = ArticleType.Normal,
            Like = 320,
            WriteTime = DateTime.Now
        });
        _articles.Add(new Article()
        {
            Name = "�̼���",
            Content = "���� ¯�̴�.",
            ArticleType = ArticleType.Normal,
            Like = 30,
            WriteTime = DateTime.Now
        });
        _articles.Add(new Article()
        {
            Name = "96���������",
            Content = "���̷�氡�氡",
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
