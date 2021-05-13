using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : Singleton<Paddle>
{
    private Camera mainCamera;
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    private float paddleInitialY;
    private float defaultPaddleWidthInPixels = 200;
    private float defaultLeftClamp = 135;
    private float defaultRightClamp = 410;

    public float extendShrinkDuration = 10f;
    public float paddleWidth = 2f;
    public float paddleHeight = 0.28f;

    public bool PaddleIsTransforming { get; set; }

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        paddleInitialY = this.transform.position.y;
    }

    void Update()
    {
        PaddleMovement();
    }

    private void PaddleMovement()
    {
        float paddleShift = (defaultPaddleWidthInPixels - ((defaultPaddleWidthInPixels / 2) * this.sr.size.x)) / 2;
        float leftClamp = defaultLeftClamp - paddleShift;
        float rightClamp = defaultRightClamp + paddleShift;
        float mousePositionPixels = Mathf.Clamp(Input.mousePosition.x, leftClamp, rightClamp);
        float mousePositionWorldX = mainCamera.ScreenToWorldPoint(new Vector3(mousePositionPixels, 0, 0)).x;
        this.transform.position = new Vector3(mousePositionWorldX, paddleInitialY, 0);
    }

    public void StartWidthAnimation(float newWidth)
    {
        StartCoroutine(AnimatePaddleWidth(newWidth));
    }

    public IEnumerator AnimatePaddleWidth(float width)
    {
        this.PaddleIsTransforming = true;
        StartCoroutine(ResetPaddleWidthAfterTime(extendShrinkDuration));

        if(width > sr.size.x)
        {
            float currentWidth = sr.size.x;
            while(currentWidth < width)
            {
                currentWidth += Time.deltaTime * 2;
                sr.size = new Vector2(currentWidth, paddleHeight);
                bc.size = new Vector2(currentWidth, paddleHeight);
                yield return null;
            }
        }
        else
        {
            float currentWidth = sr.size.x;
            while(currentWidth > width)
            {
                currentWidth -= Time.deltaTime * 2;
                sr.size = new Vector2(currentWidth, paddleHeight);
                bc.size = new Vector2(currentWidth, paddleHeight);
                yield return null;
            }
        }
        PaddleIsTransforming = false;
    }

    public IEnumerator ResetPaddleWidthAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        StartWidthAnimation(paddleWidth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector3 hitPoint = collision.contacts[0].point;
            Vector3 paddleCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

            ballRb.velocity = Vector2.zero;

            float difference = paddleCenter.x - hitPoint.x;
            if (hitPoint.x < paddleCenter.x)
            {
                ballRb.AddForce(new Vector2(-(Mathf.Abs(difference * 200)), BallsManager.Instance.initialBallSpeed));
            }
            else
            {
                ballRb.AddForce(new Vector2((Mathf.Abs(difference * 200)), BallsManager.Instance.initialBallSpeed));
            }
        }
    }
}
