using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private TextMeshProUGUI valueText_AttackPower;
    private TextMeshProUGUI valueText_ShootSpeed;
    private TextMeshProUGUI valueText_ArrowCount;
    private TextMeshProUGUI valueText_ArrowSpeed;

    public void Init()
    {
        ArrowShooter arrowShooter;
        if (!GameManager.Instance.player.TryGetComponent<ArrowShooter>(out arrowShooter)) return;

        var statValue = GameObject.Find("StatValue");

        if (statValue.transform.Find("AttackPower").TryGetComponent<TextMeshProUGUI>(out valueText_AttackPower))
            valueText_AttackPower.text = arrowShooter.ArrowDamage.ToString();

        if (statValue.transform.Find("ShootSpeed").TryGetComponent<TextMeshProUGUI>(out valueText_ShootSpeed))
            valueText_ShootSpeed.text = arrowShooter.ShootSpeed.ToString();

        if (statValue.transform.Find("ArrowCount").TryGetComponent<TextMeshProUGUI>(out valueText_ArrowCount))
            valueText_ArrowCount.text = arrowShooter.ArrowCount.ToString();

        if (statValue.transform.Find("ArrowSpeed").TryGetComponent<TextMeshProUGUI>(out valueText_ArrowSpeed))
            valueText_ArrowSpeed.text = arrowShooter.ArrowSpeed.ToString();

        arrowShooter.onStatChange += StatusUIChange;
    }

    private void StatusUIChange(StatusType type, float value)
    {
        switch (type)
        {
            case StatusType.ArrowPower:
                valueText_AttackPower.text = value.ToString();
                break;
            case StatusType.ArrowSpeed:
                valueText_ArrowSpeed.text = value.ToString();
                break;
            case StatusType.ArrowCount:
                valueText_ArrowCount.text = value.ToString();
                break;
            case StatusType.ShootSpeed:
                valueText_ShootSpeed.text = string.Format("{0:d}%", Mathf.FloorToInt(100 * value));

                break;
            default:
                break;
        }
    }
}
