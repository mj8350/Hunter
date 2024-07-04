using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolLabel : MonoBehaviour
{
    private ObjectPool ownerPool; //�ش� ���� ������ �ִ� Ǯ�� ��������. // yjh_ protected -> private

    public virtual void Create(ObjectPool pool)
    {
        ownerPool = pool;
        gameObject.SetActive(false); //��Ȱ��ȭ
    }

    //������ ���� ������Ʈ�� pool�� ��ȯ��Ű�� �Լ�.
    public virtual void Push()
    {
        ownerPool.Push(this);
    }
}
