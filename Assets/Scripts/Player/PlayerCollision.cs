using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] string deadlyTag;
    [SerializeField] string damageTag;
    [SerializeField] UnityEvent playerDied;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(deadlyTag))
        {
            playerDied.Invoke();
        } 
    }
}