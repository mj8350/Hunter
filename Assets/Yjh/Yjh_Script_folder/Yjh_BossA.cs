using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.UI;

public class Yjh_BossA : MonoBehaviour // Yjh_BossController(�� ������Ʈ)�� ����. // ������ A����.
{
    // �ʿ��� ��.
    // -�÷��̾� ��ġ, ���� ��ġ
    
    private GameObject Boss;
    [SerializeField]
    private GameObject monster; // ������ ������ ���߸� ������ ���� ������. (Ǯ�� ����?)
    private SpriteRenderer BossSR;
    private Animator BossAnims;
    private Transform BossTransform;
    private Slider BossSlider;

    private GameObject objA;
    private GameObject objB;
    private GameObject objC;

    private Transform PlayerTransform;

    private AudioSource RushSound;
    [SerializeField]
    private AudioClip RushClip;

    private void Awake()
    {
        Boss = GameObject.Find("Bossleg");
        if (Boss == null)
            Debug.Log("Yjh_BossA.cs - Bossleg ������Ʈ�� ã�� ���߽��ϴ�.");
        if(!Boss.TryGetComponent<Transform>(out BossTransform))
            Debug.Log("Yjh_BossA.cs - Bossleg ������Ʈ�� Transform ������Ʈ�� �������� ���߽��ϴ�.");
        if (!Boss.transform.GetChild(0).TryGetComponent<SpriteRenderer>(out BossSR))
            Debug.Log("Yjh_BossA.cs - Bossleg ������Ʈ�� SpriteRenderer ������Ʈ�� �������� ���߽��ϴ�.");
        if(!Boss.transform.GetChild(0).TryGetComponent<Animator>(out BossAnims))
            Debug.Log("Yjh_BossA.cs - Bossleg ������Ʈ�� Animator ������Ʈ�� �������� ���߽��ϴ�.");
        PlayerTransform = GameObject.Find("Player").transform;
        if (PlayerTransform == null)
            Debug.Log("Yjh_BossA.cs - Player ������Ʈ�� ã�� ���߰ų� Player�� Transform ������Ʈ�� �������� ���߽��ϴ�.");
        BossSlider = GameObject.Find("Slider(Monster_HP)").GetComponent<Slider>();

        TryGetComponent<AudioSource>(out RushSound);
    }

    

    private void Update()
    {
        //if(isSkillA) // ���A�� ���۵Ǹ�
        //    StartCoroutine(Rush());

    }

    public void StartBossA()
    {
        StartCoroutine(Rush());
    }

    private float stackTime;
    private float percent;
    private bool isSkillA = true;
    private bool Flag;
    IEnumerator Rush() // StartCoroutin�� ȣ��� �������� ������ ������ �������� ������ ����.
    {
        if (isSkillA)
        {
            isSkillA = false;
            Debug.Log("Rush�ڷ�ƾ ȣ��");
            stackTime = 0f;
            percent = 0f;
            Vector3 startPos = BossTransform.position;
            Vector3 startPos1111;// a
            Vector3 endPos;

            Vector3 moveDir;
            Vector3 moveDir11;

            if (PlayerTransform.position.x < BossTransform.position.x) // ������ �÷��̾�� �����ʿ� ������
            {
                moveDir11.x = 10;
                //startPos1111 = new Vector3(23f, Boss.transform.position.y, Boss.transform.position.z);
                endPos = BossTransform.position + new Vector3(1f, BossTransform.position.y, BossTransform.position.z); // �ް����� ���� ����.
                Flag = true;
            }
            else
            {
                moveDir11.x = -10;
                //startPos1111 = new Vector3(-23f, Boss.transform.position.y, Boss.transform.position.z);
                endPos = BossTransform.position - new Vector3(1f, BossTransform.position.y, BossTransform.position.z);
                Flag = false;
            }
            moveDir = startPos - endPos;

            if (Flag)
                BossSR.flipX = false;
            else
                BossSR.flipX = true;

            while (percent < 1f) // �ް�����
            {
                stackTime += Time.deltaTime;
                percent = stackTime / 2f;
                Vector3 newPos;

                newPos.x = Mathf.MoveTowards(startPos.x, Mathf.Clamp(endPos.x, -21f, 21f), percent);
                newPos.y = BossTransform.position.y; newPos.z = BossTransform.position.z;
                BossTransform.position = newPos;
                yield return null;
            }
            bool rush = true;
            while (rush) // ����
            {
                moveDir = endPos;
                moveDir11.y = 0f; moveDir11.z = 0f;
                BossTransform.position -= moveDir11 * Time.deltaTime * 7f;

                if (BossTransform.position.x < -23f)
                    rush = false;
                if (BossTransform.position.x > 23f)
                    rush = false;

                yield return null;
            }

            RushSound.PlayOneShot(RushClip);

            if (BossSlider.value > 0.8f)
            {
                //Instantiate(monster, new Vector3(0f, 15f, 0f), Quaternion.identity);
                objA = PoolManager.Instance.pools[3].Pop();
                objA.transform.position = new Vector3(0f, 5f, 0f);
            }
            else if (BossSlider.value > 0.5f)
            {
                //Instantiate(monster, new Vector3(-18f, 15f, 0f), Quaternion.identity);
                //Instantiate(monster, new Vector3(18f, 15f, 0f), Quaternion.identity);
                objA = PoolManager.Instance.pools[3].Pop();
                objB = PoolManager.Instance.pools[3].Pop();

                objA.transform.position = new Vector3(-18f, 5f, 0f);
                objB.transform.position = new Vector3(18f, 5f, 0f);
            }
            else if (BossSlider.value > 0f)
            {
                //Instantiate(monster, new Vector3(-12f, 15f, 0f), Quaternion.identity);
                //Instantiate(monster, new Vector3(0f, 15f, 0f), Quaternion.identity);
                //Instantiate(monster, new Vector3(12f, 15f, 0f), Quaternion.identity);
                objA = PoolManager.Instance.pools[3].Pop();
                objB = PoolManager.Instance.pools[3].Pop();
                objC = PoolManager.Instance.pools[3].Pop();

                objA.transform.position = new Vector3(-12f, 5f, 0f);
                objB.transform.position = new Vector3(0f, 5f, 0f);
                objC.transform.position = new Vector3(12f, 5f, 0f);
            }

            isSkillA = true;
        }
    }
}
