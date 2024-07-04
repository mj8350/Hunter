using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private PoolLabel targetLabel; // 해당 풀을 통해서 관리할 대상 오브젝트. // yjh_ GameObject -> PoolLabel

    [SerializeField]
    private int allocateCount; // 초기에 생성시킬 오브젝트의 갯수

    private Stack<PoolLabel> poolStack = new Stack<PoolLabel>();
    private GameObject obj; // yjh_

    private void Awake()
    {
        Allocate();
    }

    public void Allocate() // 풀에 오브젝트를 생성해 내는 함수. // yjh_ private -> public
    {
        for (int i = 0; i < allocateCount; i++)
        {
            //GameObject label = Instantiate(targetLabel, transform);
            //label.GetComponent<PoolLabel>().Create(this);
            //poolStack.Push(label.GetComponent<PoolLabel>());
            //------------------------------------------------------
            PoolLabel allocateObj = Instantiate<PoolLabel>(targetLabel, this.transform);
            allocateObj.Create(this);
            poolStack.Push(allocateObj); // yjh_
        }
    }

    //PoolLabel label2;

    //public GameObject Pop()
    //{
    //    label2 = poolStack.Pop();
    //    label2.gameObject.SetActive(true);

    //    return label2.gameObject;
    //}

    public GameObject Pop() // yjh_
    {
        obj = poolStack.Pop().gameObject;
        obj.SetActive(true);
        return obj;
    }

    public void Push(PoolLabel returnLabel)
    {
        //if (returnLabel.gameObject.activeSelf)
        //{
        //    returnLabel.gameObject.SetActive(false);
        //    poolStack.Push(returnLabel);
        //}
        //--------------------------------------------
        returnLabel.gameObject.SetActive(false);
        poolStack.Push(returnLabel); // yjh_
    }

    
}
