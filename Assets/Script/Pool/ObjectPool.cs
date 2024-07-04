using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private PoolLabel targetLabel; // �ش� Ǯ�� ���ؼ� ������ ��� ������Ʈ. // yjh_ GameObject -> PoolLabel

    [SerializeField]
    private int allocateCount; // �ʱ⿡ ������ų ������Ʈ�� ����

    private Stack<PoolLabel> poolStack = new Stack<PoolLabel>();
    private GameObject obj; // yjh_

    private void Awake()
    {
        Allocate();
    }

    public void Allocate() // Ǯ�� ������Ʈ�� ������ ���� �Լ�. // yjh_ private -> public
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
