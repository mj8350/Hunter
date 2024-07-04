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
    private GameObject obj; //mj�߰�
    private Vector3 fireDir; // ��
    private GameObject Yjh_obj; // ��
    private Vector3 FireSpawnPos; // ��
    private Quaternion FireSpawnRot; // ��

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpPower;
    private SpriteRenderer sr; // �÷��̾��� ��������Ʈ �������� �޾ƿ� ����.
    private Rigidbody2D rb;
    private Vector2 moveDir;

    private int layerIndex;

    //[SerializeField]
    //private GameObject jumpAnim; // �����ϴ� ����� �޾ƿ� ���� // �̵� ����� Blend Tree���� ó��.
    private Animator Anime; // �÷��̾��� ����� �޾ƿ��� ����
    private int JumpCount = 0; // �̴� ���� ������ ���� ����

    private Slider player_HP_bar;
    private int MyHP;
    [SerializeField]
    private float FGetDamage; // Slider�� Value������ float.�÷��̾ ���� ������. // ���߿� ������ ��ũ��Ʈ���� public���� ���ݷ� ������ ���� ��ũ��Ʈ��(Ŭ����)�� BossAttackDamage�� ����� �ɵ�..?
    //private int GetDamege; // Text�� ���� ���ط��� ǥ���Ҷ� ���� int�� ����.

    [SerializeField]
    private GameObject Sword;
    private Vector3 SwordSpawnPos; // A�Է��� ������ 

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
            Debug.Log("Player.cs - Awake()���� sr���� ����");
        if (!TryGetComponent<Rigidbody2D>(out rb))
            Debug.Log("Player.cs - Awake()���� rg���� ����");

        layerIndex = 1 << LayerMask.NameToLayer("Ground_Layer");

        TryGetComponent<Animator>(out Anime);

        player_HP_bar = GameObject.Find("Slider(player_HP)").GetComponent<Slider>(); // ��
        MyHP = (int)(player_HP_bar.value * 100);// ��
        sr = GetComponent<SpriteRenderer>();

        //mj�߰�
        TryGetComponent<AudioSource>(out soundMaster);
    }

    private void Update()
    {
        moveDir = new Vector2(Input.GetAxis("Horizontal"), 0f); // x�� �̵���.

        // �÷��̾ �̵� ������ X��ǥ -25 ~ 25
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -25f, 25f), transform.position.y); // x�� �̵� ����.

        if (rb.velocity.x > 0.1f) // ĳ���� ���� ��ȯ. ������ �̵� ��
        {
            sr.flipX = false;
            Anime.SetFloat("Speed", 1f);
        }
        else if (rb.velocity.x < -0.1f) // ���� �̵� ��
        {
            sr.flipX = true;
            Anime.SetFloat("Speed", 1f);
        }
        else if (rb.velocity.x == 0f)
        {
            Anime.SetFloat("Speed", 0f);
        }
        //---------------------------------------------------------- 2�� ���� ����.
        if (isGround())
        {
            //Debug.Log("�׶���");
            JumpCount = 0;
        }
        if (JumpCount < 2 && Input.GetKeyDown(KeyCode.UpArrow))
        {
            soundMaster.PlayOneShot(playerJumpSound, 0.1f);
            //Debug.Log("�� ����");
            JumpCount++;
            rb.velocity = Vector2.up * jumpPower;
            Anime.SetTrigger("Jump");
        }
        //----------------------------------------------------------
        if (isColor)
            sr.color = new Color(1f, 1f, 1f);

        //FireShoot(); // ���̾ ��ų ȣ��. ��21.02.02.11:37

        if(player_HP_bar.value <= 0f)
        {
            SceneManager.LoadScene("DeadScene");
        }
    }

    public void Teleport() // �ڷ���Ʈ
    {
        soundMaster.PlayOneShot(teleportSound, 0.1f);
        if (!sr.flipX) // �������� �ٶ󺸴� ���¿���
        {
            if (Input.GetKeyDown(KeyCode.Space)) // �����̽��ٰ� ������
            {
                transform.position = new Vector3(transform.position.x + 3f, transform.position.y, transform.position.z);
            }
        }
        if (sr.flipX) // ������ �ٶ󺸴� ���¿���
        {
            if (Input.GetKeyDown(KeyCode.Space)) // �����̽��ٰ� ������
            {
                transform.position = new Vector3(transform.position.x - 3f, transform.position.y, transform.position.z);
            }
        }
    }

    //public void FireShoot()//Fire��ų // ����.
    //{
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {

    //        obj = PoolManager.Instance.pools[0].Pop();

    //        obj.transform.position = transform.position;
    //    }
    //}
    public void FireShoot()//Fire��ų // ���� �۾���. �� yjh_
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            soundMaster.PlayOneShot(fireBallSound);
            if (!sr.flipX) // �������� �ٶ󺸴� ���
            { 
                FireSpawnPos = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
                FireSpawnRot = Quaternion.identity;
            }
            if (sr.flipX) // ������ �ٶ󺸴� ���
            {
                FireSpawnPos = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
                FireSpawnRot = new Quaternion(0f, 0f, 180f, 0f);
            }
            fireDir = FireSpawnPos - transform.position; // ����ü�� ���⺤�� ����.

            //Yjh_obj = Instantiate(obj, FireSpawnPos, FireSpawnRot); // ���� �Ҹ��� ���� ���. ���̾ ������ ����.

            Yjh_obj = PoolManager.Instance.pools[0].Pop(); // ������Ʈ Ǯ�� ���. ���̾ ������ ����.
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

    public bool isDamage = true; // ���� �������� ���� �� �ִ� ��������
    bool isColor = true; // ���� �ٲܼ� �ִ� ��������
    /*private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && isDamage)
        {
            player_HP_bar.value = player_HP_bar.value - FGetDamage; // �÷��̾ ���ظ� �ް�
            //player_HP_bar.value = Mathf.Lerp(player_HP_bar.value, player_HP_bar.value - FGetDamage, 5f); // ��������. �ε巴�� ü���� ��� ������ ���� �𸣰���...��
            isDamage = false; // ���ظ� ���� �ʴ� ���·� ����.
            Invoke("GetDamage_Posible", 1f); // 1�ʵ� �ٽ� ���ظ� ���� �� �ִ� ���·�
            //MyHP = (int)(player_HP_bar.value * 100);// ��
        }
        if (!isDamage && isColor) // ���ظ� ���� �� ���� ���¶��. �����̶��.
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
            player_HP_bar.value = player_HP_bar.value - FGetDamage; // �÷��̾ ���ظ� �ް�
            //player_HP_bar.value = Mathf.Lerp(player_HP_bar.value, player_HP_bar.value - FGetDamage, 5f); // ��������. �ε巴�� ü���� ��� ������ ���� �𸣰���...��
            isDamage = false; // ���ظ� ���� �ʴ� ���·� ����.
            Invoke("GetDamage_Posible", 1f); // 1�ʵ� �ٽ� ���ظ� ���� �� �ִ� ���·�
            //MyHP = (int)(player_HP_bar.value * 100);// ��
        }
        if (collision.gameObject.CompareTag("Mini") && isDamage)
        {
            soundMaster.PlayOneShot(playerGetDamageSound, 0.1f);
            player_HP_bar.value = player_HP_bar.value - 0.05f; // �÷��̾ ���ظ� �ް�
            //player_HP_bar.value = Mathf.Lerp(player_HP_bar.value, player_HP_bar.value - FGetDamage, 5f); // ��������. �ε巴�� ü���� ��� ������ ���� �𸣰���...��
            isDamage = false; // ���ظ� ���� �ʴ� ���·� ����.
            Invoke("GetDamage_Posible", 1f); // 1�ʵ� �ٽ� ���ظ� ���� �� �ִ� ���·�
            //MyHP = (int)(player_HP_bar.value * 100);// ��
        }
        if (!isDamage && isColor) // ���ظ� ���� �� ���� ���¶��. �����̶��.
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
            player_HP_bar.value = player_HP_bar.value - dam; // UI - Slider - value���� ����.
            soundMaster.PlayOneShot(playerGetDamageSound,0.1f);
            isDamage = false;
            Invoke("GetDamage_Posible", 1f);
        }
        if (!isDamage && isColor) // ���ظ� ���� �� ���� ���¶��. �����̶��.
        {
            sr.color = new Color(0.7f, 0.7f, 0.7f, 0.5f);
            isColor = false;
            Invoke("ChangeColor", 0.1f);
        }
    }

    public void InitPlayerHP() // ���� ���۰� ����� �� ȣ���� �÷��̾� ü�� �缳��.
    {
        player_HP_bar.value = 1f; // �÷��̾� �����̴��� value�� 1�� �ʱ�ȭ.
    }
}
