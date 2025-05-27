using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance;

    public static PoolManager Instance => instance;

    public Pool monsterPool;
    public Pool arrowPool;
    public Pool itemBoxPool;
    public Pool doubleItemBoxPool;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject); else instance = this;
    }
}
