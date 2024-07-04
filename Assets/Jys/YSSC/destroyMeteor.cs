using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyMeteor : PoolLabel
{
    private PoolLabel label;

    private void Awake()
    {
        label = GetComponent<PoolLabel>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Yjh_Player_Edit>().Damege(0.05f);
        }

        if(collision.CompareTag("Destroy"))
        {
            //Destroy(gameObject);
            label.Push();
        }
    }

    


}
