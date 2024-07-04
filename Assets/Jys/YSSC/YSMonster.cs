using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.SceneManagement;

public class YSMonster : MonoBehaviour // ���Ϳ� ����� ��ũ��Ʈ.
{

    private Transform leg;

    private Slider Monster_HP_bar;
    private Transform playerPos;
    [SerializeField]
    private float FGetDamage_1; // Slider�� Value������ float.�÷��̾ ���� ������. // �÷��̾����� �޴� ����

    private Animator BossAnims;

    private bool isColor = true; // ���� �ٲܼ� �ִ� ��������
    private SpriteRenderer sr; // ���Ͱ� �¾����� SpriteRenderer�� �����ϱ� ���� ����.

    private void Awake()
    {
        leg = GameObject.Find("Bossleg").transform;
        playerPos = GameObject.Find("Player").transform;
        Monster_HP_bar = GameObject.Find("Slider(Monster_HP)").GetComponent<Slider>();
        if (!TryGetComponent<SpriteRenderer>(out sr))
            Debug.Log("������ SpriteRenderer ���� ����");
        legx.y = -13.5f;
        pla.y = -13.5f;
        legx.z = 0;
        pla.z = 0;
        if(!TryGetComponent<Animator>(out BossAnims))
            Debug.Log("������ Animator ���� ����");
    }

    private void Update()
    {
        transform.position = new Vector3(leg.position.x, leg.position.y + 1.5f,0f);

        if (transform.position.x < playerPos.position.x)
            sr.flipX = true;
        else
            sr.flipX = false;

        if (Monster_HP_bar.value <= 0f)
            SceneManager.LoadScene("VictoryScene");
    }

    Vector3 legx;
    Vector3 pla;
    float BossMoveSpeed=2.5f;
    public void BossWalking()
    {
        StartCoroutine(walk());
        BossAnims.SetTrigger("walk2");
    }
    public void BossStay()
    {
        StopCoroutine(walk());
        BossAnims.SetTrigger("stop");
    }
    IEnumerator walk()
    {
        {
            
            legx.x = leg.position.x;
            pla.x = playerPos.position.x;
            leg.position = Vector3.MoveTowards(legx, pla, Time.deltaTime * BossMoveSpeed);
            yield return null;
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision) // Ʈ���ſ� ù ���� �� ȣ��
    {
        if (collision.gameObject.CompareTag("Fire")) // ������ ������Ʈ�� �±װ� Fire���
        {
            Monster_HP_bar.value = Monster_HP_bar.value - FGetDamage_1; // UI - Slider - value���� ����.

            StopCoroutine("ChangeColor");
            StartCoroutine("ChangeColor");
        }

        if(collision.gameObject.CompareTag("Sword"))
        {
            Monster_HP_bar.value = Monster_HP_bar.value - FGetDamage_1; // UI - Slider - value���� ����.

            StopCoroutine("ChangeColor");
            StartCoroutine("ChangeColor");
        }
    }*/

    IEnumerator ChangeColor()
    {
        for (int i = 0; i < 7; i++)
        {
            sr.color = new Color(0.7f, 0.7f, 0.7f, 0.8f);
            yield return new WaitForSeconds(0.03f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.03f);
        }
    }


    public void Damege(float dam)
    {
        Monster_HP_bar.value = Monster_HP_bar.value - dam; // UI - Slider - value���� ����.

        //StopCoroutine("ChangeColor");
        StartCoroutine("ChangeColor");
    }




}
