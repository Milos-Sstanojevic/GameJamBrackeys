using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndGameCanvas : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    
    private void OnEnable()
    {
        EventManager.Instance.SubscribeToPlayerDied(OpenOnDeath);
    }
    private void OnDisable()
    {
        EventManager.Instance.UnsubscribeToPlayerDied(OpenOnDeath);
    }
    public void OpenOnDeath()
    {
        Time.timeScale = 0;
        canvas.SetActive(true);
    }
}
