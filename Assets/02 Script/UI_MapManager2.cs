using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class UI_MapManager2 : MonoBehaviour
{
    public RawImage mapRawImage;
    public TMP_InputField startInputAddress;
    public TMP_InputField endInputAddress;

    [Header("맵 정보 입력")]
    public string strBaseURL = "https://naveropenapi.apigw.ntruss.com/map-static/v2/raster";
    public int level = 14;
    public int mapWidth;
    public int mapHeight;
    public string strAPIKey = "";
    public string secretKey = "";

    private string geocodeUrl = "https://naveropenapi.apigw.ntruss.com/map-geocode/v2/geocode";
    private string directionsUrl = "https://naveropenapi.apigw.ntruss.com/map-direction/v1/driving";

    void Start()
    {
        mapRawImage = GetComponent<RawImage>();
    }

    public void SearchRoute()
    {
        string startAddress = startInputAddress.text;
        string endAddress = endInputAddress.text;
        StartCoroutine(GetRouteBetweenAddresses(startAddress, endAddress));
    }

    public IEnumerator GetRouteBetweenAddresses(string startAddress, string endAddress)
    {
        Vector2 startCoordinates = Vector2.zero;
        Vector2 endCoordinates = Vector2.zero;

        // 시작 좌표를 가져옴
        yield return StartCoroutine(GetCoordinatesFromAddress(startAddress, result => startCoordinates = result));
        // 도착 좌표를 가져옴
        yield return StartCoroutine(GetCoordinatesFromAddress(endAddress, result => endCoordinates = result));

        if (startCoordinates != Vector2.zero && endCoordinates != Vector2.zero)
        {
            StartCoroutine(GetRoute(startCoordinates, endCoordinates));
        }
        else
        {
            Debug.LogError("Failed to get coordinates for the addresses");
        }
    }

    public IEnumerator GetCoordinatesFromAddress(string address, System.Action<Vector2> callback)
    {
        string geocodeRequestUrl = $"{geocodeUrl}?query={UnityWebRequest.EscapeURL(address)}";

        UnityWebRequest request = UnityWebRequest.Get(geocodeRequestUrl);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", strAPIKey);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", secretKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
            callback(Vector2.zero);
        }
        else
        {
            Debug.Log("Geocode Response: " + request.downloadHandler.text);
            var geocodeResponse = JsonUtility.FromJson<GeocodeResponse>(request.downloadHandler.text);
            if (geocodeResponse.addresses != null && geocodeResponse.addresses.Length > 0)
            {
                string latitude = geocodeResponse.addresses[0].y;
                string longitude = geocodeResponse.addresses[0].x;
                Debug.Log($"Address: {address}, Latitude: {latitude}, Longitude: {longitude}");
                callback(new Vector2(float.Parse(latitude), float.Parse(longitude)));
            }
            else
            {
                Debug.LogError("No addresses found in geocode response");
                callback(Vector2.zero);
            }
        }
    }

    public IEnumerator GetRoute(Vector2 startCoordinates, Vector2 endCoordinates)
    {
        string routeUrl = $"{directionsUrl}?start={startCoordinates.y},{startCoordinates.x}&goal={endCoordinates.y},{endCoordinates.x}&option=trafast";

        Debug.Log("Route URL: " + routeUrl); // URL을 로그에 출력하여 디버깅

        UnityWebRequest request = UnityWebRequest.Get(routeUrl);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", strAPIKey);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", secretKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Route Response: " + request.downloadHandler.text);
            ProcessRouteResponse(startCoordinates, endCoordinates, request.downloadHandler.text);
        }
    }

    void ProcessRouteResponse(Vector2 startCoordinates, Vector2 endCoordinates, string jsonResponse)
    {
        Debug.Log("Parsed Route Response: " + jsonResponse);
        StartCoroutine(LoadMapImageWithRoute(startCoordinates, endCoordinates));
    }

    public IEnumerator LoadMapImageWithRoute(Vector2 startCoordinates, Vector2 endCoordinates)
    {
        string start = $"{startCoordinates.y},{startCoordinates.x}";
        string end = $"{endCoordinates.y},{endCoordinates.x}";
        string markers = $"&markers=type:d|size:mid|pos:{start}%7Ctype:d|size:mid|pos:{end}";

        string path = $"&path=pathList:{start}|{end}|pathColor:0x0000ff|pathWeight:5";

        string url = $"{strBaseURL}?w={mapWidth}&h={mapHeight}&center={startCoordinates.y},{startCoordinates.x}&level={level}{markers}{path}";

        Debug.Log("Map URL: " + url); // URL을 로그에 출력하여 디버깅

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", strAPIKey);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", secretKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            if (request.downloadHandler.data != null && request.downloadHandler.data.Length > 0)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                mapRawImage.texture = texture;
                Debug.Log("Map image loaded successfully.");
            }
            else
            {
                Debug.LogError("Map image data is null or empty.");
            }
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
