using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private Pool pool;

    public virtual void Create(Pool pool)
    {
        this.pool = pool;
        gameObject.SetActive(false);
    }

    public virtual void ReturnToPool()
    {
        pool.ReturnPoolObject(this);
    }
}
