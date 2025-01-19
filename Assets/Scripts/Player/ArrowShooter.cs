using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour, IWeapon
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject arrowPrefab;

    private GameObject attacker;
    private bool isUsable = false;
    private float coolTime = 0.3f;
    private float nextAttackTime;

    private float shootIntervalAngle = 5f;

    private int arrowDamage = 1;
    private float arrowSpeed = 14f;
    private int arrowCount = 1;

    public int ArrowDamage => arrowDamage;
    public float ArrowSpeed => arrowSpeed;
    public int ArrowCount => arrowCount;



    public void Attack()
    {
        if (!isUsable) return;
        if (Time.time < nextAttackTime) return;
        ShootArrow();
        nextAttackTime = Time.time + coolTime;
    }

    public void SetOwner(GameObject owner)
    {
        attacker = owner;
    }

    public void SetUsable(bool usable)
    {
        isUsable = usable;
    }

    private void ShootArrow()
    {
        var startAngle = -shootIntervalAngle * (arrowCount - 1) / 2;
        var arrowAngle = 0f;
        var fireDir = Vector3.zero;
        Quaternion fireRotation;
        for(int i = 0;  i < arrowCount; i++)
        {
            arrowAngle = startAngle + shootIntervalAngle * i;
            fireRotation = shootPoint.rotation * Quaternion.Euler(0, arrowAngle, 0);
            fireDir = fireRotation * Vector3.forward;

            var arrow = PoolManager.Instance.arrowPool.GetPoolObject();
            arrow.transform.position = shootPoint.transform.position;
            arrow.SetActive(true);

            if(arrow.TryGetComponent<Arrow>(out Arrow arr))
                arr.InitArrow(attacker, fireDir, arrowSpeed, arrowDamage);
        }

    }
}
