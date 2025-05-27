using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoubleItemBox : PoolObject, IMovable
{
    private TextMeshProUGUI itemText;
    private Dictionary<StatusType, float> items;

    private bool isInit = false;
    private float moveSpeed;
    private Vector3 moveDir;

    private void Awake()
    {
        itemText = GetComponentInChildren<TextMeshProUGUI>();
        items = new Dictionary<StatusType, float>();
    }

    private void Update()
    {
        if (!isInit) return;

        Move(moveDir);
    }

    private void SetItemText()
    {
        var keys = items.Keys;
        itemText.text = "";
        foreach (var key in keys)
        {
            if (items.ContainsKey(key))
            {
                float value;
                items.TryGetValue(key, out value);
                if (value < 0) itemText.text += "<color=red>";
                if (value > 0) itemText.text += "<color=blue>";
                switch (key)
                {
                    case StatusType.ArrowPower:
                        itemText.text += "화살공격력" + "\n";
                        if (value > 0) itemText.text += "+";
                        itemText.text += value;
                        itemText.text += "\n";
                        break;
                    case StatusType.ArrowSpeed:
                        itemText.text += "투사체속도" + "\n";
                        if (value > 0) itemText.text += "+";
                        itemText.text += value + "f";
                        itemText.text += "\n";
                        break;
                    case StatusType.ArrowCount:
                        itemText.text += "화살개수" + "\n";
                        if (value > 0) itemText.text += "+";
                        itemText.text += value + "개";
                        itemText.text += "\n";
                        break;
                    case StatusType.ShootSpeed:
                        itemText.text += "공격속도" + "\n";
                        if (value > 0) itemText.text += "+";
                        itemText.text += value + "%";
                        itemText.text += "\n";
                        break;
                    default:
                        itemText.text += key.ToString() + "\n";
                        itemText.text += value;
                        itemText.text += "\n";
                        break;
                }
                itemText.text += "</color>";
            }
        }
    }

    public void Init(Dictionary<StatusType, float> items, float speed)
    {
        this.items = items;

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
            var keys = items.Keys;
            foreach (var key in keys)
            {
                if (items.ContainsKey(key))
                {
                    float value;
                    items.TryGetValue(key, out value);
                    shooter.UpgradeShooter(key, value);
                }
            }

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
