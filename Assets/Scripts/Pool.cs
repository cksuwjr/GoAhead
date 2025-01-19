using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{

    [SerializeField] private PoolObject poolObjectPrefab;

    [SerializeField] private int poolCount = 5;

    private Queue<PoolObject> poolObjects = new Queue<PoolObject>();

    private void Allocate()
    {
        for (int i = 0; i < poolCount; i++)
        {
            var poolObject = Instantiate(poolObjectPrefab, transform);
            poolObject.Create(this);
            poolObjects.Enqueue(poolObject);
        }
    }

    public GameObject GetPoolObject()
    {
        if (poolObjects.Count < 1)
            Allocate();
        return poolObjects.Dequeue().gameObject;
    }

    public void ReturnPoolObject(PoolObject poolObject)
    {
        poolObject.gameObject.SetActive(false);
        poolObjects.Enqueue(poolObject);
    }
}
