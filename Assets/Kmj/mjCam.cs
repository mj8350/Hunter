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

        height = Camera.main.orthographicSize;//ī�޶��� ���α��� ���ϱ�
        width = height * Screen.width / Screen.height;//ī�޶��� ���α��� ���ϱ�
        top.y = 2.5f;//ĳ������ 2.5���� ���� ����
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

