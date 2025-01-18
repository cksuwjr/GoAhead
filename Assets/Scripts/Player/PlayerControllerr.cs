using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerr : MonoBehaviour
{
    private IMovable movement;

    private void Awake()
    {
        TryGetComponent<IMovable>(out movement);
    }

    public void GameStart()
    {
        movement?.SetAct(true);
    }

    public void GameStop()
    {
        movement?.SetAct(false);
    }

    public void CustomUpdate(Vector3 moveDir)
    {
        movement?.Move(moveDir);
    }
}
