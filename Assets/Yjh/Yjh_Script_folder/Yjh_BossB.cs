using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yjh_BossB : MonoBehaviour // 보스 컨트롤러(빈 오브젝트)
{
    [SerializeField]
    private GameObject tonadoPrefab;
    [SerializeField]
    public GameObject monster;

    private GameObject Boss;
    private SpriteRenderer BossSR;

    private Vector3 tonadoPos;
    private Vector3[] spawn;
    private Vector3 spawnPos;

    private Vector3 spawnPos1 = Vector3.zero;
    private Vector3 spawnPos2 = Vector3.zero;
    private Vector3 spawnPos3 = Vector3.zero;

    private Rigidbody2D mmmr;

    private AudioSource JumpSound;
    private void Awake()
    {
        Debug.Log("Yjh_BossB 스크립트 활성화");
        Boss = GameObject.Find("Bossleg");
        if (!Boss.transform.GetChild(0).TryGetComponent<SpriteRenderer>(out BossSR))
            Debug.Log("Yjh_BossB.cs - Awake() - Boss의 SpriteRenderer 참조 실패");

        TryGetComponent<AudioSource>(out JumpSound);
    }

    private void Start()
    {
        //spawnPos1 = new Vector3(Boss.transform.position.x - 2f, Boss.transform.position.y + 3f, Boss.transform.position.z);
        //spawnPos2 = new Vector3(Boss.transform.position.x, Boss.transform.position.y + 3f, Boss.transform.position.z);
        //spawnPos3 = new Vector3(Boss.transform.position.x + 2f, Boss.transform.position.y + 3f, Boss.transform.position.z);
        //spawn = new Vector3[] { spawnPos1, spawnPos2, spawnPos3 };
    }

    private bool isSkillB = true;
    private void Update()
    {
        spawnPos1 = new Vector3(Boss.transform.position.x - 2f, Boss.transform.position.y + 3f, Boss.transform.position.z);
        spawnPos2 = new Vector3(Boss.transform.position.x, Boss.transform.position.y + 3f, Boss.transform.position.z);
        spawnPos3 = new Vector3(Boss.transform.position.x + 2f, Boss.transform.position.y + 3f, Boss.transform.position.z);
        spawn = new Vector3[] { spawnPos1, spawnPos2, spawnPos3 };
        //if (isSkillB)
        //{
        //    StartCoroutine(Tonado());
        //    StartCoroutine(SpawnMonster());
        //}
    }

    public void StartBossB()
    {

        StartCoroutine(Tonado());
        StartCoroutine(SpawnMonster());

    }


    private float stackTime;
    private float percent;
    private GameObject obj;
    IEnumerator Tonado()
    {
        isSkillB = false;
        stackTime = 0f;
        percent = 0f;
        if (isSkillB == false)
        {
            tonadoPos = new Vector3(Boss.transform.position.x, Boss.transform.position.y + 2f, Boss.transform.position.z);
            obj = Instantiate(tonadoPrefab, tonadoPos, Quaternion.identity);
            
        }

        while (percent < 1f)
        {
            obj.transform.position = new Vector3(Boss.transform.position.x, Boss.transform.position.y + 2f, Boss.transform.position.z);
            stackTime += Time.deltaTime;
            percent = stackTime / 5f;

            InvokeRepeating("turn", 0f, Time.deltaTime);
            yield return null;

            if (percent >= 1f)
            {
                Debug.Log("멈춰라 제발");
                StopCoroutine(Tonado());
                CancelInvoke("turn");
                //StopCoroutine(SpawnMonster());
                Destroy(GameObject.Find("Tonado(Clone)"));
                //isSkillB = true;
            }
        }

    }

    private void turn() { BossSR.flipX = !BossSR.flipX; }

    private float stackTimeA;
    private float percentA;
    public IEnumerator SpawnMonster()
    {
        stackTimeA = 0f;
        percentA = 0f;

        InvokeRepeating("spMon", 0f, 1f);

        while (percentA < 1f)
        {
            stackTimeA += Time.deltaTime;
            percentA = stackTimeA / 2.1f;
            //----------------------------------------------------aaaa
            //Vector3 newPos;
            //newPos = 5f * Time.deltaTime * fireDir;

            //monster.transform.position += newPos;

            //Debug.Log("현재 newPos의 x값은 " + newPos.x);
            //Debug.Log("현재 newPos의 y값은 " + newPos.y);
            //Debug.Log("현재 newPos의 z값은 " + newPos.z);

            //----------------------------------------------------
            yield return null;

            if (percentA >= 1f)
            {
                StopCoroutine(SpawnMonster());
                CancelInvoke("spMon");
            }
        }
    }

    GameObject mmm;
    private Vector2 fireDir;
    private void spMon()
    {
        int Num = Random.Range(0, 3); // 0,1,2 중 랜덤        
        spawnPos = spawn[Num];
        fireDir = spawnPos - Boss.transform.position;

        //mmm = Instantiate(monster, spawnPos, Quaternion.identity);
        

        JumpSound.Play();

        mmm = PoolManager.Instance.pools[3].Pop();
        mmm.transform.position = spawnPos;

        mmmr = mmm.GetComponent<Rigidbody2D>();
        mmmr.AddForce(fireDir * 3f, ForceMode2D.Impulse);
    }

    private void Dir(int dir)
    {
        if (dir == 0)
        {
            fireDir = spawnPos - Boss.transform.position;
        }
    }
}
