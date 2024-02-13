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
    [SerializeField] float FeetSpread;
    [SerializeField] Vector3 FeetVerticalOffset;
    private int leftJumps;
    private bool jumpPressed;
    private float lastTimeGrounded;
    private float lastTimeJumpPressed;
    private Vector3 leftFeet;
    private Vector3 rightFeet;
    private Rigidbody2D rb;
    private DoorsController currentTouchingDoor;

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
        PlaceDoor();
        CollectDoor();
        TeleportPlayer();

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

    private void PlaceDoor()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            DoorsController door = PlayerDoorInventory.Instance.GetGreenDoors();
            if (door != null)
            {
                door.gameObject.SetActive(true);
                EventManager.Instance.OnPlaceDoorHorizontally(door, new Vector3(transform.position.x + 2, transform.position.y, transform.position.z));
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            DoorsController door = PlayerDoorInventory.Instance.GetRedDoors();
            if (door != null)
            {
                door.gameObject.SetActive(true);
                EventManager.Instance.OnPlaceDoorHorizontally(door, new Vector3(transform.position.x + 2, transform.position.y, transform.position.z));
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            DoorsController door = PlayerDoorInventory.Instance.GetBlueDoors();
            if (door != null)
            {
                door.gameObject.SetActive(true);
                EventManager.Instance.OnPlaceDoorHorizontally(door, new Vector3(transform.position.x + 2, transform.position.y, transform.position.z));
            }
        }
    }

    private void CollectDoor()
    {
        if (Input.GetKeyDown(KeyCode.K) && currentTouchingDoor != null && !currentTouchingDoor.IsDoorPremadeInScene())
        {
            EventManager.Instance.OnCollectDoor(currentTouchingDoor);
        }
    }

    private void TeleportPlayer()
    {
        if (Input.GetKeyDown(KeyCode.T) && currentTouchingDoor != null)
        {
            EventManager.Instance.OnTeleportPlayer(currentTouchingDoor, this);
        }
    }

    public void CanCollectDoor(DoorsController door)
    {
        currentTouchingDoor = door;
    }

    public void CannotCollectDoor()
    {
        currentTouchingDoor = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(leftFeet, .1f);
        Gizmos.DrawSphere(rightFeet, .1f);
    }
}
