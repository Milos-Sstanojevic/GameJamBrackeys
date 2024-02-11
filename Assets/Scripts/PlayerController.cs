using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float MovementSpeed;
    [SerializeField] public float JumpStrength;
    [SerializeField] public float CoyoteTime;
    [SerializeField] public Vector3 FeetOffset;
    [SerializeField] public LayerMask PlatformLayer;

    private bool wantToJump;
    private Vector3 FeetPostion;
    private Rigidbody2D rb;
    private float lastTimeGrounded;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        wantToJump = false;
        FeetPostion = transform.position + FeetOffset;
        lastTimeGrounded = float.MinValue;
    }
    void Update()
    {
        FeetPostion = transform.position + FeetOffset;

        var xInput = Input.GetAxis("Horizontal");

        if (xInput != 0)
        {
            transform.Translate(Vector3.right * xInput * Time.deltaTime * MovementSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            wantToJump = true;
        }
    }
    private void FixedUpdate()
    {
        bool isGrounded = Physics2D.OverlapCircle(FeetPostion, .1f, PlatformLayer) != null;
        bool coyoteFulfilled = !isGrounded && (Time.time - lastTimeGrounded) < CoyoteTime;

        if (wantToJump)
        {
            if (isGrounded || coyoteFulfilled)
            {
                wantToJump = false;
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * Time.deltaTime * JumpStrength);
            }
        }

        lastTimeGrounded = isGrounded ? Time.time : lastTimeGrounded;
    }
}
