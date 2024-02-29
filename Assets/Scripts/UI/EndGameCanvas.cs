using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndGameCanvas : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    public void OpenOnDeath()
    {
        Time.timeScale = 0;
        canvas.SetActive(true);
    }
}
