using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UI_NaverApi : MonoBehaviour
{

    private string clientId = "zhcDbCqv11egWvhrIkTg"; 
    private string clientSecret = "Hr_R7jTdS6";
    private Naver_ItemData naverItemData;

    void Start()
    {
        naverItemData = GetComponent<Naver_ItemData>();
        StartCoroutine(SearchNaverShopping("리그오브레전드", 3));
    }

    IEnumerator SearchNaverShopping(string query, int display)
    {
        string url = "https://openapi.naver.com/v1/search/shop.json?query=" + UnityWebRequest.EscapeURL(query) + "&display=" + display;

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("X-Naver-Client-Id", clientId);
        request.SetRequestHeader("X-Naver-Client-Secret", clientSecret);

     
        yield return request.SendWebRequest();
   
        ProcessResponse(request.downloadHandler.text);
    }


    void ProcessResponse(string jsonResponse)
    {
        MyItemDataArray myItem = naverItemData.ParseJsonResponse(jsonResponse);
        naverItemData.DisplayItems(myItem);
    }


}

