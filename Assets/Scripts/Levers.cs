using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Levers : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private bool doorOpen = false;
    [SerializeField] private float timer;

    void OnTriggerStay2D()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            door.SetActive(false);
            doorOpen = true;
            Debug.Log("Otvoreno");
            StartCoroutine(CloseDoor());
        }
    }

    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(timer);
        door.SetActive(true);
        doorOpen = false;
        Debug.Log("Zatvoreno");
    }
}
