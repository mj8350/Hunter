using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mjCam : MonoBehaviour
{
    [SerializeField]
    Vector2 mapSize;

    private GameObject player;

    float cameraMoveSpeed = 7f;
    float height;
    float width;
    Vector3 top;

    void Start()
    {
        player = GameObject.Find("Player");

        height = Camera.main.orthographicSize;//카메라의 세로길이 구하기
        width = height * Screen.width / Screen.height;//카메라의 가로길이 구하기
        top.y = 2.5f;//캐릭터의 2.5유닛 위로 설정
    }

    void FixedUpdate()
    {
        LimitCameraArea();
    }

    void LimitCameraArea()
    {
        
        transform.position = Vector3.Lerp(transform.position,
                                          player.transform.position + top,
                                          Time.deltaTime * cameraMoveSpeed) ;
        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx, lx);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly, ly);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}

