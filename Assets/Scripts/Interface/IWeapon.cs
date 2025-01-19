using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void SetOwner(GameObject owner);

    void SetUsable(bool usable);

    void Attack();
}
