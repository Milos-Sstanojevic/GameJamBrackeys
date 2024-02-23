using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] float checkDepth;
    [SerializeField] float feetVerticalOffset;
    [SerializeField] float feetHorizontalSpread;

    [SerializeField] float wallCheckSpread;

    [SerializeField] float headOffset;

    private Vector3 leftFeet;
    private Vector3 rightFeet;

    private Vector3 leftWallCheckOrigin;
    private Vector3 rightWallCheckOrigin;

    private RaycastHit2D[] results;

    private bool isGrounded;
    public bool IsGrounded
    {
        get { return isGrounded; }
    }

    private bool isTouchingWall;
    public bool IsTouchingWall
    {
        get { return isTouchingWall; }
    }
    private GameObject lastTouchedWall;
    public GameObject LastTouchedWall
    {
        get { return lastTouchedWall; }
    }

    private bool isTouchingDoor;
    public bool IsTouchingDoor
    {
        get { return isTouchingDoor; }
    }
    private DoorsController lastTouchedDoor;
    public DoorsController LastTouchedDoor
    {
        get { return lastTouchedDoor; }
    }

    private void Awake()
    {
        results = new RaycastHit2D[10];
    }
    public void Update()
    {
        var feetLevel = transform.position + Vector3.down * feetVerticalOffset;

        leftFeet = feetLevel + Vector3.left * feetHorizontalSpread;
        rightFeet = feetLevel + Vector3.right * feetHorizontalSpread;

        bool hit1, hit2 = false;
        int c1 = Physics2D.RaycastNonAlloc(leftFeet, Vector3.down, results, checkDepth);
        hit1 = AnyIsPlatform(results, c1);
        if (!hit1)
        {
            int c2 = Physics2D.RaycastNonAlloc(rightFeet, Vector3.down, results, checkDepth);
            hit2 = AnyIsPlatform(results, c2);
        }

        //var hit1 = Physics2D.RaycastAll(leftFeet, Vector3.down, checkDepth).Any(hit => hit.collider.CompareTag("Platform"));
        //var hit2 = Physics2D.RaycastAll(rightFeet, Vector3.down, checkDepth).Any(hit => hit.collider.CompareTag("Platform"));

        isGrounded = hit1 || hit2;

        leftWallCheckOrigin = transform.position + Vector3.left * wallCheckSpread;
        rightWallCheckOrigin = transform.position + Vector3.right * wallCheckSpread;

        bool hit3, hit4 = false;
        int c3 = Physics2D.RaycastNonAlloc(leftWallCheckOrigin, Vector3.left, results, checkDepth);
        hit3 = AnyIsPlatform(results, c3);
        if (!hit3)
        {
            int c4 = Physics2D.RaycastNonAlloc(rightWallCheckOrigin, Vector3.right, results, checkDepth);
            hit4 = AnyIsPlatform(results, c4);
        }

        isTouchingWall = hit3 || hit4;
        if (isTouchingWall)
        {
            lastTouchedWall = lastTrueTriggeringObject;
        }

        if (isGrounded)
        {
            var headPosition = transform.position + headOffset * Vector3.up;

            int c5 = Physics2D.RaycastNonAlloc(headPosition, Vector3.up, results, checkDepth);

            if (AnyIsPlatform(results, c5))
            {
                EventManager.Instance.OnPlayerDied();
            }
        }
    }
    private GameObject lastTrueTriggeringObject;
    private bool AnyIsPlatform(RaycastHit2D[] hits, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (hits[i].collider == null) return false;

            if (hits[i].collider.CompareTag("Platform"))
            {
                lastTrueTriggeringObject = hits[i].collider.gameObject;
                return true;
            }
        }
        return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Deadly"))
        {
            EventManager.Instance.OnPlayerDied();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DoorsController door = other.gameObject.GetComponent<DoorsController>();

        if (door != null)
        {
            isTouchingDoor = true;
            lastTouchedDoor = door;
            //playerController.OnDoorCollisionDetected(door);
            //currentlyTouchedDoor = door;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        DoorsController door = other.gameObject.GetComponent<DoorsController>();

        if (door != null && door == lastTouchedDoor)
        {
            isTouchingDoor = false;
            // ???? lastTouchedDoor = door;

            //playerController.OnDoorCollisionEnd();
            //currentlyTouchedDoor = null;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + headOffset * Vector3.up, .1f);

        Gizmos.DrawLine(leftFeet, leftFeet + Vector3.down * checkDepth);
        Gizmos.DrawLine(rightFeet, rightFeet + Vector3.down * checkDepth);

        Gizmos.DrawLine(leftWallCheckOrigin, leftWallCheckOrigin + Vector3.left * checkDepth);
        Gizmos.DrawLine(rightWallCheckOrigin, rightWallCheckOrigin + Vector3.right * checkDepth);
    }
}