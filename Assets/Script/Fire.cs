using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : PoolLabel
{

    public float moveSpeed;

    private Vector3 vec;

    private void Start()
    {
        moveSpeed = 5f;
        vec = gameObject.transform.position;
        vec.x += 10f;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * (Time.deltaTime * moveSpeed));

        if (transform.position.x >= vec.x)
        {
            Destroy(gameObject);
        }
    }



}
