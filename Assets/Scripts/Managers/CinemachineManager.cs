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
    private CinemachineConfiner2D framingVirtualCameraConfiner;

    private CinemachineTargetGroup framingGroup;

    public static CinemachineManager Instance;
    private void OnEnable()
    {
        EventManager.Instance.SubscribeToPlayerTeleported(OnTeleportationFinished);
    }
    private void OnDisable()
    {
        EventManager.Instance.UnsubscribeToPlayerTeleported(OnTeleportationFinished);
    }
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

        framingVirtualCameraConfiner = framingVirtualCamera.GetComponent<CinemachineConfiner2D>();

        framingGroup = (new GameObject()).AddComponent<CinemachineTargetGroup>();
    }
    public void OnTeleportationFinished(GameObject player)
    {
        var newCollider = colliders.Find(c =>
        {
            c.enabled = true;
            var b = c.OverlapPoint(player.transform.position);
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
        var test1 = Camera.main.WorldToViewportPoint(target1.position);
        var test2 = Camera.main.WorldToViewportPoint(target1.position);

        if (!(test1.x < 0f || test1.x > 1f ||
              test1.y < 0f || test1.y > 1f ||
              test2.x < 0f || test2.x > 1f ||
              test2.y < 0f || test2.y > 1f)) return;

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

        framingVirtualCameraConfiner.m_BoundingShape2D = mainVirtualCameraConfiner.m_BoundingShape2D;

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
