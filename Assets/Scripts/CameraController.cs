using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float BaseCameraSize = 5;
    public float BaseAspectRatio = 9/16f;
    public Camera cam;
    public GameObject backgroundImg;

    private void Awake()
    {
        Camera.main.orthographicSize = BaseCameraSize * (BaseAspectRatio / Camera.main.aspect);
        //ScaleToScreenSize();
    }

    private void ScaleToScreenSize()
    {
        Vector2 deviceScreenResolution = new Vector2(Screen.width, Screen.height);
        float screenHeight = Screen.height;
        float screenWidth = Screen.width;
        float deviceScreenAspect = screenWidth / screenHeight;

        cam.aspect = deviceScreenAspect;

        float camHeight = 100f * cam.orthographicSize * 2f;
        float camWidth = camHeight * deviceScreenAspect;

        SpriteRenderer backgroundImage = backgroundImg.GetComponent<SpriteRenderer>();
        float bgHeight = backgroundImage.sprite.rect.height;
        float bgWidth = backgroundImage.sprite.rect.width;

        float bgImgScaleRatioHeight = camHeight / bgHeight;
        float bgImhScaleRatioWidth = camWidth / bgWidth;

        backgroundImage.transform.localScale = new Vector3(bgImgScaleRatioHeight, bgImhScaleRatioWidth, 1);
    }
}
