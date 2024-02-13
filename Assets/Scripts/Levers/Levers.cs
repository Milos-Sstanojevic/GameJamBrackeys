using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Levers : MonoBehaviour
{
    [SerializeField] private Transform movingDoorsTransform;
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    [SerializeField] private bool doorOpen = false;
    [SerializeField] private float timer = 3f;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float distance;

    void Start()
    {
        originalPosition = movingDoorsTransform.position;
        targetPosition = originalPosition + Vector3.up * distance;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && !doorOpen)
            {
                doorOpen = true;
                Debug.Log("Opening doors");
                StartCoroutine(MoveDoors(targetPosition));
                StartCoroutine(CloseDoor());
            }
        }
    }

    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(timer);
        doorOpen = false;
        Debug.Log("Closing doors");
        StartCoroutine(MoveDoors(originalPosition));
    }

    IEnumerator MoveDoors(Vector3 target)
    {
        Vector3 startPosition = movingDoorsTransform.position;
        float elapsedTime = 0;

        while (elapsedTime < timer)
        {
            movingDoorsTransform.position = Vector3.Lerp(startPosition, target, (elapsedTime / timer) * moveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        movingDoorsTransform.position = target;
    }
}