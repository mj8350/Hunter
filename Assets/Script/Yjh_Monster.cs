using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yjh_Monster : MonoBehaviour // 쫄몹 몬스터 자체에 적용된 스크립트. 플레이어를 따라 움직이는 역할.
{
    // 필요한 것?
    // - Monster 오브젝트
    // - Player의 위치.

    // +추가로 나중에 몬스터를 던지거나 소환하거나 할때는 별도의 다른 이동은 끄고 중력값만 받도록 설정.

    private GameObject Mst; // Monster의 약자.

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
            Debug.Log("Yjh_MstWalk.cs - Monster 오브젝트의 SpriteRenderer컴포넌트를 받아오지 못했습니다.");

        Player = GameObject.Find("Player");
        if (Player == null)
            Debug.Log("Player 라는 이름의 오브젝트를 찾지 못했습니다.");

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
        PlayerPos = Player.transform.position; // 플레이어의 위치 설정.

        MstMoveDir = PlayerPos - leg.transform.position; // 몬스터의 방향 설정.
        MstMoveDir.y = 0f; MstMoveDir.z = 0f;
        leg.transform.position += MstMoveSpeed * Time.deltaTime * MstMoveDir; // 몬스터의 포지션 이동

        if (PlayerPos.x < leg.transform.position.x) // 플레이어가 몬스터보다 왼쪽에 있다면
            MstSR.flipX = false;
        else                                         // 플레이어가 몬스터보다 오른쪽에 있다면
            MstSR.flipX = true;
    }
}
