using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var playerKeysController = collision.collider.GetComponent<PlayerKeys>();
            if (playerKeysController.HasKeys())
            {
                playerKeysController.UseKey();
                Destroy(gameObject);
            }
        }
    }
}
