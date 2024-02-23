using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CinemachineManager : MonoBehaviour
{
    [SerializeField] GameObject mainVirtualCameraGO;
    [SerializeField] List<PolygonCollider2D> colliders;

    private CinemachineVirtualCamera mainVirtualCamera;
    private CinemachineVirtualCamera framingVirtualCamera;

    private CinemachineConfiner2D mainVirtualCameraConfiner;

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

        mainVirtualCamera = mainVirtualCameraGO.GetComponent<CinemachineVirtualCamera>();
        mainVirtualCameraConfiner = mainVirtualCameraGO.GetComponent<CinemachineConfiner2D>();

        framingVirtualCamera = Instantiate(mainVirtualCamera);
        framingVirtualCamera.transform.SetParent(transform);
        framingVirtualCamera.gameObject.SetActive(false);

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
            mainVirtualCameraConfiner.m_BoundingShape2D = newCollider;
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

        mainVirtualCamera.gameObject.SetActive(false);
        framingVirtualCamera.gameObject.SetActive(true);

        framingVirtualCamera.Follow = framingGroup.Transform;
    }
    public void Unframe(float seconds)
    {
        IEnumerator GoBack(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            mainVirtualCamera.gameObject.SetActive(true);
            framingVirtualCamera.gameObject.SetActive(false);
        }

        coroutine = StartCoroutine(GoBack(seconds));
    }
}
