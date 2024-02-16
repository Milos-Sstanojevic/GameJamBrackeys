using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int health;
    public int Health
    {
        set
        {
            health = value;
            EventManager.Instance.OnHealthChanged(health);
        }
        get { return health; }
    }
    private void Start()
    {
        Health = 100;
        EventManager.Instance.SubscribeToPlayerDied(Die);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            TakeDamage(10);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Die();
        }
    }
    public void Die()
    {
        Health = 0;
    }
    public void TakeDamage(int dmg)
    {
        Health -= dmg;
        Health = Health < 0 ? 0 : Health;
    }
}
