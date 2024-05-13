using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebImageTest : MonoBehaviour
{
    public RawImage ImageUI;

    //Http : 웹에서 요청(Request)과 응답(Response) 을 하기 위한 약속된 형태의 텍스트
    //웹(Web) : 거미줄이라는 뜻으로 현재는 '인터넷'을 의미
    

    void Start()
    {
        StartCoroutine(GetTexture());
        //코루틴을통해 네트워크에서 데이터를 받아오는거 실시간 아니라서 비동기데이터로 가져옴

    }
    IEnumerator GetTexture()
    {
        string imageURL = "https://m.doremicat.co.kr/web/product/big/202209/c77d1167c82e18f12b18421556553e85.jpg";

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageURL);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            ImageUI.texture = myTexture;
        }
    }

}
