using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int health;

    public int MaxHealth;
    public int Health {
        get
        { 
            return health; 
        }
    }
    private void Start()
    {
        health = MaxHealth;
    }
    public void Die()
    {
        health = 0;
        Debug.Log("I died");
    }
    public void TakeDamage(int dmg)
    {
        health -= dmg;
        health = health < 0 ? 0 : health;
        Debug.Log("I took damage: " + dmg);
    }
}
