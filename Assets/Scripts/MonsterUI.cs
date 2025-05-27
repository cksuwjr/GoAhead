using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUI : MonoBehaviour
{
    private TextMeshProUGUI hpText;
    private Monster monster;

    private void Awake()
    {
        hpText = GetComponentInChildren<TextMeshProUGUI>();
        monster = GetComponentInParent<Monster>();
    }

    private void OnEnable()
    {
        monster.onHpChanged += HpUISet;
    }

    private void OnDisable()
    {
        monster.onHpChanged -= HpUISet;
    }

    private void HpUISet(float value)
    {
        hpText.text = value.ToString();
    }
}
