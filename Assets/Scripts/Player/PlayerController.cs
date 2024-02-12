using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    [SerializeField] float FeetSpread;
    [SerializeField] Vector3 FeetVerticalOffset;

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
    private Vector3 leftFeet;
    private Vector3 rightFeet;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
            lastTimeJumpPressed = Time.time;
        }
    //}
    //private void FixedUpdate()
    //{
        leftFeet = transform.position + FeetVerticalOffset + Vector3.left * FeetSpread;
        rightFeet = transform.position + FeetVerticalOffset + Vector3.right * FeetSpread;

        var hit1 = Physics2D.RaycastAll(leftFeet, Vector3.down, .1f).Any(hit => hit.collider.CompareTag("Platform"));
        var hit2 = Physics2D.RaycastAll(rightFeet, Vector3.down, .1f).Any(hit => hit.collider.CompareTag("Platform"));

        bool isGrounded = hit1 || hit2;
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

        transform.Translate(MovementSpeed * Time.deltaTime * Input.GetAxisRaw("Horizontal") * Vector3.right);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(leftFeet, .1f);
        Gizmos.DrawSphere(rightFeet, .1f);
    }
}
