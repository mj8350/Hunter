using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yjh_Fire_Boll : PoolLabel // 파이어볼.
{
    private float moveSpeed = 5f;
    private bool isInit = false;
    private Vector3 moveDir;

    private Vector3 FireBallPos; // 파이어볼을 쏜 시점의 파이어볼 좌표.

    private PoolLabel label;

    private float fireDam;//불 데미지

    public void SetMoveDir(Vector3 newDir, Vector3 firespawnPos)
    {
        isInit = true;
        moveDir = newDir;
        FireBallPos = firespawnPos;
    }
    
    private void Awake() // Awake는 파이어볼이 SetActive(true)가 된 순간 호출.
    {
        TryGetComponent<PoolLabel>(out label);
        FireBallPos = transform.position;
        //FireBallPos = new Vector2(Mathf.Clamp(transform.position.x, -10f, 10f), transform.position.y);
        // 호출된 순간의 파이어볼의 좌표을 기준으로 x축을 -10 ~ 10 이동 제한.

        fireDam = 0.05f;
    }

    private void Update()
    {

        if (isInit)
        {
            transform.position += moveDir * (moveSpeed * Time.deltaTime); // 일단 날아가라.
        }
        
        if(moveDir.x > 0.1f) // 오른쪽으로 날아가면서
        {
            //Debug.Log("오른쪽으로 날아가려 시도함");
            if(FireBallPos.x + 10f < transform.position.x) // 제한한 x좌표보다 현재 날아가고 있는 자신의 x좌표가 커지면
                label.Push();
        }
        else if (moveDir.x < -0.1f) // 왼쪽으로 날아가면서
        {
            //Debug.Log("왼쪽으로 날아가려 시도함");
            if (FireBallPos.x - 10f > transform.position.x) // 제한한 x좌표보다 현재 날아가고 있는 자신의 x좌표가 더 작아진다면
                label.Push();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<YSMonster>().Damege(fireDam);
            label.Push();
        }
        if (collision.CompareTag("monster"))
        {
            collision.GetComponent<Monsterleg>().Die();
            label.Push();
        }
    }

}
