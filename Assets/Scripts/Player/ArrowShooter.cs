using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum StatusType
{
    ArrowPower,
    ArrowSpeed,
    ArrowCount,
    ShootSpeed,
}


public class ArrowShooter : MonoBehaviour, IWeapon
{
    [SerializeField] private Transform shootPoint;

    private GameObject attacker;
    private bool isUsable = false;
    private float nextAttackTime;

    private float shootIntervalAngle = 5f;

    private const float basicShootSpeed = 1f;
    private float shootSpeedAdd = 0f;
    private int arrowDamage = 1;
    private float arrowSpeed = 14f;
    private int arrowCount = 1;

    
    public int ArrowDamage
    {
        get => arrowDamage;
        set 
        {
            if (value < 1) arrowDamage = 1;
            else arrowDamage = value;

            onStatChange?.Invoke(StatusType.ArrowPower, arrowDamage); 
        }
    }

    public float ArrowSpeed
    {
        get => arrowSpeed;
        set 
        { 
            arrowSpeed = value; 
            onStatChange?.Invoke(StatusType.ArrowSpeed, arrowSpeed); 
        }
    }

    public int ArrowCount 
    { 
        get => arrowCount; 
        set 
        {
            if (value < 1) arrowCount = 1;
            else arrowCount = value;

            onStatChange?.Invoke(StatusType.ArrowCount, arrowCount);
        }
    }

    public float ShootSpeed
    {
        get => shootSpeedAdd;
        set 
        { 
            shootSpeedAdd = value; 
            onStatChange?.Invoke(StatusType.ShootSpeed, shootSpeedAdd); 
        }
    }

    public float ShootReloadTime => 1f / (basicShootSpeed * (1 + shootSpeedAdd));


    public delegate void StatChange(StatusType changeType, float value);
    public event StatChange onStatChange;
    

    public void Attack()
    {
        if (!isUsable) return;
        if (Time.time < nextAttackTime) return;
        ShootArrow();
        nextAttackTime = Time.time + ShootReloadTime;
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

    public void UpgradeShooter(StatusType upgradeType, float value)
    {
        switch (upgradeType)
        {
            case StatusType.ArrowPower:
                ArrowDamage += (int)value;
                break;
            case StatusType.ArrowSpeed:
                ArrowSpeed += value;
                break;
            case StatusType.ArrowCount:
                ArrowCount += (int)value;
                break;
            case StatusType.ShootSpeed:
                ShootSpeed += (0.01f * value);
                break;
            default:
                break;
        }
    }
}
