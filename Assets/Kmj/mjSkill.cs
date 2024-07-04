using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class mjSkill : MonoBehaviour
{
    [SerializeField]
    private Image[] SkillCool;

    [SerializeField]
    private TextMeshProUGUI[] SkillText;

    private float[] cool = { 0.5f, 5f, 3f };
    private bool[] coolcheck = { true, true, true };

    private Yjh_Player_Edit player;
    private AttackMotion attack;
    


    void Start()
    {

        player = GameObject.Find("Player").GetComponent<Yjh_Player_Edit>();
        attack = GameObject.Find("Sward_16").GetComponent<AttackMotion>();

        for (int i = 0; i < 3; i++)
        {
            SkillCool[i].fillAmount = 0f;
        }
    }

    void Update()
    {
        Skillstart();

    }
    private void Skillstart()
    {
        if (Input.GetKeyDown(KeyCode.A) && coolcheck[0])
        {
            StartCoroutine(SkillChk(0));
            attack.swPos();
        }
        if (Input.GetKeyDown(KeyCode.S) && coolcheck[1])
        {
            StartCoroutine(SkillChk(1));
            player.FireShoot();
        }
        if (Input.GetKeyDown(KeyCode.Space) && coolcheck[2])
        {
            StartCoroutine(SkillChk(2));
            player.Teleport();
        }
    }

    IEnumerator SkillChk(int skillNum)
    {

        coolcheck[skillNum] = false;
        SkillCool[skillNum].fillAmount = 1f;

        float lastTime = Time.time;
        float text = cool[skillNum];
        float percent = cool[skillNum] / 100;

        while (SkillCool[skillNum].fillAmount > 0)
        {
            text -= Time.deltaTime;
            SkillCool[skillNum].fillAmount = text / cool[skillNum];
            SkillText[skillNum].text = text.ToString("N1");
            yield return null;
        }

        coolcheck[skillNum] = true;
        SkillText[skillNum].text = "";


    }
}
