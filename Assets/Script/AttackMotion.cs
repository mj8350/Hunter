using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMotion : MonoBehaviour // �˿��ٰ� ����� ��ũ��Ʈ.
{
    private Transform swordPos;
    private Transform handPos;
    private Transform playerPos;
    private SpriteRenderer sr; // �÷��̾��� �ٶ󺸰� �ִ� ������ �޾ƿ��� ���� ����.
    private CapsuleCollider2D Ccol;

    private float swordDam;//Į ������

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
        hitboxSize = new Vector2(1.5f, 1.3f);//��Ʈ�ڽ� ������
        swordDam = 0.01f;//Į ������

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

        if (!sr.flipX) // �������� ���� ������
        {
            if (Input.GetKeyDown(KeyCode.A)) // A �Է¹�����
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
        if (sr.flipX) // ������ ���� ������
        {
            if (Input.GetKeyDown(KeyCode.A)) // A �Է¹�����
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
    private void OnDrawGizmos()//��Ʈ�ڽ� ���̰��ϱ�
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(hitboxPos, hitboxSize);
    }

}
