using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    private void Awake()
    {
        if(TryGetComponent<Camera>(out var cam))
        {
            Rect rt = cam.rect;

            // scale_height = 실제(가로/세로)비율 / 만들려는(가로/세로)비율
            float scale_Height = ((float)Screen.width / Screen.height) / ((float)16 / 9);
            float scale_width = 1f / scale_Height;

            if (scale_Height < 1f)
            {
                rt.height = scale_Height;
                rt.width = (1f - scale_width) / 2f;
            }
            else
            {
                rt.width = scale_width;
                rt.x = (1f - scale_width) / 2f;
            }
            cam.rect = rt;
        }
    }
}
