using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSGroundMeteor : PoolLabel
{
    public GameObject MmteorPrefab;
    public float shootAngle = 0f;
    public float shootForce = 0f;

    private PoolLabel label;

    public Rigidbody2D rb;

    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        label = GetComponent<PoolLabel>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        /*float radianAngle = shootAngle * Mathf.Deg2Rad;
        Vector2 shootDirection = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));
        rb.AddForce(shootDirection * shootForce, ForceMode2D.Impulse);*/
    }

    private void FixedUpdate()
    {
        

        /*if (rb.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }*/
    }


    public void Turn(int a)
    {
        StartCoroutine(Turning(a));

    }

    private float rotateValue;
    IEnumerator Turning(int a)
    {
        rotateValue = Time.deltaTime * a * 50f;

        while (gameObject.activeSelf == true)
        {
            transform.Rotate(0, 0, rotateValue);

            yield return null;

        }
    }

    public void Sr(bool b)
    {
        sr.flipX=b;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Yjh_Player_Edit>().Damege(0.05f);
            label.Push();
            //Destroy(gameObject);
        }
        if (collision.CompareTag("Destroy"))
        {
            label.Push();
        }
    }

}
