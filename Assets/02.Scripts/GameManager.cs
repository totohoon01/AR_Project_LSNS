using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //싱글톤 변수들
    public static GameManager instance = null;
    //userInfo
    public string userName;
    public string userPW;
    public string userIdentifier;

    public float latitude;
    public float logitude;
    private LocationInfo userPos;

    //지금 읽을 오브젝트 정보
    public string hashCode;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        Input.location.Start(0.5f);
        int wait = 1000; // 기본 값
        // Checks if the GPS is enabled by the user (-> Allow location ) 
        if (Input.location.isEnabledByUser)//사용자에 의하여 좌표값을 실행 할 수 있을 경우
        {
            while (Input.location.status == LocationServiceStatus.Initializing && wait > 0)//초기화 진행중이면
            {
                wait--; // 기다리는 시간을 뺀다
            }
            //GPS를 잡는 대기시간
            if (Input.location.status != LocationServiceStatus.Failed)//GPS가 실행중이라면
            {
                // We start the timer to check each tick (every 3 sec) the current gps position
                InvokeRepeating("GetUserPostion", 0.0001f, 1.0f);//0.0001초에 실행하고 1초마다 해당 함수를 실행합니다.
            }
        }
    }
    void GetUserPostion()
    {
        userPos = Input.location.lastData;
        logitude = userPos.longitude;
        latitude = userPos.latitude;
    }
}
