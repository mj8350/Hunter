using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yjh_Monster : MonoBehaviour // �̸� ���� ��ü�� ����� ��ũ��Ʈ. �÷��̾ ���� �����̴� ����.
{
    // �ʿ��� ��?
    // - Monster ������Ʈ
    // - Player�� ��ġ.

    // +�߰��� ���߿� ���͸� �����ų� ��ȯ�ϰų� �Ҷ��� ������ �ٸ� �̵��� ���� �߷°��� �޵��� ����.

    private GameObject Mst; // Monster�� ����.

    private float MstMoveSpeed = 0.5f;
    private SpriteRenderer MstSR;
    private Vector3 MstMoveDir;

    [SerializeField]
    private Transform leg;

    private GameObject Player;
    private Vector3 PlayerPos;

    Vector3 legx;
    Vector3 pla;

    private void Awake()
    {
        Mst = gameObject;

        if (!Mst.TryGetComponent<SpriteRenderer>(out MstSR))
            Debug.Log("Yjh_MstWalk.cs - Monster ������Ʈ�� SpriteRenderer������Ʈ�� �޾ƿ��� ���߽��ϴ�.");

        Player = GameObject.Find("Player");
        if (Player == null)
            Debug.Log("Player ��� �̸��� ������Ʈ�� ã�� ���߽��ϴ�.");

        legx.y = -13.5f;
        pla.y = -13.5f;
        legx.z = 0;
        pla.z = 0;
    }

    private void Update()
    {
        transform.position = leg.position;
        MstWalking();
    }

    public void MstWalking()
    {
        PlayerPos = Player.transform.position; // �÷��̾��� ��ġ ����.

        MstMoveDir = PlayerPos - leg.transform.position; // ������ ���� ����.
        MstMoveDir.y = 0f; MstMoveDir.z = 0f;
        leg.transform.position += MstMoveSpeed * Time.deltaTime * MstMoveDir; // ������ ������ �̵�

        if (PlayerPos.x < leg.transform.position.x) // �÷��̾ ���ͺ��� ���ʿ� �ִٸ�
            MstSR.flipX = false;
        else                                         // �÷��̾ ���ͺ��� �����ʿ� �ִٸ�
            MstSR.flipX = true;
    }
}
