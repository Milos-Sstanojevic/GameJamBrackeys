using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LeverManager : MonoBehaviour
{
    private Dictionary<PlatformsControlledByLevers, Vector3> originalPositions = new Dictionary<PlatformsControlledByLevers, Vector3>();
    private Dictionary<PlatformsControlledByLevers, Vector3> targetPositions = new Dictionary<PlatformsControlledByLevers, Vector3>();
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float distance;
    [SerializeField] private List<PlatformsControlledByLevers> movingDoors = new List<PlatformsControlledByLevers>();
    private bool stopMoving;
    private bool isRaised;

    private void OnEnable()
    {
        EventManager.Instance.SubscribeToOnChangeDirection(Check);
    }

    private void Check(PlatformsControlledByLevers platform)
    {
        foreach(PlatformsControlledByLevers p in movingDoors)
        {
            if(p == platform)
            {
                stopMoving=true;
                break;
            }
        }
        foreach(PlatformsControlledByLevers p in movingDoors)
        {
            if(stopMoving)
            {
                StartCoroutine(MoveDoors(p.transform, originalPositions[p]));
                isRaised=false;
            }
        }
    }

    public void AddDoorToLever(PlatformsControlledByLevers movingPlatform)
    {
        movingDoors.Add(movingPlatform);
    }
    
    void Start()
    {
        for (int i = 0; i < movingDoors.Count; i++)
        {
            PlatformsControlledByLevers movingPlatform = movingDoors[i];
            originalPositions[movingPlatform] = movingPlatform.transform.position;
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

    public void PlatformMover()
        {
            switch (isRaised)
            {
                case true:
                    Debug.Log("case true");
                    StartCoroutine(CloseDoor());
                    isRaised = false;
                    break;
                    
                
                case false:
                    Debug.Log("case false");
                    foreach (PlatformsControlledByLevers movingPlatform in movingDoors)
                    {
                        StartCoroutine(MoveDoors(movingPlatform.transform, targetPositions[movingPlatform]));
                    };
                    isRaised=true;
                    break;
            }
        }
    

    private IEnumerator CloseDoor()
    {
        foreach (PlatformsControlledByLevers movingPlatform in movingDoors)
        {
            StartCoroutine(MoveDoors(movingPlatform.transform, originalPositions[movingPlatform]));
        }
    yield return null;
    }

    private IEnumerator MoveDoors(Transform movingPlatform, Vector3 target)
    {
        Vector3 startPosition = movingPlatform.position;
        movingPlatform.position = Vector3.Lerp(startPosition, target, moveSpeed);
        movingPlatform.position = target;
        yield return null;
    }
}
