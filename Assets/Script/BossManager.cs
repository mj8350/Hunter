using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    private Yjh_BossA bossA;
    private Yjh_BossB bossB;
    private YSmeteor bossCD;
    private mjMonsterControl boss123;

    private GameObject Boss;
    private GameObject Player;

    private Transform BossPos;


    void Start()
    {
        bossA = GameObject.Find("Yjh_BossController").GetComponent<Yjh_BossA>();
        bossB = GameObject.Find("Yjh_BossController").GetComponent<Yjh_BossB>();
        bossCD = GameObject.Find("MeteorSpawner").GetComponent<YSmeteor>();
        boss123 = GameObject.Find("MonsterControl").GetComponent<mjMonsterControl>();

        Boss = GameObject.Find("Boss_");
        Player = GameObject.Find("Player");

        BossPos = GameObject.Find("Bossleg").transform;

        lastSkill = 0;
        lasttime = Time.time-7;
        OnCool = false;
    }

    // Update is called once per frame
    void Update()
    {

        BossStart();
        //StartCoroutine(BossSkill());

    }

    
    bool OnCool = true;
    bool Wait = false;
    int rand = 1;
    int lastSkill;
    float lasttime;


    IEnumerator BossSkill()
    {
        if (OnCool && Pos() && !Wait)
        {
            OnCool = false;
            Wait = true;
            lasttime = Time.time;
            yield return new WaitForSecondsRealtime(0.5f);
            Boss.GetComponent<YSMonster>().BossStay();

            rand = Random.Range(1, 8);
            while(lastSkill==rand)
                rand = Random.Range(1, 8);

            //rand = 2;
            lastSkill = rand;
            yield return null;

            switch (rand)
            {
                case 1:
                    bossA.StartBossA();
                    lasttime -= 3f;
                    yield return null;
                    break;
                case 2:
                    bossB.StartBossB();
                    lasttime -= 5f;
                    yield return null;
                    break;
                case 3:
                    bossCD.StartBossC();
                    lasttime -= 3f;
                    yield return null;
                    break;
                case 4:
                    bossCD.StartBossD();
                    lasttime -= 4f;
                    yield return null;
                    break;
                case 5:
                    boss123.StartBoss1();
                    lasttime -= 2f;
                    yield return null;
                    break;
                case 6:
                    boss123.StartBoss2();
                    yield return null;
                    break;
                case 7:
                    boss123.StartBoss3();
                    lasttime -= 3f;
                    yield return null;
                    break;
            }
        }
        else if (Wait)
        {
            Debug.Log("두번째 if문");
            Boss.GetComponent<YSMonster>().BossStay();
            yield return null;
        }
        else
        {
            Debug.Log("세번째 if문");
            Boss.GetComponent<YSMonster>().BossWalking();
            yield return null;
        }
    }
    private void BossStart()
    {

        StartCoroutine(BossSkill());
        if (!OnCool)
        {
            if (Time.time >= lasttime + 10f)
                OnCool = true;
        }
        if (Wait)
        {
            if (Time.time >= lasttime + 7f)
                Wait = false;
        }
    }



    private bool Pos()
    {
        if (BossPos.position.x > Player.transform.position.x + 10f || BossPos.position.x < Player.transform.position.x - 10f)
            return false;
        else
            return true;
    }

}
