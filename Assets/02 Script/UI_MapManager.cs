using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class UI_MapManager : MonoBehaviour
{
    public RawImage mapRawImage;

    [Header("맵 정보 입력")]
    public string strBaseURL = "";
    public int level = 14;
    public int mapWidth;
    public int mapHeight;
    public string strAPIKey = "";
    public string secretKey = "";
    public TMP_InputField inputAddress;
    private string baseUrl = "https://naveropenapi.apigw.ntruss.com/map-geocode/v2/geocode";

    void Start()
    {
        mapRawImage = GetComponent<RawImage>();
        GetCoordinatesFromAddress("고양시 화정로 69");
    }

    public void GetCoordinatesFromAddress(string address)
    {
        StartCoroutine(SendGeocodeRequest(address));
    }
    public void SearchAddress()
    {
        string _address = inputAddress.text;
        StartCoroutine(SendGeocodeRequest(_address));
    }
    IEnumerator SendGeocodeRequest(string address)
    {
        // URL을 인코딩하여 요청 생성
        string url = $"{baseUrl}?query={UnityWebRequest.EscapeURL(address)}";

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", strAPIKey);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", secretKey);

        // 요청 보내기
        yield return request.SendWebRequest();

        // 응답 처리
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Response: " + request.downloadHandler.text);
            ProcessGeocodeResponse(request.downloadHandler.text);
        }
    }

    void ProcessGeocodeResponse(string jsonResponse)
    {
        var geocodeResponse = JsonUtility.FromJson<GeocodeResponse>(jsonResponse);
        if (geocodeResponse.addresses != null && geocodeResponse.addresses.Length > 0)
        {
            string latitude = geocodeResponse.addresses[0].y;
            string longitude = geocodeResponse.addresses[0].x;
            Debug.Log($"Latitude: {latitude}, Longitude: {longitude}");

            StartCoroutine(MapLoader(latitude, longitude));
        }
        else
        {
            Debug.LogError("No addresses found in geocode response");
        }
    }

    IEnumerator MapLoader(string latitude, string longitude)
    {
        string str = $"{strBaseURL}?w={mapWidth}&h={mapHeight}&center={longitude},{latitude}&level={level}";

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(str);

        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", strAPIKey);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", secretKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            mapRawImage.texture = DownloadHandlerTexture.GetContent(request);
        }
    }

    [System.Serializable]
    public class GeocodeResponse
    {
        public Address[] addresses;
    }

    [System.Serializable]
    public class Address
    {
        public string roadAddress;
        public string jibunAddress;
        public string englishAddress;
        public AddressElement[] addressElements;
        public string x;
        public string y;
        public float distance;
    }

    [System.Serializable]
    public class AddressElement
    {
        public string[] types;
        public string longName;
        public string shortName;
    }
}
