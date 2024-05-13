using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class UI_Article : MonoBehaviour
{
    public RawImage ProfileImageUI;
    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI ContentTextUI;
    public TextMeshProUGUI LikeTextUI;
    public TextMeshProUGUI WriteTimeUI;
    public Article ThisArticle;
    private  Dictionary<string, Texture> textureCache = new Dictionary<string, Texture>();

    public void Init(Article article)
    {

        NameTextUI.text = article.Name;
        ContentTextUI.text = article.Content;
        LikeTextUI.text = $"좋아요 {article.Like}";
        WriteTimeUI.text = GetTimeString(article.WriteTime.ToLocalTime());
        ThisArticle = article;
        if(article.ProfileImage!= null)
        {
            StartCoroutine(GetTexture(article.ProfileImage));

        }
        else
        {
            StartCoroutine(GetTexture("http://localhost:8080/Base.png"));

        }


    }
    private string GetTimeString(DateTime dateTime)
    {
        DateTime now = DateTime.Now;

        TimeSpan Timediff = now - dateTime;



        switch (Timediff.TotalMinutes)
        {
            case < 1:
                return "방금전";
            case < 60:
                return $"{Timediff.TotalMinutes:N0}분전";
            case < 1440:
                return $"{Timediff.TotalHours:N0}시간전";
            case < 10080:
                return $"{Timediff.TotalDays:N0}일전";
            case < 40320:
                return $"{Timediff.TotalDays / 7:N0}주전";

            default:
                return dateTime.ToString("yyyy년 MM월 dd일");


        }
    }
    IEnumerator GetTexture(string imageURL)
    {
        if (textureCache.TryGetValue(imageURL, out Texture cachedTexture))
        {
            ProfileImageUI.texture = cachedTexture;
            Debug.Log("캐싱");
            yield break; 
        }

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageURL);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture downloadedTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            textureCache[imageURL] = downloadedTexture;
            ProfileImageUI.texture = downloadedTexture;
        }
    }
}
