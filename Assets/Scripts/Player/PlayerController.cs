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

    private int leftJumps;
    private bool jumpPressed;
    private float lastTimeGrounded;
    private float lastTimeJumpPressed;
    private Rigidbody2D rb;
    private PlayerCollision collisionComponent;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collisionComponent = GetComponent<PlayerCollision>();
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
            lastTimeJumpPressed = Time.time;
        }

        var isGrounded = collisionComponent.IsGrounded;

        //}
        //private void FixedUpdate()
        //{


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
            rb.AddForce(JumpStrength * Vector3.up, ForceMode2D.Impulse);
            leftJumps--;
            jumpPressed = false;
        }
        else if (!jumpBufferTimeFulfilled)
        {
            jumpPressed = false;
        }

        transform.Translate(MovementSpeed * Time.deltaTime * Input.GetAxisRaw("Horizontal") * Vector3.right);
    }

    //private void PlaceDoor()
    //{
    //    if (Input.GetKeyDown(KeyCode.G))
    //    {
    //        DoorsController door = PlayerDoorInventory.Instance.GetGreenDoors();
    //        if (door != null)
    //        {
    //            door.gameObject.SetActive(true);
    //            EventManager.Instance.OnPlaceDoorHorizontally(door, new Vector3(transform.position.x + 2, transform.position.y, transform.position.z));
    //        }
    //    }

    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        DoorsController door = PlayerDoorInventory.Instance.GetRedDoors();
    //        if (door != null)
    //        {
    //            door.gameObject.SetActive(true);
    //            EventManager.Instance.OnPlaceDoorHorizontally(door, new Vector3(transform.position.x + 2, transform.position.y, transform.position.z));
    //        }
    //    }

    //    if (Input.GetKeyDown(KeyCode.B))
    //    {
    //        DoorsController door = PlayerDoorInventory.Instance.GetBlueDoors();
    //        if (door != null)
    //        {
    //            door.gameObject.SetActive(true);
    //            EventManager.Instance.OnPlaceDoorHorizontally(door, new Vector3(transform.position.x + 2, transform.position.y, transform.position.z));
    //        }
    //    }
    //}
}
