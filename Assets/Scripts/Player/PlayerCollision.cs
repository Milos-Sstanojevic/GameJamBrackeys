using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    private PlayerDoorHandler playerController;
    private DoorsController currentlyTouchedDoor;

    private void Awake()
    {
        playerController = GetComponent<PlayerDoorHandler>();
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
}