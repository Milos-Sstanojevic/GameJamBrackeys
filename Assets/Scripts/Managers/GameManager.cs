using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        //EventManager.Instance.SubscribeToHealthChanged(health => { if (health == 0) OnPlayerDeath(); });
    }
    private void OnPlayerDeath()
    {
        //RestartManager.Instance.Restart();
    }
}
