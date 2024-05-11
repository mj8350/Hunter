using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mjMonsterControl : MonoBehaviour
{

    private Transform playerPos;
    private GameObject boss;
    private GameObject bossleg;
    private Rigidbody2D bossRig;

    [SerializeField]
    private GameObject Redobj;
    [SerializeField]
    private GameObject Folobj;

    private SpriteRenderer sr;
    private Vector3 FireSpawnPos;
    private Quaternion FireSpawnRot;
    private Vector3 fireDir;
    private GameObject fireobj;
    private GameObject follwobj;

    private GameObject Pm1;

    private Animator anime; // yjh : �̹� �ֱ淡 �̰� ���ڽ��ϴ�!

    private Vector3 target;

    private AudioSource soundMaster;
    [SerializeField]
    private AudioClip BossJumpSound;
    [SerializeField]
    private AudioClip BossStempSound;

    private void Start()
    {
        bossleg = GameObject.Find("Bossleg");
        playerPos = GameObject.Find("Player").transform;
        boss = GameObject.Find("Boss_");
        Pm1 = GameObject.Find("PoolManager");


        sr = boss.GetComponent<SpriteRenderer>();


        bossRig = bossleg.GetComponent<Rigidbody2D>();
        anime = boss.GetComponent<Animator>();
        target.y = -6f;

        vec.Add(new Vector2(0, -11f));
        vec.Add(new Vector2(0, -12.25f));
        vec.Add(new Vector2(0, -13.5f));

        if (!boss.TryGetComponent<Animator>(out anime))
            Debug.Log("mjMonsterControl.cs - start()���� Animator ���� ����");

        TryGetComponent<AudioSource>(out soundMaster);
    }

    void Update()
    {

        //Boss2();
        //StartCoroutine(mjBoss2());
    }

    public void StartBoss1()//���� �ϴÿ��� �������
    {
        a = true;
        StartCoroutine(mjBoss1());
    }


    bool a;
    IEnumerator mjBoss1()
    {
        int count = 0;
        bool sound = true;
        while (a)
        {

            bossRig.gravityScale = 0;
            //target.x = xPos.x;
            target.x = playerPos.position.x;

            bossleg.transform.position = Vector3.MoveTowards(bossleg.transform.position, target, Time.deltaTime * 15);
            yield return null;
            
            if (sound)
            {
                soundMaster.PlayOneShot(BossJumpSound);
                sound = false;
            }

            if (bossleg.transform.position == target)
            {
                a = false;
                yield return new WaitForSeconds(0.3f);
                bossRig.gravityScale = 8f;
                yield return new WaitForSeconds(0.5f);
                soundMaster.PlayOneShot(BossStempSound);
                yield return new WaitForSeconds(0.5f);
                a = true;
                //anime.SetBool("isGround", false);
                bossRig.gravityScale = 1f;

                count ++;

                anime.SetBool("jump11",true); // yjh �߰�
                sound = true;
            }
            if (count >= 2)
            {
                a = false;
                anime.SetBool("jump11", false); // yjh �߰�
            }
        }
    }

    List<Vector2> vec = new List<Vector2>();
    public void Boss2(int rand)//���� ���� ������ ����
    {
        //if (Input.GetKeyDown(KeyCode.D))
        {
            if (sr.flipX) // �������� �ٶ󺸴� ���
            {
                FireSpawnPos = new Vector3(boss.transform.position.x + 2f, vec[rand].y, boss.transform.position.z);
                FireSpawnRot = Quaternion.identity;
                fireDir = new Vector3(1, 0, 0);
            }
            if (!sr.flipX) // ������ �ٶ󺸴� ���
            {
                FireSpawnPos = new Vector3(boss.transform.position.x - 2f, vec[rand].y, boss.transform.position.z);
                FireSpawnRot = new Quaternion(0f, 0f, 180f, 0f);
                fireDir = new Vector3(-1, 0, 0);
            }
            //fireDir = FireSpawnPos - boss.transform.position; // ����ü�� ���⺤�� ����.

            //fireobj = Instantiate(Redobj, FireSpawnPos, FireSpawnRot); // ���� �Ҹ��� ���� ���. ���̾ ������ ����.

            fireobj = PoolManager.Instance.pools[1].Pop(); // ������Ʈ Ǯ�� ���. ���̾ ������ ����.

            fireobj.transform.position = FireSpawnPos;
            fireobj.transform.rotation = FireSpawnRot;

            fireobj.GetComponent<Red_Fire>().SetMoveDir(fireDir, FireSpawnPos);
        }


    }
    int lastrand = 2;
    int rand;
    IEnumerator mjBoss2()
    {
        int count = 0;

        while (a)
        {
            rand = UnityEngine.Random.Range(0, 3);
            while (rand == lastrand)
            {
                rand = UnityEngine.Random.Range(0, 3);
            }
            lastrand = rand;

            a = false;
            Boss2(rand);
            yield return new WaitForSeconds(0.5f);
            count++;
            yield return null;
            a = true;
            if (count >= 15)
                break;

        }

    }

    public void StartBoss2()
    {
        a = true;
        StartCoroutine(mjBoss2());
    }


    public void Boss3()//���� ����ź �߻�
    {
        if (sr.flipX) // �������� �ٶ󺸴� ���
        {
            FireSpawnPos = new Vector3(boss.transform.position.x + 2f, boss.transform.position.y, boss.transform.position.z);
            FireSpawnRot = Quaternion.identity;
        }
        if (!sr.flipX) // ������ �ٶ󺸴� ���
        {
            FireSpawnPos = new Vector3(boss.transform.position.x - 2f, boss.transform.position.y, boss.transform.position.z);
            FireSpawnRot = new Quaternion(0f, 0f, 180f, 0f);
        }
        follwobj = PoolManager.Instance.pools[2].Pop();
        //follwobj = Instantiate(Folobj, FireSpawnPos, FireSpawnRot);
        follwobj.transform.position = FireSpawnPos;
        follwobj.transform.rotation = FireSpawnRot;
    }

    public void StartBoss3()
    {
         Boss3();
    }

}
