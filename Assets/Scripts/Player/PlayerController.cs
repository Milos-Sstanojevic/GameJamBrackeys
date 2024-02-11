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
    [SerializeField] LayerMask PlatformLayer;

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
        var xInput = Input.GetAxis("Horizontal");

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
        bool isGrounded = Physics2D.OverlapArea(transform.position + FeetOffset - new Vector3(.5f, .1f, 0), transform.position + FeetOffset + new Vector3(.5f, .1f, 0), PlatformLayer) != null;
        bool coyoteTimeFulfilled = !isGrounded && (Time.time - lastTimeGrounded) < CoyoteTime;
        bool jumpBufferTimeFulfilled = (Time.time - lastTimeJumpPressed) < JumpBufferTime;

        lastTimeGrounded = isGrounded ? Time.time : lastTimeGrounded;

        if (isGrounded)
        {
            leftJumps = MaxJumps;
        }
        else if (coyoteTimeFulfilled && leftJumps == MaxJumps)
        {

        }


        if (leftJumps > 0 && jumpPressed)
        {
            Debug.Log(leftJumps);

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
