using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissappearZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.CompareTag("Monster")) return;
        
        if(collision.TryGetComponent<PoolObject>(out var poolObject))
            poolObject.ReturnToPool();
    }
}
