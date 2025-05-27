using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemBox : PoolObject, IMovable
{
    private TextMeshProUGUI itemText;
    private StatusType itemType;
    private float value;

    private bool isInit = false;
    private float moveSpeed;
    private Vector3 moveDir;

    private void Awake()
    {
        itemText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (!isInit) return;

        Move(moveDir);
    }

    private void SetItemText()
    {
        itemText.text = "";
        if (value < 0) itemText.text += "<color=red>";


        switch (itemType)
        {
            case StatusType.ArrowPower:
                itemText.text += "화살공격력" + "\n";
                if (value > 0) itemText.text += "+";
                itemText.text += value;
                break;
            case StatusType.ArrowSpeed:
                itemText.text += "투사체속도" + "\n";
                if (value > 0) itemText.text += "+";
                itemText.text += value + "f";
                break;
            case StatusType.ArrowCount:
                itemText.text += "화살개수" + "\n";
                if (value > 0) itemText.text += "+";
                itemText.text += value + "개";
                break;
            case StatusType.ShootSpeed:
                itemText.text += "공격속도" + "\n";
                if (value > 0) itemText.text += "+";
                itemText.text += value + "%";
                break;
            default:
                itemText.text += itemType.ToString() + "\n";
                itemText.text += value;
                break;
        }
        if (value < 0) itemText.text += "</color>";
    }

    public void Init(StatusType itemType, float value, float speed)
    {
        this.itemType = itemType;
        this.value = value;

        moveDir = Vector3.back;
        SetSpeed(speed);
        SetAct(true);


        SetItemText();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.CompareTag("Player")) return;

        if(collision.TryGetComponent<ArrowShooter>(out var shooter))
        {
            shooter.UpgradeShooter(itemType, value);
            ReturnToPool();
        }
    }

    public void SetAct(bool act)
    {
        isInit = act;
    }

    public void Move(Vector3 direction)
    {
        transform.Translate(direction * (moveSpeed * Time.deltaTime));
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
