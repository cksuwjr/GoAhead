using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerr : MonoBehaviour
{
    private IMovable movement;
    private PlayerWeapon weapon;

    private void Awake()
    {
        TryGetComponent<IMovable>(out movement);
        TryGetComponent<PlayerWeapon>(out weapon);
    }

    public void GameStart()
    {
        movement?.SetAct(true);
        weapon?.Init();
    }

    public void GameStop()
    {
        movement?.SetAct(false);
    }

    public void CustomUpdate(Vector3 moveDir)
    {
        movement?.Move(moveDir);
        weapon?.Attack();
    }
}
