using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private IWeapon[] activeWeapons;

    public void Init()
    {
        activeWeapons = GetComponents<IWeapon>();
        for (int i = 0; i < activeWeapons.Length; i++)
        {
            activeWeapons[i].SetUsable(true);
            activeWeapons[i].SetOwner(gameObject);
        }
    }

    public void Attack()
    {
        if(activeWeapons.Length < 1) return;

        for(int i = 0; i < activeWeapons.Length; i++)
        {
            activeWeapons[i].Attack();
        }
    }
}
