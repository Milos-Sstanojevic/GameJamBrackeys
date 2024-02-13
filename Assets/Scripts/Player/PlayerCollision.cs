using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] string deadlyTag;
    [SerializeField] UnityEvent playerDied;
    private PlayerController playerController;
    private DoorsController currentllyTouchedDoor;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(deadlyTag))
        {
            playerDied.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DoorsController door = other.gameObject.GetComponent<DoorsController>();

        if (door != null)
        {
            playerController.CanCollectDoor(door);
            currentllyTouchedDoor = door;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        DoorsController door = other.gameObject.GetComponent<DoorsController>();

        if (door != null && door == currentllyTouchedDoor)
        {
            playerController.CannotCollectDoor();
            currentllyTouchedDoor = null;
        }
    }
}