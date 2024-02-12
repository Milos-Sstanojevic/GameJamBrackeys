using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int MaxJumps;
    [SerializeField] float MovementSpeed;
    [SerializeField] float JumpStrength;
    [SerializeField] float CoyoteTime;
    [SerializeField] float JumpBufferTime;
    [SerializeField] Vector3 FeetOffset;

    private int leftJumps;
    private bool jumpPressed;
    private float lastTimeGrounded;
    private float lastTimeJumpPressed;

    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    void Start()
    {
        leftJumps = MaxJumps;
        jumpPressed = false;
        lastTimeGrounded = float.MinValue;
        lastTimeJumpPressed = float.MinValue;
    }
    void Update()
    {
        var xInput = Input.GetAxisRaw("Horizontal");

        if (xInput != 0)
        {
            transform.Translate(MovementSpeed * Time.deltaTime * xInput * Vector3.right);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
            lastTimeJumpPressed = Time.time;
        }
    }
    private void FixedUpdate()
    {
        //bool isGrounded = Physics2D.OverlapCircle(transform.position + FeetOffset, .1f, PlatformLayer) != null;
        //bool isGrounded = Physics2D.OverlapArea(transform.position + FeetOffset - new Vector3(.5f, .1f, 0), transform.position + FeetOffset + new Vector3(.5f, .1f, 0), PlatformLayer) != null;
        var feetPosition = transform.position + FeetOffset;
        var hit1 = Physics2D.Raycast(feetPosition + Vector3.left * .5f, Vector3.down, .1f);
        var hit2 = Physics2D.Raycast(feetPosition + Vector3.right * .5f, Vector3.down, .1f);

        bool isGrounded = hit1.collider || hit2.collider;
        bool coyoteTimeFulfilled = !isGrounded && (Time.time - lastTimeGrounded) < CoyoteTime;
        bool jumpBufferTimeFulfilled = (Time.time - lastTimeJumpPressed) < JumpBufferTime;

        lastTimeGrounded = isGrounded ? Time.time : lastTimeGrounded;

        if (isGrounded)
        {
            leftJumps = MaxJumps;
        }
        else if (leftJumps == MaxJumps)
        {
            if (!coyoteTimeFulfilled)
            {
                leftJumps = MaxJumps - 1;
            }
        }

        if (leftJumps > 0 && jumpPressed)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(JumpStrength * Vector3.up,ForceMode2D.Impulse);
            leftJumps--;
            jumpPressed = false;
        }
        else if (!jumpBufferTimeFulfilled)
        {
            jumpPressed = false;
        }
    }
}
