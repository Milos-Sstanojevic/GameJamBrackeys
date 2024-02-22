using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Levers : MonoBehaviour
{
    private Dictionary<PlatformsControlledByLevers, Vector3> originalPositions = new Dictionary<PlatformsControlledByLevers, Vector3>();
    private Dictionary<PlatformsControlledByLevers, Vector3> targetPositions = new Dictionary<PlatformsControlledByLevers, Vector3>();
    [SerializeField] private float timer = 3f;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float distance;
    [SerializeField] private List<PlatformsControlledByLevers> movingDoors = new List<PlatformsControlledByLevers>();
    private bool stopMoving;

    private Animator animator;
    private AudioSource audioSource;

    private void OnEnable()
    {
        EventManager.Instance.SubscribeToOnChangeDirection(Check);
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
            }
        }
    }

    public void AddDoorToLever(PlatformsControlledByLevers movingPlatform)
    {
        movingDoors.Add(movingPlatform);
    }
    private void Start()
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
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator?.SetTrigger("Press");
                audioSource?.PlayOneShot(audioSource.clip);
                foreach (PlatformsControlledByLevers movingPlatform in movingDoors)
                {
                    StartCoroutine(MoveDoors(movingPlatform.transform, targetPositions[movingPlatform]));
                }
                StartCoroutine(CloseDoor());
            }
        }
    }

    private IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(timer);
        foreach (PlatformsControlledByLevers movingPlatform in movingDoors)
        {
            StartCoroutine(MoveDoors(movingPlatform.transform, originalPositions[movingPlatform]));
        }
    }

    private IEnumerator MoveDoors(Transform movingPlatform, Vector3 target)
    {
        Debug.Log("Lever Clicked za " + movingPlatform.name);
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