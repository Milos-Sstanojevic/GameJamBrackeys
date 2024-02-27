using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OneWayTeleport : MonoBehaviour
{
    public Vector3 teleportPosition;
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject player = other.GetComponent<PlayerController>()?.gameObject;
        if (player != null)
        {
            player.transform.position = teleportPosition;
            EventManager.Instance.OnPlayerTeleported(player);
        }
    }
}
