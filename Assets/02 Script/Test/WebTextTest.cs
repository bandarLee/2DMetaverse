using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebTextTest : MonoBehaviour
{
    public Text TextUI;

    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://time.jsontest.com/");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);

/*            byte[] results = www.downloadHandler.data;
*/          string text = www.downloadHandler.text;
            TextUI.text = text;
            WebTime webtime = JsonUtility.FromJson<WebTime>(text);
            Debug.Log(webtime.date);
            Debug.Log(webtime.milliSeconds_since_epoch);
            Debug.Log(webtime.Time);

        }

    }
   
}
[Serializable]
public class WebTime
{
    public string date;
    public long milliSeconds_since_epoch;
    public string Time;
}
