using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] LayerMask killerLayer;
    [SerializeField] LayerMask damangeLayer;
    [SerializeField] UnityEvent playerDied;
    [SerializeField] UnityEvent<int> playerTookDamage;
    private void OnCollisionEnter(Collision collision)
    {
        var cl = collision.gameObject.layer;
        if (cl == killerLayer)
        {
            playerDied.Invoke();
        }
        else if (cl == damangeLayer)
        {
            playerTookDamage.Invoke(-99);
        }
    }
}
