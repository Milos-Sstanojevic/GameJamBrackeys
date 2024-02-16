using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int MaxJumps;
    [SerializeField] float MovementSpeed;
    [SerializeField] float JumpStrength;
    [SerializeField] float CoyoteTime;
    [SerializeField] float JumpBufferTime;
    [SerializeField] bool WallJumpEnabled;

    private int leftJumps;
    private bool jumpPressed;
    private float lastTimeJumpsReseted;
    private float lastTimeJumpPressed;

    private GameObject lastWallUsedForReset;


    private Rigidbody2D rb;
    private PlayerCollision collisionComponent;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collisionComponent = GetComponent<PlayerCollision>();

        lastWallUsedForReset = null;
    }

    void Start()
    {
        leftJumps = MaxJumps;
        jumpPressed = false;
        lastTimeJumpsReseted = float.MinValue;
        lastTimeJumpPressed = float.MinValue;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
            lastTimeJumpPressed = Time.time;
        }

        var isGrounded = collisionComponent.IsGrounded;
        var isTouchingWall = collisionComponent.IsTouchingWall;


        bool coyoteTimeFulfilled = (Time.time - lastTimeJumpsReseted) < CoyoteTime;
        bool jumpBufferTimeFulfilled = (Time.time - lastTimeJumpPressed) < JumpBufferTime;

        if (isGrounded)
        {
            lastTimeJumpsReseted = Time.time;
            lastWallUsedForReset = null;
        }

        if (isTouchingWall)
        {
            if (lastWallUsedForReset != collisionComponent.LastTouchedWall)
            {
                lastTimeJumpsReseted = Time.time;
                lastWallUsedForReset = collisionComponent.LastTouchedWall;
            }
        }

        if (coyoteTimeFulfilled)
        {
            leftJumps = MaxJumps;
        }
        else
        {
            if (leftJumps == MaxJumps)
            {
                leftJumps = MaxJumps - 1;
            }
        }

        if (jumpPressed)
        {
            if(leftJumps > 0)
            {
                if (leftJumps == MaxJumps)
                {
                    lastTimeJumpsReseted = 0;
                }
                leftJumps = leftJumps - 1;
                Jump();
                jumpPressed = false;
            }
        }

        if (!jumpBufferTimeFulfilled)
        {
            jumpPressed = false;
        }

        transform.Translate(MovementSpeed * Time.deltaTime * Input.GetAxisRaw("Horizontal") * Vector3.right);
    }
    private void Jump()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(JumpStrength * Vector3.up, ForceMode2D.Impulse);
    }
}
