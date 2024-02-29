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
            if (health != value)
            {
                health = value;
                EventManager.Instance.OnHealthChanged(health);
            }
        }
        get { return health; }
    }
    private void OnEnable()
    {
        EventManager.Instance.SubscribeToLethalDamageTaken(Die);
    }
    private void OnDisable()
    {
        EventManager.Instance.UnsubscribeToLethalDamageTaken(Die);
    }
    private void Start()
    {
        Health = 100;
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
