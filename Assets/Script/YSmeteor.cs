using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSmeteor : MonoBehaviour
{
    public GameObject Meteorobj; // 생성할 프리팹
    private GameObject boss;
    private Transform playerTf;
    public float spawnInterval = 0f; //생성 간격
    public float spawnForce = 0f; // 떨어지는 힘
    public float spawHeight = -10f; // 생성높이

    private SpriteRenderer sr;

    private float currtime = 0f;

    public Transform bossPos;


    private void Start()
    {
        bossPos = GameObject.Find("Boss_").transform;
        boss = GameObject.Find("Boss_");
        sr = boss.GetComponent<SpriteRenderer>();
        playerTf = GameObject.Find("Player").transform;
        //StartBossD();
    }



    private void Update()
    {
        //SpawnGround();
        //StartBossC();
        //SpawnMeteor();
    }


    public void StartBossC()
    {
        StartCoroutine(BossC());
    }

    IEnumerator BossC()
    {
        int count = 0;
        while (true)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(playerTf.position.x - 3f, playerTf.position.x + 3f), -3f, 0f);
            //GameObject Meteor = Instantiate(MeteorPrefab, spawnPosition, Quaternion.Euler(0f, 0f, -90f));

            Meteorobj = PoolManager.Instance.pools[4].Pop();

            Meteorobj.transform.position = spawnPosition;
            Meteorobj.transform.rotation = Quaternion.Euler(0f, 0f, -90f);

            Rigidbody2D Meteorrigidbody = Meteorobj.GetComponent<Rigidbody2D>();
            if (Meteorrigidbody != null)
            {
                Meteorrigidbody.AddForce(Vector2.down * spawnForce, ForceMode2D.Impulse);
            }
            currtime = 0;
            yield return new WaitForSeconds(0.5f);
            count++;
            yield return null;
            if (count >= 15)
                break;

        }
    }

    Vector2 Dir;

    //public void SpawnGround()
    public void BossD()
    {

        List<Vector3> pos = new List<Vector3>();

        if (sr.flipX) //오른쪽을 바라보는 경우
        {
            pos.Add(new Vector3(bossPos.position.x + Random.Range(0.2f, 0.5f), bossPos.position.y + 1f, 0));
            pos.Add(new Vector3(bossPos.position.x + Random.Range(0.5f, 1.0f), bossPos.position.y + 1f, 0));
            pos.Add(new Vector3(bossPos.position.x + Random.Range(1.0f, 1.5f), bossPos.position.y + 1f, 0));

            for (int i = 0; i < 3; i++)
            {
                Meteorobj = PoolManager.Instance.pools[5].Pop();
                Meteorobj.transform.position = pos[i];
                Meteorobj.transform.rotation = Quaternion.identity;
                Dir = pos[i] - bossPos.position;
                Meteorobj.GetComponent<YSGroundMeteor>().Sr(false);
                Meteorobj.GetComponent<YSGroundMeteor>().rb.AddForce(Dir * 5f, ForceMode2D.Impulse);
                Meteorobj.GetComponent<YSGroundMeteor>().Turn(-1);
                //Meteorobj.transform.rotation = Quaternion.Lerp(Meteorobj.transform.rotation, Quaternion.LookRotation(rot[i]),Time.deltaTime * 10f);
            }
        }

        if (!sr.flipX)
        {
            pos.Add(new Vector3(bossPos.position.x - Random.Range(0.2f, 0.5f), bossPos.position.y + 1f, 0));
            pos.Add(new Vector3(bossPos.position.x - Random.Range(0.5f, 1.0f), bossPos.position.y + 1f, 0));
            pos.Add(new Vector3(bossPos.position.x - Random.Range(1.0f, 1.5f), bossPos.position.y + 1f, 0));


            for (int i = 0; i < 3; i++)
            {
                Meteorobj = PoolManager.Instance.pools[5].Pop();
                Meteorobj.transform.position = pos[i];
                Meteorobj.transform.rotation = Quaternion.identity;
                Dir = pos[i] - bossPos.position;
                Meteorobj.GetComponent<YSGroundMeteor>().Sr(true);
                Meteorobj.GetComponent<YSGroundMeteor>().rb.AddForce(Dir * 5f, ForceMode2D.Impulse);
                Meteorobj.GetComponent<YSGroundMeteor>().Turn(1);
                //Meteorobj.transform.rotation = Quaternion.Lerp(Meteorobj.transform.rotation, Quaternion.LookRotation(rot[i]),Time.deltaTime * 10f);
            }
        }


    }

    IEnumerator BossD_()
    {
        for (int i = 0; i < 5; i++)
        {
            BossD();
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void StartBossD()
    {
        StartCoroutine(BossD_());
    }

    /*private void OnTriggerEnter2D(Collider2D collision) // 트리거와 첫 접촉 시 호출
    {
        Debug.Log("아야");
        if (collision.gameObject.CompareTag("Player"))
        {
            ; // 접촉한 오브젝트의 태그가 라면
        }
    }*/
}
