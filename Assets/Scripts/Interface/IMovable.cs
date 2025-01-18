using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    void SetAct(bool act);

    void Move(Vector3 direction);

    void SetSpeed(float speed);
}
