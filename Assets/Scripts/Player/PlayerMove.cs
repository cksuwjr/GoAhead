using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IMovable
{
    private bool isMoving = false;
    private float speed = 5f;
    
    private Vector3 moveDelta;
    private Vector3 minAreaXZ = new Vector3(-5f, 0, 1f);
    private Vector3 maxAreaXZ = new Vector3(5f, 0, 10f);

    public void SetAct(bool act)
    {
        isMoving = act;
    }

    public void Move(Vector3 direction)
    {
        if (isMoving)
        {
            moveDelta.x = direction.x;
            moveDelta.y = 0;
            moveDelta.z = direction.z;

            moveDelta.Normalize();

            moveDelta *= (speed * Time.deltaTime);
            moveDelta += transform.position;

            moveDelta.x = Mathf.Clamp(moveDelta.x, minAreaXZ.x, maxAreaXZ.x);
            moveDelta.z = Mathf.Clamp(moveDelta.z, minAreaXZ.z, maxAreaXZ.z);


            transform.position = moveDelta;
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
