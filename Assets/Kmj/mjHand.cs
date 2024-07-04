using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mjHand : MonoBehaviour
{
    private Transform swordPos;
    private Transform playerPos;
    [SerializeField]
    public GameObject sw;
    private SpriteRenderer sr; // �÷��̾��� �ٶ󺸰� �ִ� ������ �޾ƿ��� ���� ����.

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
        if (!sr.flipX) // �������� ���� ������
        {
            swordPos.position = new Vector3(playerPos.position.x + 0.6f, playerPos.position.y + 0.3f, 0f);


        }
        if (sr.flipX) // ������ ���� ������
        {
            swordPos.position = new Vector3(playerPos.position.x - 0.6f, playerPos.position.y + 0.3f, 0f);
        }
    }

}