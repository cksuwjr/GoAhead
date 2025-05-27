using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IScrollObject
{
    void Scroll(float deltaTime);
    void ResetPosition();
    void SetScrollSpeed(float newSpeed);
}

public class ScrollObject : MonoBehaviour, IScrollObject
{
    private float scrollSpeed = 0f;
    private Vector3 startPos = new Vector3(0, 0, 75f);
    private float resetPosZ = -25f;

    public void ResetPosition()
    {
        transform.position = startPos;
    }

    public void Scroll(float deltaTime)
    {
        transform.position += Vector3.back * (scrollSpeed * deltaTime);
        if(transform.position.z < resetPosZ)
        {
            ResetPosition();
        }
    }

    public void SetScrollSpeed(float newSpeed)
    {
        scrollSpeed = newSpeed;
    }
}
