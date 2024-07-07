using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class MyItemData
{
    public string title;
    public string link;
    public string image;
    public int lprice;
    public int hprice;
    public string mallName;
    public int productId;
    public int productType;
    public string brand;
    public string maker;
    public string category1;
    public string category2;
    public string category3;
    public string category4;

}
[System.Serializable]
public class MyItemDataArray
{
    public MyItemData[] items;
}
public class Naver_ItemData : MonoBehaviour
{
    TextAsset textData;

    MyItemDataArray myItem;
    public TextMeshProUGUI[] textBoxes;
    public RawImage[] imgBoxes;

    public void DisplayItems(MyItemDataArray myItem)
    {
        for (int i = 0; i < textBoxes.Length && i < myItem.items.Length; i++)
        {
            var item = myItem.items[i];
            string truncatedTitle = item.title.Length > 22 ? item.title.Substring(0, 22) : item.title;
            string mallName = item.mallName.Length > 4 ? item.title.Substring(0, 4) : item.mallName;
            textBoxes[i].text = $"{truncatedTitle}\n\n{item.lprice}¿ø\n{mallName}";

            StartCoroutine(LoadImage(item.image, imgBoxes[i]));
            int index = i;
            textBoxes[i].GetComponentInChildren<Button>().onClick.AddListener(() => OpenLink(index));
            imgBoxes[i].GetComponentInChildren<Button>().onClick.AddListener(() => OpenLink(index));

        }
    }
    private void OpenLink(int index)
    {
        Application.OpenURL(myItem.items[index].link);
    }
    public MyItemDataArray ParseJsonResponse(string jsonResponse)
    {
        return JsonUtility.FromJson<MyItemDataArray>(jsonResponse);
    }
    IEnumerator LoadImage(string url, RawImage imgBox)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

    
        
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            imgBox.texture = texture;
     
    }

}
