using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardInputHandle : MonoBehaviour, IInputHandle
{
    public Vector3 GetInput()
    {
        return new Vector3(Input.GetAxisRaw("Horizontal"), 0,Input.GetAxisRaw("Vertical"));
    }
}
