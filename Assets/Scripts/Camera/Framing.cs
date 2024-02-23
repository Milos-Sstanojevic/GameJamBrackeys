using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Framing : MonoBehaviour
{
    [SerializeField] Transform target1;
    [SerializeField] Transform target2;
    [SerializeField] float delay;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (CinemachineManager.Instance != null)
            {
                CinemachineManager.Instance.Frame(target1, target2);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (CinemachineManager.Instance != null)
            {
                CinemachineManager.Instance.Unframe(delay);
            }
        }
    }
}
