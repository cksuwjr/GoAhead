using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Monster : PoolObject, IMovable, IDamagable
{
    private float moveSpeed;
    private bool isInit = false;

    private int maxHp = 4;
    private int curHp = 4;

    public bool isDead => curHp <= 0;

    private void Update()
    {
        if (isInit)
            Move(Vector3.forward);
    }

    public void Move(Vector3 direction)
    {
        transform.Translate(direction * (moveSpeed * Time.deltaTime));
    }

    public void SetAct(bool act)
    {
        isInit = act;
        curHp = maxHp;
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void TakeDamage(GameObject attacker, int damage)
    {
        if (isDead) return;

        curHp -= damage;
        if (curHp <= 0) OnDie(); else OnDamaged();
    }

    private void OnDamaged()
    {

    }

    private void OnDie()
    {
        SetAct(false);
        ReturnToPool();
    }
}
