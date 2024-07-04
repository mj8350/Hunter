using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

using UnityEngine.SceneManagement;

public class Yjh_Player_Edit : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private GameObject obj; //mj추가
    private Vector3 fireDir; // ★
    private GameObject Yjh_obj; // ★
    private Vector3 FireSpawnPos; // ★
    private Quaternion FireSpawnRot; // ★

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpPower;
    private SpriteRenderer sr; // 플레이어의 스프라이트 렌더러를 받아올 변수.
    private Rigidbody2D rb;
    private Vector2 moveDir;

    private int layerIndex;

    //[SerializeField]
    //private GameObject jumpAnim; // 점프하는 모션을 받아올 변수 // 이동 모션은 Blend Tree에서 처리.
    private Animator Anime; // 플레이어의 모션을 받아오는 변수
    private int JumpCount = 0; // 이단 점프 구현을 위한 변수

    private Slider player_HP_bar;
    private int MyHP;
    [SerializeField]
    private float FGetDamage; // Slider의 Value에서의 float.플레이어가 받은 데미지. // 나중에 보스의 스크립트에서 public으로 공격력 구현시 보스 스크립트명(클래스)로 BossAttackDamage로 만들면 될듯..?
    //private int GetDamege; // Text로 받은 피해량을 표기할때 쓰일 int형 변수.

    [SerializeField]
    private GameObject Sword;
    private Vector3 SwordSpawnPos; // A입력을 받을때 

    private AudioSource soundMaster;
    [SerializeField]
    private AudioClip teleportSound;
    [SerializeField]
    private AudioClip fireBallSound;
    [SerializeField]
    private AudioClip playerJumpSound;
    [SerializeField]
    private AudioClip playerGetDamageSound;


    private void Awake()
    {
        player = gameObject;
        if (!TryGetComponent<SpriteRenderer>(out sr))
            Debug.Log("Player.cs - Awake()에서 sr참조 실패");
        if (!TryGetComponent<Rigidbody2D>(out rb))
            Debug.Log("Player.cs - Awake()에서 rg참조 실패");

        layerIndex = 1 << LayerMask.NameToLayer("Ground_Layer");

        TryGetComponent<Animator>(out Anime);

        player_HP_bar = GameObject.Find("Slider(player_HP)").GetComponent<Slider>(); // ★
        MyHP = (int)(player_HP_bar.value * 100);// ★
        sr = GetComponent<SpriteRenderer>();

        //mj추가
        TryGetComponent<AudioSource>(out soundMaster);
    }

    private void Update()
    {
        moveDir = new Vector2(Input.GetAxis("Horizontal"), 0f); // x축 이동값.

        // 플레이어가 이동 가능한 X좌표 -25 ~ 25
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -25f, 25f), transform.position.y); // x축 이동 제한.

        if (rb.velocity.x > 0.1f) // 캐릭터 방향 전환. 오른쪽 이동 시
        {
            sr.flipX = false;
            Anime.SetFloat("Speed", 1f);
        }
        else if (rb.velocity.x < -0.1f) // 왼쪽 이동 시
        {
            sr.flipX = true;
            Anime.SetFloat("Speed", 1f);
        }
        else if (rb.velocity.x == 0f)
        {
            Anime.SetFloat("Speed", 0f);
        }
        //---------------------------------------------------------- 2단 점프 로직.
        if (isGround())
        {
            //Debug.Log("그라운드");
            JumpCount = 0;
        }
        if (JumpCount < 2 && Input.GetKeyDown(KeyCode.UpArrow))
        {
            soundMaster.PlayOneShot(playerJumpSound, 0.1f);
            //Debug.Log("값 증가");
            JumpCount++;
            rb.velocity = Vector2.up * jumpPower;
            Anime.SetTrigger("Jump");
        }
        //----------------------------------------------------------
        if (isColor)
            sr.color = new Color(1f, 1f, 1f);

        //FireShoot(); // 파이어볼 스킬 호출. ★21.02.02.11:37

        if(player_HP_bar.value <= 0f)
        {
            SceneManager.LoadScene("DeadScene");
        }
    }

    public void Teleport() // 텔레포트
    {
        soundMaster.PlayOneShot(teleportSound, 0.1f);
        if (!sr.flipX) // 오른쪽을 바라보는 상태에서
        {
            if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바가 눌리면
            {
                transform.position = new Vector3(transform.position.x + 3f, transform.position.y, transform.position.z);
            }
        }
        if (sr.flipX) // 왼쪽을 바라보는 상태에서
        {
            if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바가 눌리면
            {
                transform.position = new Vector3(transform.position.x - 3f, transform.position.y, transform.position.z);
            }
        }
    }

    //public void FireShoot()//Fire스킬 // 원본.
    //{
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {

    //        obj = PoolManager.Instance.pools[0].Pop();

    //        obj.transform.position = transform.position;
    //    }
    //}
    public void FireShoot()//Fire스킬 // 수정 작업본. ★ yjh_
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            soundMaster.PlayOneShot(fireBallSound);
            if (!sr.flipX) // 오른쪽을 바라보는 경우
            { 
                FireSpawnPos = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
                FireSpawnRot = Quaternion.identity;
            }
            if (sr.flipX) // 왼쪽을 바라보는 경우
            {
                FireSpawnPos = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
                FireSpawnRot = new Quaternion(0f, 0f, 180f, 0f);
            }
            fireDir = FireSpawnPos - transform.position; // 투사체의 방향벡터 생성.

            //Yjh_obj = Instantiate(obj, FireSpawnPos, FireSpawnRot); // 생성 소멸을 쓰는 방식. 파이어볼 프리팹 생성.

            Yjh_obj = PoolManager.Instance.pools[0].Pop(); // 오브젝트 풀링 방식. 파이어볼 프리팹 생성.
            Yjh_obj.transform.position = FireSpawnPos;
            Yjh_obj.transform.rotation = FireSpawnRot;

            Yjh_obj.GetComponent<Yjh_Fire_Boll>().SetMoveDir(fireDir, FireSpawnPos);
        }
    }

    private bool isGround()
    {
        if (rb.velocity.y > 0.1f)
            return false;
        return Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y),
                                             Vector2.down,
                                             0.7f,
                                             layerIndex);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDir.x * moveSpeed, rb.velocity.y);
    }

    public bool isDamage = true; // 현재 데미지를 받을 수 있는 상태인지
    bool isColor = true; // 색을 바꿀수 있는 상태인지
    /*private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && isDamage)
        {
            player_HP_bar.value = player_HP_bar.value - FGetDamage; // 플레이어가 피해를 받고
            //player_HP_bar.value = Mathf.Lerp(player_HP_bar.value, player_HP_bar.value - FGetDamage, 5f); // 선형보간. 부드럽게 체력이 닳는 구현을 할줄 모르겠음...ㅠ
            isDamage = false; // 피해를 받지 않는 상태로 만듬.
            Invoke("GetDamage_Posible", 1f); // 1초뒤 다시 피해를 받을 수 있는 상태로
            //MyHP = (int)(player_HP_bar.value * 100);// ★
        }
        if (!isDamage && isColor) // 피해를 받을 수 없는 상태라면. 무적이라면.
        {
            sr.color = new Color(0.7f, 0.7f, 0.7f, 0.5f);
            isColor = false;
            Invoke("ChangeColor", 0.1f);
        }
    }*/
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") &&isDamage)
        {
            soundMaster.PlayOneShot(playerGetDamageSound, 0.1f);
            player_HP_bar.value = player_HP_bar.value - FGetDamage; // 플레이어가 피해를 받고
            //player_HP_bar.value = Mathf.Lerp(player_HP_bar.value, player_HP_bar.value - FGetDamage, 5f); // 선형보간. 부드럽게 체력이 닳는 구현을 할줄 모르겠음...ㅠ
            isDamage = false; // 피해를 받지 않는 상태로 만듬.
            Invoke("GetDamage_Posible", 1f); // 1초뒤 다시 피해를 받을 수 있는 상태로
            //MyHP = (int)(player_HP_bar.value * 100);// ★
        }
        if (collision.gameObject.CompareTag("Mini") && isDamage)
        {
            soundMaster.PlayOneShot(playerGetDamageSound, 0.1f);
            player_HP_bar.value = player_HP_bar.value - 0.05f; // 플레이어가 피해를 받고
            //player_HP_bar.value = Mathf.Lerp(player_HP_bar.value, player_HP_bar.value - FGetDamage, 5f); // 선형보간. 부드럽게 체력이 닳는 구현을 할줄 모르겠음...ㅠ
            isDamage = false; // 피해를 받지 않는 상태로 만듬.
            Invoke("GetDamage_Posible", 1f); // 1초뒤 다시 피해를 받을 수 있는 상태로
            //MyHP = (int)(player_HP_bar.value * 100);// ★
        }
        if (!isDamage && isColor) // 피해를 받을 수 없는 상태라면. 무적이라면.
        {
            sr.color = new Color(0.7f, 0.7f, 0.7f, 0.5f);
            isColor = false;
            Invoke("ChangeColor", 0.1f);
        }
    }
    

    private void GetDamage_Posible() { isDamage = true; }
    private void ChangeColor() { isColor = true; }

    public void Damege(float dam)
    {
        if (isDamage)
        {
            player_HP_bar.value = player_HP_bar.value - dam; // UI - Slider - value값을 조정.
            soundMaster.PlayOneShot(playerGetDamageSound,0.1f);
            isDamage = false;
            Invoke("GetDamage_Posible", 1f);
        }
        if (!isDamage && isColor) // 피해를 받을 수 없는 상태라면. 무적이라면.
        {
            sr.color = new Color(0.7f, 0.7f, 0.7f, 0.5f);
            isColor = false;
            Invoke("ChangeColor", 0.1f);
        }
    }

    public void InitPlayerHP() // 게임 시작과 재시작 시 호출할 플레이어 체력 재설정.
    {
        player_HP_bar.value = 1f; // 플레이어 슬라이더의 value값 1로 초기화.
    }
}
