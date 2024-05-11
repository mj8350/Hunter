using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMotion : MonoBehaviour // 검에다가 적용된 스크립트.
{
    private Transform swordPos;
    private Transform handPos;
    private Transform playerPos;
    private SpriteRenderer sr; // 플레이어의 바라보고 있는 방향을 받아오기 위한 변수.
    private CapsuleCollider2D Ccol;

    private float swordDam;//칼 데미지

    Vector2 hitboxPos;
    Vector2 hitboxSize;

    private AudioSource attSound;

    private void Awake()
    {
        Ccol = GetComponent<CapsuleCollider2D>();
        handPos = GameObject.Find("hand").transform;
        playerPos = GameObject.Find("Player").transform;
        sr = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        gameObject.transform.position = handPos.position;
        swordPos = gameObject.transform;
        Ccol.isTrigger = false;
        hitboxSize = new Vector2(1.5f, 1.3f);//히트박스 사이즈
        swordDam = 0.01f;//칼 데미지

        TryGetComponent<AudioSource>(out attSound);
    }
    private float rotateValue;
    private float moveSpeed;
    private void Update()
    {
        //swPos();
    }

    public void swPos()
    {
        attSound.Play();

        if (!sr.flipX) // 오른쪽을 보고 있을때
        {
            if (Input.GetKeyDown(KeyCode.A)) // A 입력받으면
            {
                Ccol.isTrigger = true;
                StartCoroutine(SwardMove(-1));
                

                hitboxPos.x = handPos.position.x + 0.4f;
                hitboxPos.y = handPos.position.y - 0.3f;
                Collider2D[] hit = Physics2D.OverlapBoxAll(hitboxPos, hitboxSize, 0);
                foreach (Collider2D col in hit)
                {
                    if (col.CompareTag("Enemy"))
                    {
                        col.GetComponent<YSMonster>().Damege(swordDam);
                        //Destroy(col.gameObject);
                    }
                    if (col.CompareTag("monster"))
                    {
                        col.GetComponent<Monsterleg>().Die();
                        //Destroy(col.gameObject);
                    }
                }
            }
        }
        if (sr.flipX) // 왼쪽을 보고 있을때
        {
            if (Input.GetKeyDown(KeyCode.A)) // A 입력받으면
            {
                Ccol.isTrigger = true;
                StartCoroutine(SwardMove(1));

                hitboxPos.x = handPos.position.x - 0.22f;
                hitboxPos.y = handPos.position.y - 0.3f;
                Collider2D[] hit = Physics2D.OverlapBoxAll(hitboxPos, hitboxSize, 0);
                foreach (Collider2D col in hit)
                {
                    if (col.CompareTag("Enemy"))
                    {
                        col.GetComponent<YSMonster>().Damege(swordDam);
                        //Destroy(col.gameObject);
                    }
                    if (col.CompareTag("monster"))
                    {
                        col.GetComponent<Monsterleg>().Die();
                        //Destroy(col.gameObject);
                    }
                }
            }
        }
    }

    IEnumerator SwardMove(int a)
    {
        transform.rotation = Quaternion.Euler(0, 0, 45 * a);
        yield return null;
        float lasttime = Time.time;

        rotateValue = 300 * a * Time.deltaTime;
        moveSpeed = 5f;

        while (true)
        {
            swordPos.Rotate(0, 0, rotateValue);
            transform.Translate(Vector3.right * -(Time.deltaTime * moveSpeed * a));

            if (a == -1 && sr.flipX)
            {
                handPos.position = new Vector3(playerPos.position.x + 0.6f, playerPos.position.y + 0.3f, 0f);
            }
            if (a == 1 && !sr.flipX)
            {
                handPos.position = new Vector3(playerPos.position.x - 0.6f, playerPos.position.y + 0.3f, 0f);
            }


            yield return null;

            if (Time.time >= lasttime + 0.2f)
            {
                transform.rotation = Quaternion.identity;
                gameObject.transform.position = handPos.position;
                Ccol.isTrigger = false;
                break;
            }
        }
    }
    private void OnDrawGizmos()//히트박스 보이게하기
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(hitboxPos, hitboxSize);
    }

}
