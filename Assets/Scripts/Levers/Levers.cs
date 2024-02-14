using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Levers : MonoBehaviour
{
    private Dictionary<Transform, Vector3> originalPositions = new Dictionary<Transform, Vector3>();
    private Dictionary<Transform, Vector3> targetPositions = new Dictionary<Transform, Vector3>();
    [SerializeField] private float timer = 3f;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float distance;
    [SerializeField] private List<Transform> movingDoors = new List<Transform>();


    public void AddDoorToLever(Transform movingPlatform)
    {
        movingDoors.Add(movingPlatform);
    }

    void Start()
    {
        for (int i = 0; i < movingDoors.Count; i++)
        {
            Transform movingPlatform = movingDoors[i];
            originalPositions[movingPlatform] = movingPlatform.position;
            if (i % 2 == 0)
            {
                targetPositions[movingPlatform] = originalPositions[movingPlatform] + Vector3.up * distance;
            }
            else
            {
                 targetPositions[movingPlatform] = originalPositions[movingPlatform] + Vector3.up * -distance;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                foreach (Transform movingPlatform in movingDoors)
                {
                    StartCoroutine(MoveDoors(movingPlatform, targetPositions[movingPlatform]));
                }
                StartCoroutine(CloseDoor());
            }
        }
    }

    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(timer);
        foreach (Transform movingPlatform in movingDoors)
        {
            StartCoroutine(MoveDoors(movingPlatform, originalPositions[movingPlatform]));
        }
    }

    IEnumerator MoveDoors(Transform movingPlatform, Vector3 target)
    {
        Vector3 startPosition = movingPlatform.position;
        float elapsedTime = 0;

        while (elapsedTime < timer)
        {
            movingPlatform.position = Vector3.Lerp(startPosition, target, (elapsedTime / timer) * moveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        movingPlatform.position = target;
    }
}