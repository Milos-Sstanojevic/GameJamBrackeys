using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] float FeetVerticalOffset;
    [SerializeField] float FeetHorizontalSpread;

    private PlayerDoorHandler playerController;
    private DoorsController currentlyTouchedDoor;

    private Vector3 leftFeet;
    private Vector3 rightFeet;

    private RaycastHit2D[] results;

    private bool isGrounded;
    public bool IsGrounded
    {
        get { return isGrounded; }
    }

    private void Awake()
    {
        playerController = GetComponent<PlayerDoorHandler>();
        results = new RaycastHit2D[10];
    }
    public void Update()
    {
        var feetLevel = transform.position + Vector3.down * FeetVerticalOffset;

        leftFeet = feetLevel + Vector3.left * FeetHorizontalSpread;
        rightFeet = feetLevel + Vector3.right * FeetHorizontalSpread;

        int c1 = Physics2D.RaycastNonAlloc(leftFeet, Vector3.down, results, 0.1f);
        bool hit1 = Any(results, c1);
        int c2 = Physics2D.RaycastNonAlloc(rightFeet, Vector3.down, results, 0.1f);
        bool hit2 = Any(results, c2);

        //var hit1 = Physics2D.RaycastAll(leftFeet, Vector3.down, .1f).Any(hit => hit.collider.CompareTag("Platform"));
        //var hit2 = Physics2D.RaycastAll(rightFeet, Vector3.down, .1f).Any(hit => hit.collider.CompareTag("Platform"));

        isGrounded = hit1 || hit2;
    }
    private bool Any(RaycastHit2D[] hits, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (hits[i].collider == null) return false;

            if (hits[i].collider.CompareTag("Platform"))
            {
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
            playerController.OnDoorCollisionDetected(door);
            currentlyTouchedDoor = door;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        DoorsController door = other.gameObject.GetComponent<DoorsController>();

        if (door != null && door == currentlyTouchedDoor)
        {
            playerController.OnDoorCollisionEnd();
            currentlyTouchedDoor = null;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(leftFeet, .1f);
        Gizmos.DrawSphere(rightFeet, .1f);
    }
}