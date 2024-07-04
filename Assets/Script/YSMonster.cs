using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.SceneManagement;

public class YSMonster : MonoBehaviour // 몬스터에 적용된 스크립트.
{

    private Transform leg;

    private Slider Monster_HP_bar;
    private Transform playerPos;
    [SerializeField]
    private float FGetDamage_1; // Slider의 Value에서의 float.플레이어가 받은 데미지. // 플레이어한테 받는 피해

    private Animator BossAnims;

    private bool isColor = true; // 색을 바꿀수 있는 상태인지
    private SpriteRenderer sr; // 몬스터가 맞았을때 SpriteRenderer를 조작하기 위한 변수.

    private void Awake()
    {
        leg = GameObject.Find("Bossleg").transform;
        playerPos = GameObject.Find("Player").transform;
        Monster_HP_bar = GameObject.Find("Slider(Monster_HP)").GetComponent<Slider>();
        if (!TryGetComponent<SpriteRenderer>(out sr))
            Debug.Log("보스의 SpriteRenderer 참조 실패");
        legx.y = -13.5f;
        pla.y = -13.5f;
        legx.z = 0;
        pla.z = 0;
        if(!TryGetComponent<Animator>(out BossAnims))
            Debug.Log("보스의 Animator 참조 실패");
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

    /*private void OnTriggerEnter2D(Collider2D collision) // 트리거와 첫 접촉 시 호출
    {
        if (collision.gameObject.CompareTag("Fire")) // 접촉한 오브젝트의 태그가 Fire라면
        {
            Monster_HP_bar.value = Monster_HP_bar.value - FGetDamage_1; // UI - Slider - value값을 조정.

            StopCoroutine("ChangeColor");
            StartCoroutine("ChangeColor");
        }

        if(collision.gameObject.CompareTag("Sword"))
        {
            Monster_HP_bar.value = Monster_HP_bar.value - FGetDamage_1; // UI - Slider - value값을 조정.

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
        Monster_HP_bar.value = Monster_HP_bar.value - dam; // UI - Slider - value값을 조정.

        //StopCoroutine("ChangeColor");
        StartCoroutine("ChangeColor");
    }




}
