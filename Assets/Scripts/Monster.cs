using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MonsterState
{
    MoveToApear,
    Move01,
    Move02,
    Move03,
    Attack01,
    Attack02,
    MoveToDesappear,
}


[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Monster : PoolObject, IMovable, IDamagable
{
    private Vector3 moveDir;
    private float moveSpeed;
    private bool isInit = false;

    private int maxHp = 4;
    private int curHp = 4;

    [SerializeField] private MonsterState currentState;
    [SerializeField] private MonsterState nextState = MonsterState.Move01;

    private float appearPointZ = 23f;
    public bool isDead => curHp <= 0;

    public delegate void OnHPChange(float value);
    public event OnHPChange onHpChanged;

    public delegate void OnDead(Monster monster);
    public static event OnDead onDead;


    private void Update()
    {
        if (!isInit) return;

        Move(moveDir);
    }

    private void OnDamaged()
    {
        onHpChanged?.Invoke(curHp);
    }

    private void OnDie()
    {
        onDead?.Invoke(this);
        SetAct(false);
        ReturnToPool();
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

    public void InitMonster(int maxHP, MonsterState nextState)
    {
        curHp = maxHp = maxHP;
        this.nextState = nextState;
        onHpChanged?.Invoke(curHp);
        ChangeState(MonsterState.MoveToApear);
    }

    public void ChangeState(MonsterState newState)
    {
        StopCoroutine(currentState.ToString());
        currentState = newState;
        StartCoroutine(currentState.ToString());
    }

    private IEnumerator MoveToApear()
    {
        moveDir = Vector3.forward;
        while (true)
        {
            if (transform.position.z <= appearPointZ)
            {
                moveDir = Vector3.zero;
                ChangeState(nextState);
            }
            yield return null;
        }
    }

    private IEnumerator Move01()
    {
        moveDir = Vector3.forward;
        yield return null;
    }

    private IEnumerator Move02()
    {
        yield return null;

        var playerPosition = new Vector3(Random.Range(-4.5f, 4.5f),0,0);
        var dir = playerPosition - transform.position;
        dir.y = transform.position.y;
        dir.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = targetRotation;
        moveDir = Vector3.forward;
        SetSpeed(2 * moveSpeed);

    }

    private IEnumerator Move03()
    {
        yield return YieldInstructionCache.WaitForSeconds(0.7f);

        var playerPosition = GameManager.Instance.player.transform.position;
        var dir = playerPosition - transform.position;
        dir.y = transform.position.y;

        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = targetRotation;
        moveDir = Vector3.forward;

        SetSpeed(2 * moveSpeed);
    }

    private IEnumerator Attack01()
    {
        yield return null;
    }

    private IEnumerator Attack02()
    {
        yield return null;
    }

    private IEnumerator MoveToDesappear() 
    {
        yield return null;
    }
}
