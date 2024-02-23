using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerCollision))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] AudioClip jumpSound;

    [SerializeField] int MaxJumps;
    [SerializeField] float MovementSpeed;
    [SerializeField] float JumpStrength;
    [SerializeField] float CoyoteTime;
    [SerializeField] float JumpBufferTime;
    [SerializeField] bool WallJumpEnabled;

    private int leftJumps;
    private int LeftJumps
    {
        set
        {
            if (leftJumps != value)
            {
                leftJumps = value;
                EventManager.Instance.OnLeftJumpsChanged(leftJumps);
            }
        }
        get { return leftJumps; }
    }
    private bool jumpPressed;
    private float lastTimeJumpsReseted;
    private float lastTimeJumpPressed;

    private GameObject lastWallUsedForReset;


    private Rigidbody2D rb;
    private PlayerCollision collisionComponent;
    private AudioSource audioSource;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collisionComponent = GetComponent<PlayerCollision>();
        audioSource = GetComponent<AudioSource>();

        EventManager.Instance.SubscribeToCollectJumpsAdder(AddJumps);
        EventManager.Instance.SubscribeToCollectWallJump(EnableWallJump);



        lastWallUsedForReset = null;
    }
    private void OnDisable()
    {
        EventManager.Instance.UnsubscribeToCollectJumpsAdder(AddJumps);
        EventManager.Instance.UnsubscribeToCollectWallJump(EnableWallJump);
    }

    void Start()
    {
        LeftJumps = MaxJumps;
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

        if (WallJumpEnabled && isTouchingWall)
        {
            //if (lastWallUsedForReset != collisionComponent.LastTouchedWall)
            {
                lastTimeJumpsReseted = Time.time;
                lastWallUsedForReset = collisionComponent.LastTouchedWall;
            }
        }

        if (coyoteTimeFulfilled)
        {
            LeftJumps = MaxJumps;
        }
        else
        {
            if (LeftJumps == MaxJumps)
            {
                LeftJumps = MaxJumps - 1;
            }
        }

        if (jumpPressed)
        {
            if(LeftJumps > 0)
            {
                if (LeftJumps == MaxJumps)
                {
                    lastTimeJumpsReseted = 0;
                }
                LeftJumps = LeftJumps - 1;
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
        if (audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
            audioSource.pitch = 1f + UnityEngine.Random.Range(0, 0.5f);
        }
    }
    public void AddJumps()
    {
        MaxJumps++;
    }
    public void EnableWallJump()
    {
        WallJumpEnabled = true;
    }
}
