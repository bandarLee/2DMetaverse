using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebImageTest : MonoBehaviour
{
    public RawImage ImageUI;

    //Http : ������ ��û(Request)�� ����(Response) �� �ϱ� ���� ��ӵ� ������ �ؽ�Ʈ
    //��(Web) : �Ź����̶�� ������ ����� '���ͳ�'�� �ǹ�
    

    void Start()
    {
        StartCoroutine(GetTexture());
        //�ڷ�ƾ������ ��Ʈ��ũ���� �����͸� �޾ƿ��°� �ǽð� �ƴ϶� �񵿱ⵥ���ͷ� ������

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
