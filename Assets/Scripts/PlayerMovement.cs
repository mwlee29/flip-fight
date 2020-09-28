﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float jumpPower = 1.0f;
    public float flipRate = 1.0f;

    private float nextFlip = 1.0f;

    bool isFlipped = false;

    private Vector2 moveDirection;

    Player _player;
    Rigidbody2D rb;
    SpriteRenderer _sprite;

    private void OnEnable()
    {
        _player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Move(moveDirection);
    }

    public void SetMovement(Vector2 direction)
    {
        moveDirection = direction;
    }

    private void Move(Vector2 direction)
    {
        var scaledSpeed = speed * Time.fixedDeltaTime;
        Vector2 v = new Vector2(direction.x, 0);

        rb.AddForce(scaledSpeed * v);
    }

    public void Jump()
    {
        Vector2 direction = new Vector2(0, jumpPower);

        if (isFlipped)
        {
            direction.y *= -1;
        }

        if (_player.isGrounded)
        {
            rb.AddForce(direction);
            _player.isGrounded = false;
        }
    }

    public void Flip()
    {
        Vector2 v = new Vector2(1, -1);

        if (Time.time >= nextFlip)
        {
            _player.EnemyCheck(transform.position * v);

            transform.position *= v;
            rb.velocity *= v;
            rb.gravityScale *= -1;
            nextFlip = Time.time + flipRate;

            if (isFlipped)
            {
                _sprite.color = Color.black;
            }
            else
            {
                _sprite.color = Color.white;
            }

            isFlipped = !isFlipped;
        }
    }
}
