using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mjHand : MonoBehaviour
{
    private Transform swordPos;
    private Transform playerPos;
    [SerializeField]
    public GameObject sw;
    private SpriteRenderer sr; // 플레이어의 바라보고 있는 방향을 받아오기 위한 변수.

    private void Awake()
    {
        playerPos = GameObject.Find("Player").transform;
        sr = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        swordPos = gameObject.transform;

    }


    private void Update()
    {
        handPos();
    }

    public void handPos()
    {
        if (!sr.flipX) // 오른쪽을 보고 있을때
        {
            swordPos.position = new Vector3(playerPos.position.x + 0.6f, playerPos.position.y + 0.3f, 0f);


        }
        if (sr.flipX) // 왼쪽을 보고 있을때
        {
            swordPos.position = new Vector3(playerPos.position.x - 0.6f, playerPos.position.y + 0.3f, 0f);
        }
    }

}