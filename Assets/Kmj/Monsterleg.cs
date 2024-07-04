using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsterleg : PoolLabel
{
    [SerializeField]
    private Yjh_Monster mst;
    private PoolLabel label;
    // Start is called before the first frame update
    void Start()
    {
        //mst = GetComponent<Yjh_Monster>();
        TryGetComponent<PoolLabel>(out label);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -25f, 25f), transform.position.y);
    }

    public void Die()
    {
        label.Push();
    }


}
