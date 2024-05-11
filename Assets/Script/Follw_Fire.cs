using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follw_Fire : PoolLabel
{
    private Transform playerPos;

    private PoolLabel label;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player").transform;
        TryGetComponent<PoolLabel>(out label);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerPos.position, Time.deltaTime * 5f) ;
        Vector2 vec = new Vector2(transform.position.x - playerPos.position.x, transform.position.y - playerPos.position.y);
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 180f, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, Time.deltaTime * 5f);
        transform.rotation = rotation;

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<Yjh_Player_Edit>().isDamage)
            {
                collision.GetComponent<Yjh_Player_Edit>().Damege(0.25f);
                label.Push();
            }
        }else if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<YSMonster>().Damege(0.1f);
            label.Push();
        }
    }


}
