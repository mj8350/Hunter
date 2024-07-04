using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolLabel : MonoBehaviour
{
    private ObjectPool ownerPool; //해당 라벨을 가지고 있는 풀이 누구인지. // yjh_ protected -> private

    public virtual void Create(ObjectPool pool)
    {
        ownerPool = pool;
        gameObject.SetActive(false); //비활성화
    }

    //쓰임이 끝난 오브젝트를 pool에 반환시키는 함수.
    public virtual void Push()
    {
        ownerPool.Push(this);
    }
}
