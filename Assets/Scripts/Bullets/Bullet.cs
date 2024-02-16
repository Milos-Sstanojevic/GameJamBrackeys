using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float TimeToLive;
    public float ActivationTime;
    public Vector3 Velocity;
    void Update()
    {
        transform.Translate(Velocity * Time.deltaTime);

        if ((Time.time - ActivationTime) > TimeToLive)
        {
            BulletPool.Instance.ReleaseBullet(this);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var go = collision.gameObject;
        if (go.CompareTag("Player"))
        {
            var playerHealth = go.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(10);
            BulletPool.Instance.ReleaseBullet(this);
        }
    }
}
