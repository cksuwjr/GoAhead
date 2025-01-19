using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Arrow : PoolObject, IMovable
{
    private GameObject attacker;
    private int arrowDamage;
    private float arrowSpeed;
    private Vector3 moveDir;

    private bool isInit = false;

    public void Move(Vector3 direction)
    {
        transform.Translate(direction * (arrowSpeed * Time.deltaTime));
    }

    public void SetAct(bool act)
    {
        isInit = act;
    }

    public void SetSpeed(float speed)
    {
        arrowSpeed = speed;
    }

    public void InitArrow(GameObject attacker, Vector3 dir, float speed, int damage)
    {
        this.attacker = attacker;
        moveDir = dir;
        SetSpeed(speed);
        arrowDamage = damage;

        SetAct(true);

        Invoke("DissappearNaturaly", 3f);
    }

    private void Update()
    {
        if (!isInit) return;

        Move(moveDir);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject == attacker) return;
        if (collision.CompareTag(attacker.tag)) return;

        if(collision.TryGetComponent<IDamagable>(out IDamagable component))
        {
            component.TakeDamage(attacker, arrowDamage);
            ReturnToPool();
        }
    }

    private void DissappearNaturaly()
    {
        ReturnToPool();
    }

    private void OnDisable()
    {
        CancelInvoke();
        SetAct(false);   
    }
}
