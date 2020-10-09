using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    public float horizontalSpeed;
    public float horiztonalBounds;
    public float maxVelocity;
    public float horizontalLerp;
   
    private Rigidbody2D rigidBody;
    private Vector3 touchesEnd;
    public BulletManager bulletManager;

    // Start is called before the first frame update
    void Start()
    {
        touchesEnd = new Vector3();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
    }

    private void _FireBullet()
    {
        if (Time.frameCount % 40 == 0)
        {
            bulletManager._GetBullet(transform.position);
        }
    }

    private void _CheckBounds()
    {
        // check left boundary
        if (transform.position.x <= -horiztonalBounds)
        {
            transform.position = new Vector3(-horiztonalBounds, transform.position.y, transform.position.z);
        }
        // check right boundary
        else if (transform.position.x >= horiztonalBounds)
        {
            transform.position = new Vector3(horiztonalBounds, transform.position.y, transform.position.z);
        }
    }
    private void _Move()
    {
        float direction = 0.0f;

        //touch input support
        foreach(var touch in Input.touches)
        {
            _FireBullet();

            var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

            if (worldTouch.x > transform.position.x)
            {
                direction = 1.0f;
            }   
            
            if (worldTouch.x < transform.position.x)
            {
                direction = -1.0f;
            }

            touchesEnd = worldTouch;
        }

        // keyboard support
        if (Input.GetAxis("Horizontal") >= 0.1f)
        {
            // direction is positive
            direction = 1.0f;
        }

        if (Input.GetAxis("Horizontal") <= -0.1f)
        {
            // direction is negative
            direction = -1.0f;
        }

        if (touchesEnd.x != 0.0f)
        {
            transform.position = new Vector2(Mathf.Lerp(transform.position.x, touchesEnd.x, 0.1f), transform.position.y);
        }
        else
        {
            Vector2 newVelocity = rigidBody.velocity = new Vector2(direction * horizontalSpeed, 0.0f);
            rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxVelocity);
            rigidBody.velocity *= 0.99f;
        }
    }
}
