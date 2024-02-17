using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectedLevers : MonoBehaviour
{
    public LeverManager leverManager;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null && Input.GetKeyDown(KeyCode.E))
        {
            leverManager.PlatformMover();
        }
    }
}
