using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class OneWayTeleport : MonoBehaviour
{
    public Vector3 teleportPosition;
    [SerializeField] UnityEvent specialTrigger;
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject player = other.GetComponent<PlayerController>()?.gameObject;
        if (player != null)
        {
            player.transform.position = teleportPosition;
            EventManager.Instance.OnPlayerTeleported(player);
            specialTrigger?.Invoke();
        }
    }
}
