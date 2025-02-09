using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    [SerializeField] private List<ScrollObject> scrollObjects;
    private float scrollSpeed;
    
    private void Update()
    {
        if (scrollObjects.Count < 1) return;

        for(int i = 0; i < scrollObjects.Count; i++)
            scrollObjects[i].Scroll(Time.deltaTime);
    }

    public void SetScrollSpeed(float value)
    {
        scrollSpeed = value;
        for(int i = 0;i < scrollObjects.Count;i++)
            scrollObjects[i].SetScrollSpeed(scrollSpeed);
    }
}
