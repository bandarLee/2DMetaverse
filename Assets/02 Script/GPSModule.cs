using UnityEngine;
using UnityEngine.Android; //���ӽ����̽� �߰�

public class GPSModule : MonoBehaviour
{
    [Header("Setting")]
    public bool startGPSOnStart; //Start������ GPS�� ������ ������ ����
    public float desiredAccuracyInMeters; //���� ��ġ�κ����� �ִ� ������ �����ϴ� ���� (��Ȯ��)
    public float updateDistanceInMeters; //Ư�� �Ÿ� �̻� �̵��ϸ� ���ŵǵ��� �����ϴ� ���� (���� ��)

    [Header("Cache")]
    private LocationService locationService; //�ٽ� Ŭ����

    private void Awake()
    {
        locationService = Input.location; //��� ������ ���̹Ƿ� ĳ��
        Debug.Log(Input.location);
    }
    private void Start()
    {
        if (startGPSOnStart) //Start������ GPS�� �����ϰ��� �ϸ�
            StartGPS(); //����
    }

    public void StartGPS(string permissionName = null) //GPS�� �����ϴ� �Լ�
    {
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation)) //�̹� ��ġ ������ ȹ��������
        {
            locationService.Start(desiredAccuracyInMeters, updateDistanceInMeters); //���� ����
        }
        else //���� ��ġ ������ ȹ������ ��������
        {
            PermissionCallbacks callbacks = new(); //�ݹ� �Լ� ���� ��
            callbacks.PermissionGranted += StartGPS; //���� �Լ��� ��ͷ� ��������
            Permission.RequestUserPermission(Permission.FineLocation, callbacks); //���� ��û ��, �ٽ� GPS�� �����ϵ��� �Լ� ����
        }
    }
    public void StopGPS() //GPS�� �����ϴ� �Լ�
    {
        locationService.Stop(); //���� ����
    }
    public bool GetLocation(out LocationServiceStatus status, out float latitude, out float longitude, out float altitude) //��ġ ������ ��� �Լ�
    {
        latitude = 0f; //����
        longitude = 0f; //�浵
        altitude = 0f; //��
        status = locationService.status; //���� ����

        if (!locationService.isEnabledByUser) //����, ����ڰ� ����Ʈ���� GPS ����� ���ٸ�
            return false;

        switch (status)
        {
            case LocationServiceStatus.Stopped: //GPS�� �������� ����
            case LocationServiceStatus.Failed: //GPS ������ ������ �� ����
            case LocationServiceStatus.Initializing: //GPS ��� ���� �� �ʱ�ȭ ��
                return false; //false�� ��ȯ�ؼ� ���������� ������ ���� �������� �˸� (�� ������ status�� ���)

            default: //GPS ����� �������� (Running)
                LocationInfo locationInfo = locationService.lastData; //������ GPS ������ ���
                latitude = locationInfo.latitude; //���� ����
                longitude = locationInfo.longitude; //�浵 ����
                altitude = locationInfo.altitude; //�� ����
                return true; //true�� ��ȯ�ؼ� ���������� ������ ������ �˸�
        }
    }
}