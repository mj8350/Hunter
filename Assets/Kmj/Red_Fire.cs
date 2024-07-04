using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_Fire : PoolLabel
{
    private float moveSpeed = 5f;
    private bool isInit = false;
    private Vector3 moveDir;

    private Vector3 FireBallPos; // ���̾�� �� ������ ���̾ ��ǥ.

    private PoolLabel label;

    private float fireDam;//�� ������


    public void SetMoveDir(Vector3 newDir, Vector3 firespawnPos)
    {
        isInit = true;
        moveDir = newDir;
        FireBallPos = firespawnPos;
    }

    private void Awake() // Awake�� ���̾�� SetActive(true)�� �� ���� ȣ��.
    {
        TryGetComponent<PoolLabel>(out label);
        FireBallPos = transform.position;
        //FireBallPos = new Vector2(Mathf.Clamp(transform.position.x, -10f, 10f), transform.position.y);
        // ȣ��� ������ ���̾�� ��ǥ�� �������� x���� -10 ~ 10 �̵� ����.

        fireDam = 0.1f;
    }


    private void Update()
    {
        if (isInit)
        {
            transform.position += moveDir * (moveSpeed * Time.deltaTime); // �ϴ� ���ư���.
        }

        if (moveDir.x > 0.1f) // ���������� ���ư��鼭
        {
            //Debug.Log("���������� ���ư��� �õ���");
            if (FireBallPos.x + 10f < transform.position.x) // ������ x��ǥ���� ���� ���ư��� �ִ� �ڽ��� x��ǥ�� Ŀ����
                label.Push();
                //Destroy(gameObject);
        }
        else if (moveDir.x < -0.1f) // �������� ���ư��鼭
        {
            //Debug.Log("�������� ���ư��� �õ���");
            if (FireBallPos.x - 10f > transform.position.x) // ������ x��ǥ���� ���� ���ư��� �ִ� �ڽ��� x��ǥ�� �� �۾����ٸ�
                label.Push();
                //Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Yjh_Player_Edit>().Damege(fireDam);
            label.Push();
            //Destroy(gameObject);
        }
    }
}
