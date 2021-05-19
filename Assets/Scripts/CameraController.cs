using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float BaseCameraSize = 5;
    public float BaseAspectRatio = 9/16f;

    private void Awake()
    {
        Camera.main.orthographicSize = BaseCameraSize * (BaseAspectRatio / Camera.main.aspect);
    }
}
