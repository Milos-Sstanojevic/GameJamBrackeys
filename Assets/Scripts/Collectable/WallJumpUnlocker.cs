using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpUnlocker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.Instance.OnCollectedWallJump();
            Destroy(gameObject);
        }
    }
}
