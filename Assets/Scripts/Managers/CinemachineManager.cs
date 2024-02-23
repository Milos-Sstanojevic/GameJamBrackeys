using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CinemachineManager : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] CinemachineConfiner2D confiner;
    [SerializeField] List<PolygonCollider2D> colliders;

    private CinemachineVirtualCamera mainCamera;
    private CinemachineVirtualCamera framingCamera;
    private CinemachineTargetGroup framingGroup;
    public static CinemachineManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        mainCamera = GetComponentInChildren<CinemachineVirtualCamera>();

        framingCamera = Instantiate(mainCamera);
        framingCamera.transform.SetParent(transform);
        framingCamera.gameObject.SetActive(false);

        framingGroup = (new GameObject()).AddComponent<CinemachineTargetGroup>();
    }
    public void OnTeleportFinished(Vector3 newPosition)
    {
        var newCollider = colliders.Find(c =>
        {
            c.enabled = true;
            var b = c.OverlapPoint(newPosition);
            c.enabled = false;

            return b;
        });

        if (newCollider != null)
        {
            confiner.m_BoundingShape2D = newCollider;
        }
    }
    private Coroutine coroutine = null;
    public void Frame(Transform target1, Transform target2)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        framingGroup.m_Targets = new CinemachineTargetGroup.Target[2];

        framingGroup.m_Targets[0].target = target1;
        framingGroup.m_Targets[1].target = target2;

        framingGroup.m_Targets[0].weight = 1f;
        framingGroup.m_Targets[1].weight = 1f;

        mainCamera.gameObject.SetActive(false);
        framingCamera.gameObject.SetActive(true);

        framingCamera.Follow = framingGroup.Transform;

        //confiner.InvalidateCache();
    }
    public void Unframe(float seconds)
    {
        IEnumerator BackToFollowingPlayer(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            mainCamera.gameObject.SetActive(true);
            framingCamera.gameObject.SetActive(false);
        }

        coroutine = StartCoroutine(BackToFollowingPlayer(seconds));
    }
}
