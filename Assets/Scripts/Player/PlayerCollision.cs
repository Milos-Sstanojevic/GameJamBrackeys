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
    [SerializeField] UnityEvent<int> playerTookDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag(deadlyTag))
        {
            playerDied.Invoke();
        }
        else if (collision.gameObject.CompareTag(damageTag))
        {
            playerTookDamage.Invoke(10);
            GameObject.Destroy(collision.gameObject);
        }
    }
}